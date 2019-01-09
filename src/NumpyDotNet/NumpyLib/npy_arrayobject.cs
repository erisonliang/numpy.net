﻿/*
 * BSD 3-Clause License
 *
 * Copyright (c) 2018-2019
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice,
 *    this list of conditions and the following disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 *    this list of conditions and the following disclaimer in the documentation
 *    and/or other materials provided with the distribution.
 *
 * 3. Neither the name of the copyright holder nor the names of its
 *    contributors may be used to endorse or promote products derived from
 *    this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
 * FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if NPY_INTP_64
using npy_intp = System.Int64;
using npy_ucs4 = System.Int64;
using NpyArray_UCS4 = System.Int64;
#else
using npy_intp = System.Int32;
using npy_ucs4 = System.Int32;
using NpyArray_UCS4 = System.Int32;
#endif
using size_t = System.UInt64;

using System.ComponentModel;

namespace NumpyLib
{
    #region definitions
    
    public class NpyArray : NpyObject_HEAD
    {
        public uint RefCount
        {
            get { return nob_refcnt; }
        }

        public VoidPtr data = new VoidPtr();          /* pointer to raw data buffer */
        public int nd;                /* number of dimensions, also called ndim */
        public npy_intp[] dimensions; /* size in each dimension */
        public npy_intp[] strides;    /*
                                        * bytes to jump to get to the
                                        * next element in each dimension
                                        */

        public NpyArray base_arr;     /* Base when it's specifically an array object */
        internal object base_obj;       /* Base when it's an opaque interface object */

        public NpyArray_Descr descr;  /* Pointer to type structure */
        public NPYARRAYFLAGS flags;   /* Flags describing array -- see below */

        public int ItemSize
        {
            get
            {
                return descr.elsize;
            }
        }
        public NPY_TYPES ItemType
        {
            get
            {
                return descr.type_num;
            }
        }

        public long GetElementCount()
        {
            long totalElements = 1;
            foreach (var dim in this.dimensions)
            {
                totalElements *= dim;
            }
            return totalElements;
        }

        public IEnumerable<long> ViewOffsets
        {
            get
            {
                return GetViewOffsetEnumeration(0, 0);
            }
        }
  
        private IEnumerable<long> GetViewOffsetEnumeration(int dimIdx, long offset)
        {
            if (dimIdx == nd)
            {
                yield return offset / ItemSize;
            }
            else
            {
                for (int i = 0; i < dimensions[dimIdx]; i++)
                {
                    foreach (var offset2 in GetViewOffsetEnumeration(dimIdx + 1, offset + strides[dimIdx] * i))
                    {
                        yield return offset2;
                    }
                }
            }

        }


    }

    internal partial class numpyinternal
    {
        private static void Npy_DECREF(NpyObject_HEAD Head)
        {
            lock (Head)
            {
                Debug.Assert(Head.nob_refcnt > 0);

                if (0 == --Head.nob_refcnt)
                {
                    if (null != Head.nob_interface)
                    {
                        //_NpyInterface_Decref(arr.Head.nob_interface, ref arr.Head.nob_interface); 
                    }
                    else
                    {
                        Head.nob_magic_number = (UInt32)npy_defs.NPY_INVALID_MAGIC;
                    }
                }
            }
            return;
        }
        internal static bool NpyDataType_ISOBJECT(NpyArray_Descr desc)
        {
            return (NpyTypeNum_ISOBJECT(desc.type_num));
        }
        internal static bool NpyDataType_ISSTRING(NpyArray_Descr desc)
        {
            return (NpyTypeNum_ISSTRING(desc.type_num));
        }
        internal static NPY_TYPES NpyDataType_TYPE_NUM(NpyArray_Descr obj)
        {
            return obj.type_num;
        }

 
        private static void Npy_INCREF(NpyObject_HEAD Head)
        {
            lock (Head)
            {
                if ((1 == ++Head.nob_refcnt) && null != Head.nob_interface)
                {
                    //_NpyInterface_Incref(arr.Head.nob_interface, ref arr.Head.nob_interface);
                }
            }
            return;
        }


        internal static void NpyInterface_DECREF(object o)
        {

        }

        internal static void Npy_XINCREF(NpyArray a)
        {
            if (a != null)
            {
                Npy_INCREF(a);
            }
        }

        private static void Npy_XDECREF(NpyArray arr)
        {
            if (arr == null)
                return;
            Npy_DECREF(arr);
        }
        private static void Npy_XDECREF(NpyArray_Descr descr)
        {
            if (descr == null)
                return;
            Npy_DECREF(descr);
        }
        private static void Npy_XDECREF(NpyArrayIterObject iterobject)
        {
            if (iterobject == null)
                return;
            Npy_DECREF(iterobject);
        }

        private static void Npy_XDECREF(NpyArrayMultiIterObject multi)
        {
            if (multi == null)
                return;
            Npy_DECREF(multi);
        }

        private static void NpyArray_XDECREF_ERR(NpyArray obj)
        {
            if (obj != null)
            {
                if ((obj.flags & NPYARRAYFLAGS.NPY_UPDATEIFCOPY) > 0)
                {
                    obj.base_arr.flags |= NPYARRAYFLAGS.NPY_WRITEABLE;
                    obj.flags &= ~NPYARRAYFLAGS.NPY_UPDATEIFCOPY;
                }
            }
        }

        private static VoidPtr NpyInterface_INCREF(VoidPtr ptr)
        {
            return ptr;
        }
        private static VoidPtr NpyInterface_DECREF(VoidPtr ptr)
        {
            return ptr;
        }

        private static void NpyInterface_CLEAR(VoidPtr castbuf)
        {
            return;
        }


        internal static NpyArray_Descr NpyArray_DESCR_REPLACE(ref NpyArray_Descr descr)
        {
            NpyArray_Descr newDescr = NpyArray_DescrNew(descr);
            Npy_XDECREF(descr);
            descr = newDescr;
            return descr;
        }




        internal static bool NpyArray_CHKFLAGS(NpyArray arr, NPYARRAYFLAGS FLAGS)
        {
            return ((arr.flags  & FLAGS) == FLAGS);
        }
        internal static bool NpyArray_ISCONTIGUOUS(NpyArray arr)
        {
            return NpyArray_CHKFLAGS(arr, NPYARRAYFLAGS.NPY_CONTIGUOUS);
        }
        internal static bool NpyArray_ISWRITEABLE(NpyArray arr)
        {
            return NpyArray_CHKFLAGS(arr, NPYARRAYFLAGS.NPY_WRITEABLE);
        }
        internal static bool NpyArray_ISALIGNED(NpyArray arr)
        {
            return NpyArray_CHKFLAGS(arr, NPYARRAYFLAGS.NPY_ALIGNED);
        }

        internal static int NpyArray_NDIM(NpyArray arr)
        {
            return arr.nd;
        }
        private static void NpyArray_NDIM_Update(NpyArray arr, int newnd)
        {
            arr.nd = newnd;
        }

        internal static bool NpyArray_ISONESEGMENT(NpyArray arr)
        {
            bool b =( NpyArray_NDIM(arr) == 0) ||
                      NpyArray_CHKFLAGS(arr, NPYARRAYFLAGS.NPY_CONTIGUOUS) ||
                      NpyArray_CHKFLAGS(arr, NPYARRAYFLAGS.NPY_FORTRAN);

            return b;
        }

        internal static bool NpyArray_ISFORTRAN(NpyArray arr)
        {
            bool b = (NpyArray_NDIM(arr) > 1) &&
                      NpyArray_CHKFLAGS(arr, NPYARRAYFLAGS.NPY_FORTRAN);

            return b;
        }

        internal static int NpyArray_FORTRAN_IF(NpyArray arr)
        {
            return NpyArray_CHKFLAGS(arr, NPYARRAYFLAGS.NPY_FORTRAN) ? (int)NPYARRAYFLAGS.NPY_FORTRAN : 0;
        }

        internal static VoidPtr NpyArray_DATA(NpyArray arr)
        {
            return arr.data;
        }
        internal static VoidPtr NpyArray_BYTES(NpyArray arr)
        {
            return arr.data;
        }
        internal static ulong NpyArray_BYTES_Length(NpyArray arr)
        {
            return VoidPointer_BytesLength(arr.data);
        }
        internal static int NpyArray_Array_Length(NpyArray arr)
        {
            return VoidPointer_Length(arr.data);
        }
        internal static ulong VoidPointer_BytesLength(VoidPtr vp)
        {
            int Length = 0;
            switch (vp.type_num)
            {
                case NPY_TYPES.NPY_BOOL:
                    var dbool = vp.datap as bool[];
                    Length = dbool.Length;
                    break;
                case NPY_TYPES.NPY_BYTE:
                    var dsbyte = vp.datap as sbyte[];
                    Length = dsbyte.Length;
                    break;
                case NPY_TYPES.NPY_UBYTE:
                    var dbyte = vp.datap as byte[];
                    Length = dbyte.Length;
                    break;
                case NPY_TYPES.NPY_UINT16:
                    var duint16 = vp.datap as UInt16[];
                    Length = duint16.Length * sizeof(UInt16);
                    break;
                case NPY_TYPES.NPY_INT16:
                    var dint16 = vp.datap as Int16[];
                    Length = dint16.Length * sizeof(Int16);
                    break;
                case NPY_TYPES.NPY_UINT32:
                    var duint32 = vp.datap as UInt32[];
                    Length = duint32.Length * sizeof(UInt32);
                    break;
                case NPY_TYPES.NPY_INT32:
                    var dint32 = vp.datap as Int32[];
                    Length = dint32.Length * sizeof(UInt32);
                    break;
                case NPY_TYPES.NPY_INT64:
                    var dint64 = vp.datap as Int64[];
                    Length = dint64.Length * sizeof(Int64);
                    break;
                case NPY_TYPES.NPY_UINT64:
                    var duint64 = vp.datap as UInt64[];
                    Length = duint64.Length * sizeof(UInt64);
                    break;
                case NPY_TYPES.NPY_FLOAT:
                    var float1 = vp.datap as float[];
                    Length = float1.Length * sizeof(float);
                    break;
                case NPY_TYPES.NPY_DOUBLE:
                    var double1 = vp.datap as double[];
                    Length = double1.Length * sizeof(double);
                    break;
                case NPY_TYPES.NPY_DECIMAL:
                    var decimal1 = vp.datap as decimal[];
                    Length = decimal1.Length * sizeof(decimal);
                    break;
                default:
                    throw new Exception("Unsupported data type");
            }

            return (ulong)(Length - vp.data_offset);
        }

        internal static int VoidPointer_Length(VoidPtr vp)
        {
            switch (vp.type_num)
            {
                case NPY_TYPES.NPY_BOOL:
                    var dbool = vp.datap as bool[];
                    return dbool.Length;
                case NPY_TYPES.NPY_BYTE:
                    var dsbyte = vp.datap as sbyte[];
                    return dsbyte.Length;
                case NPY_TYPES.NPY_UBYTE:
                    var dbyte = vp.datap as byte[];
                    return dbyte.Length;
                case NPY_TYPES.NPY_UINT16:
                    var duint16 = vp.datap as UInt16[];
                    return duint16.Length;
                case NPY_TYPES.NPY_INT16:
                    var dint16 = vp.datap as Int16[];
                    return dint16.Length;
                case NPY_TYPES.NPY_UINT32:
                    var duint32 = vp.datap as UInt32[];
                    return duint32.Length;
                case NPY_TYPES.NPY_INT32:
                    var dint32 = vp.datap as Int32[];
                    return dint32.Length;
                case NPY_TYPES.NPY_INT64:
                    var dint64 = vp.datap as Int64[];
                    return dint64.Length;
                case NPY_TYPES.NPY_UINT64:
                    var duint64 = vp.datap as UInt64[];
                    return duint64.Length;
                case NPY_TYPES.NPY_FLOAT:
                    var float1 = vp.datap as float[];
                    return float1.Length;
                case NPY_TYPES.NPY_DOUBLE:
                    var double1 = vp.datap as double[];
                    return double1.Length;
                case NPY_TYPES.NPY_DECIMAL:
                    var decimal1 = vp.datap as decimal[];
                    return decimal1.Length;
                default:
                    throw new Exception("Unsupported data type");
            }

        }
        internal static npy_intp[] NpyArray_DIMS(NpyArray arr)
        {
            return arr.dimensions;
        }
        private static void NpyArray_DIMS_Update(NpyArray arr, npy_intp[] newdimensions)
        {
            arr.dimensions = newdimensions;
        }
        internal static npy_intp NpyArray_DIM(NpyArray arr, npy_intp n)
        {
            return arr.dimensions[n];
        }
        private static void NpyArray_DIM_Update(NpyArray arr, int n, npy_intp newsize)
        {
            arr.dimensions[n] = newsize;
        }
        internal static npy_intp[] NpyArray_STRIDES(NpyArray arr)
        {
            return arr.strides;
        }
        private static void NpyArray_STRIDES_Update(NpyArray arr, npy_intp[] newStrides)
        {
            arr.strides = newStrides;
        }

        internal static npy_intp NpyArray_DIMS(NpyArray arr, int n)
        {
            return arr.dimensions[n];
        }
        internal static npy_intp NpyArray_STRIDE(NpyArray arr, int n)
        {
            return arr.strides[n];
        }
        private static void NpyArray_STRIDE_Update(NpyArray arr, int n, npy_intp newsize)
        {
            arr.strides[n] = newsize;
        }
        internal static NpyArray_Descr NpyArray_DESCR(NpyArray arr)
        {
            return arr.descr;
        }
        private static void NpyArray_DESCR_Update(NpyArray arr, NpyArray_Descr newtype)
        {
            arr.descr = newtype;
        }
        internal static NPYARRAYFLAGS NpyArray_FLAGS(NpyArray arr)
        {
            return arr.flags;
        }
        internal static NPYARRAYFLAGS NpyArray_FLAGS_OR(NpyArray arr, NPYARRAYFLAGS flag)
        {
            return arr.flags |= flag;
        }
        internal static int NpyArray_ITEMSIZE( NpyArray arr)
        {
            return arr.descr.elsize;
        }
        internal static int NpyArray_ITEMSIZE(NpyArrayIterObject arr)
        {
            return arr.ao.descr.elsize;
        }

        internal static NPY_TYPES NpyArray_TYPE(NpyArray arr)
        {
            return arr.descr.type_num;
        }

        internal static NpyArray NpyArray_BASE_ARRAY(NpyArray arr)
        {
            return arr.base_arr;
        }
        internal static NpyArray NpyArray_BASE_ARRAY_Update(NpyArray arr, NpyArray newArr)
        {
            return arr.base_arr = newArr;
        }
        internal static object NpyArray_BASE(NpyArray arr)
        {
            return arr.base_obj;
        }

        internal static npy_intp NpyArray_SIZE(NpyArray arr)
        {
            return numpyinternal.NpyArray_MultiplyList(NpyArray_DIMS(arr), NpyArray_NDIM(arr));
        }


        internal static long NpyArray_NBYTES(NpyArray arr)
        {
            return (NpyArray_ITEMSIZE(arr) * NpyArray_SIZE(arr));
        }

        internal static bool NpyArray_SAMESHAPE(NpyArray a1, NpyArray a2)
        {
            return ((NpyArray_NDIM(a1) == NpyArray_NDIM(a2)) &&
                    numpyinternal.NpyArray_CompareLists(NpyArray_DIMS(a1), NpyArray_DIMS(a2), NpyArray_NDIM(a1)));
        }

        internal static bool NpyArray_ISBOOL(NpyArray arr)
        {
            return NpyTypeNum_ISBOOL(NpyArray_TYPE(arr));
        }
        internal static bool NpyArray_ISUNSIGNED(NpyArray arr)
        {
            return NpyTypeNum_ISUNSIGNED(NpyArray_TYPE(arr));
        }
        internal static bool NpyArray_ISSIGNED(NpyArray arr)
        {
            return NpyTypeNum_ISSIGNED(NpyArray_TYPE(arr));
        }
        internal static bool NpyArray_ISINTEGER(NpyArray arr)
        {
            return NpyTypeNum_ISINTEGER(NpyArray_TYPE(arr));
        }
        internal static bool NpyArray_ISFLOAT(NpyArray arr)
        {
            return NpyTypeNum_ISFLOAT(NpyArray_TYPE(arr));
        }
        internal static bool NpyArray_ISNUMBER(NpyArray arr)
        {
            return NpyTypeNum_ISNUMBER(NpyArray_TYPE(arr));
        }
        internal static bool NpyArray_ISSTRING(NpyArray arr)
        {
            return NpyTypeNum_ISSTRING(NpyArray_TYPE(arr));
        }
        internal static bool NpyArray_ISCOMPLEX(NpyArray arr)
        {
            return NpyTypeNum_ISCOMPLEX(NpyArray_TYPE(arr));
        }
        internal static bool NpyArray_ISPYTHON(NpyArray arr)
        {
            return NpyTypeNum_ISPYTHON(NpyArray_TYPE(arr));
        }
        internal static bool NpyArray_ISFLEXIBLE(NpyArray arr)
        {
            return NpyTypeNum_ISFLEXIBLE(NpyArray_TYPE(arr));
        }
        internal static bool NpyArray_ISDATETIME(NpyArray arr)
        {
            return NpyTypeNum_ISDATETIME(NpyArray_TYPE(arr));
        }
        internal static bool NpyArray_ISUSERDEF(NpyArray arr)
        {
            return NpyTypeNum_ISUSERDEF(NpyArray_TYPE(arr));
        }
        internal static bool NpyArray_ISEXTENDED(NpyArray arr)
        {
            return NpyTypeNum_ISEXTENDED(NpyArray_TYPE(arr));
        }
        internal static bool NpyArray_ISOBJECT(NpyArray arr)
        {
            return NpyTypeNum_ISOBJECT(NpyArray_TYPE(arr));
        }
        internal static bool NpyArray_HASFIELDS(NpyArray arr)
        {
            return NpyArray_DESCR(arr).fields != null;
        }
        internal static bool NpyArray_ISVARIABLE(NpyArray arr)
        {
            return NpyTypeNum_ISFLEXIBLE(NpyArray_TYPE(arr));
        }
        internal static bool NpyArray_SAFEALIGNEDCOPY(NpyArray arr)
        {
            return (NpyArray_ISALIGNED(arr) && !NpyArray_ISVARIABLE(arr));
        }
        internal static bool NpyArray_ISNOTSWAPPED(NpyArray arr)
        {
            return NpyArray_ISNBO(arr);
        }
        internal static bool NpyArray_ISBYTESWAPPED(NpyArray arr)
        {
            return !NpyArray_ISNOTSWAPPED(arr);
        }
        internal static bool NpyArray_FLAGSWAP(NpyArray arr, NPYARRAYFLAGS flags)
        {
            return (NpyArray_CHKFLAGS(arr, flags) && NpyArray_ISNOTSWAPPED(arr));
        }
        internal static bool NpyArray_ISCARRAY(NpyArray arr)
        {
            return (NpyArray_FLAGSWAP(arr, NPYARRAYFLAGS.NPY_CARRAY));
        }
        internal static bool NpyArray_ISCARRAY_RO(NpyArray arr)
        {
            return (NpyArray_FLAGSWAP(arr, NPYARRAYFLAGS.NPY_CARRAY_RO));
        }
        internal static bool NpyArray_ISFARRAY(NpyArray arr)
        {
            return (NpyArray_FLAGSWAP(arr, NPYARRAYFLAGS.NPY_FARRAY));
        }
        internal static bool NpyArray_ISFARRAY_RO(NpyArray arr)
        {
            return (NpyArray_FLAGSWAP(arr, NPYARRAYFLAGS.NPY_FARRAY_RO));
        }
        internal static bool NpyArray_ISBEHAVED(NpyArray arr)
        {
            return (NpyArray_FLAGSWAP(arr, NPYARRAYFLAGS.NPY_BEHAVED));
        }
        internal static bool NpyArray_ISBEHAVED_RO(NpyArray arr)
        {
            return (NpyArray_FLAGSWAP(arr, NPYARRAYFLAGS.NPY_ALIGNED));
        }
    }
    #endregion

    internal partial class numpyinternal
    {
        /*
        * Compute the size of an array (in number of items)
        */
        internal static npy_intp NpyArray_Size(NpyArray op)
        {

            Debug.Assert(Validate(op));
            return NpyArray_SIZE(op);
        }

        internal static int NpyArray_CompareUCS4(npy_ucs4[] s1, npy_ucs4[] s2, int len)
        {
            npy_ucs4 c1, c2;

            if (s1.Length != s2.Length)
            {
                NpyErr_SetString(npyexc_type.NpyExc_IndexError, "NpyArray_CompareUCS4: different size arrays being compared");
                return -2;
            }

            int i = 0;
            while (len-- > 0)
            {
                c1 = s1[i];
                c2 = s2[i++];
                
                if (c1 != c2)
                {
                    return (c1 < c2) ? -1 : 1;
                }
            }
            return 0;
        }

        internal static int NpyArray_CompareString(string s1, string s2, int len)
        {
            //if (s1.Equals(s2, StringComparison.CurrentCultureIgnoreCase))
            //    return 0;

            try
            {
                for (int i = 0; i < len; ++i)
                {
                    //if (s1.Length <= i)
                    //    return -1;
                    //if (s2.Length <= 1)
                    //    return 1;

                    if (s1[i] != s2[i])
                    {
                        return (s1[i] > s2[i]) ? 1 : -1;
                    }
                }
            }
            catch (Exception ex)
            {
                NpyErr_SetString(npyexc_type.NpyExc_IndexError, string.Format("NpyArray_CompareString: {0}", ex.Message));
                return -2;
            }

            return 0;
        }

        internal static int NpyArray_ElementStrides(NpyArray arr)
        {
            int itemsize = NpyArray_ITEMSIZE(arr);
            int N = NpyArray_NDIM(arr);
            npy_intp[] strides = NpyArray_STRIDES(arr);

            for (int i = 0; i < N; i++)
            {
                if ((strides[i] % itemsize) != 0)
                {
                    return 0;
                }
            }
            return 1;
        }

        /*
         * This routine checks to see if newstrides (of length nd) will not
         * ever be able to walk outside of the memory implied numbytes and offset.
         *
         * The available memory is assumed to start at -offset and proceed
         * to numbytes-offset.  The strides are checked to ensure
         * that accessing memory using striding will not try to reach beyond
         * this memory for any of the axes.
         *
         * If numbytes is 0 it will be calculated using the dimensions and
         * element-size.
         *
         * This function checks for walking beyond the beginning and right-end
         * of the buffer and therefore works for any integer stride (positive
         * or negative).
         */
        internal static bool NpyArray_CheckStrides(int elsize, int nd, npy_intp numbytes, npy_intp offset, npy_intp[] dims, npy_intp[] newstrides)
        {
            int i;
            npy_intp byte_begin;
            npy_intp begin;
            npy_intp end;

            if (numbytes == 0)
            {
                numbytes = (npy_intp)(NpyArray_MultiplyList(dims, nd) * elsize);
            }
            begin = (npy_intp)(-offset);
            end = (npy_intp)(numbytes - offset - elsize);
            for (i = 0; i < nd; i++)
            {
                byte_begin = newstrides[i] * (dims[i] - 1);
                if ((byte_begin < begin) || (byte_begin > end))
                {
                    return false;
                }
            }
            return true;
        }


        internal static void NpyArray_ForceUpdate(NpyArray self)
        {
            if (((self.flags & NPYARRAYFLAGS.NPY_UPDATEIFCOPY) != 0) && (self.base_arr != null))
            {
                /*
                 * UPDATEIFCOPY means that base points to an
                 * array that should be updated with the contents
                 * of this array upon destruction.
                 * self.base.flags must have been WRITEABLE
                 * (checked previously) and it was locked here
                 * thus, unlock it.
                 */
                if ((self.flags & NPYARRAYFLAGS.NPY_UPDATEIFCOPY) != 0)
                {
                    self.flags &= ~NPYARRAYFLAGS.NPY_UPDATEIFCOPY;
                    self.base_arr.flags |= NPYARRAYFLAGS.NPY_WRITEABLE;
                    Npy_INCREF(self); /* hold on to self in next call */
                    if (NpyArray_CopyAnyInto(self.base_arr, self) < 0)
                    {
                        /* NpyErr_Print(); */
                        NpyErr_Clear();
                    }
                    Npy_DECREF(self);
                    Npy_DECREF(self.base_arr);
                    self.base_arr = null;
                }
            }
        }

        internal static NpyArray NpyArray_CompareStringArrays(NpyArray a1, NpyArray a2, int cmp_op, int rstrip)
        {
            NpyArray result;
            int val;
            NpyArrayMultiIterObject mit;

            NPY_TYPES t1 = NpyArray_TYPE(a1);
            NPY_TYPES t2 = NpyArray_TYPE(a2);

            if (NpyArray_TYPE(a1) != NpyArray_TYPE(a2) ||
                (t1 != NPY_TYPES.NPY_UNICODE && t1 != NPY_TYPES.NPY_STRING && t1 != NPY_TYPES.NPY_VOID))
            {
                NpyErr_SetString(npyexc_type.NpyExc_ValueError, "Arrays must be of the same string type.");
                return null;
            }

            /* Broad-cast the arrays to a common shape */
            mit = NpyArray_MultiIterFromArrays(null, 0, 2, a1, a2);
            if (mit == null)
            {
                return null;
            }

            result = NpyArray_NewFromDescr(NpyArray_DescrFromType(NPY_TYPES.NPY_BOOL),
                                           mit.nd,
                                           mit.dimensions, null, 
                                           null, 0, true, null, null);
            if (result == null)
            {
                goto finish;
            }

            if (t1 == NPY_TYPES.NPY_UNICODE)
            {
                val = _compare_strings(result, mit, cmp_op, _myunincmp, rstrip);
            }
            else
            {
                val = _compare_strings(result, mit, cmp_op, _mystrncmp, rstrip);
            }

            if (val < 0)
            {
                Npy_DECREF(result);
                result = null;
            }

            finish:
            Npy_DECREF(mit);
            return result;
        }

        delegate int myArrayCompareFunc(NpyArray_UCS4[] s1, NpyArray_UCS4[] s2, int len1, int len2);
        delegate int MyStringCompareFunc(string s1, string s2, int len1, int len2);

        static int _compare_strings(NpyArray result, NpyArrayMultiIterObject multi, int cmp_op, MyStringCompareFunc func, int rstrip)
        {
            return 0;
        }

        static int _compare_strings(NpyArray result, NpyArrayMultiIterObject multi, int cmp_op, myArrayCompareFunc func, int rstrip)
        {
            //NpyArrayIterObject iself,iother;
            //bool []dptr;
            //npy_intp size;
            //int val;
            //int N1, N2;
            //int (*cmpfunc)(void*, void*, int, int);
            //void (*relfunc)(char *, int);
            //char* (*stripfunc)(char *, char *, int);

            //cmpfunc = func;
            //dptr = (npy_bool*)NpyArray_DATA(result);
            //iself = multi.iters[0];
            //iother = multi.iters[1];
            //size = multi.size;
            //N1 = iself.ao.descr.elsize;
            //N2 = iother.ao.descr.elsize;
            //if ((void*)cmpfunc == (void*)_myunincmp)
            //{
            //    N1 >>= 2;
            //    N2 >>= 2;
            //    stripfunc = _uni_copy_n_strip;
            //    relfunc = _uni_release;
            //}
            //else
            //{
            //    stripfunc = _char_copy_n_strip;
            //    relfunc = _char_release;
            //}
            //switch (cmp_op)
            //{
            //    case NPY_EQ:
            //        _loop(==)
            //        break;
            //    case NPY_NE:
            //        _loop(!=)
            //        break;
            //    case NPY_LT:
            //        _loop(<)
            //        break;
            //    case NPY_LE:
            //        _loop(<=)
            //        break;
            //    case NPY_GT:
            //        _loop(>)
            //        break;
            //    case NPY_GE:
            //        _loop(>=)
            //        break;
            //    default:
            //        NpyErr_SetString(npyexc_type.NpyExc_RuntimeError, "bad comparison operator");
            //        return -1;
            //}
            return 0;
        }

        /* This also handles possibly mis-aligned data */
        /* Compare s1 and s2 which are not necessarily null-terminated.
           s1 is of length len1
           s2 is of length len2
           If they are null terminated, then stop comparison.
        */
        static int _myunincmp(NpyArray_UCS4[] s1, NpyArray_UCS4[] s2, int len1, int len2)
        {
            NpyArray_UCS4[] sptr;
            NpyArray_UCS4[] s1t = s1;
            NpyArray_UCS4[] s2t = s2;
            int val;
            npy_intp size;
            int diff;
            int index;

            if (false)
            {
                size = (npy_intp)(len1 * sizeof(NpyArray_UCS4));
                s1t = new npy_intp[size];
                memcpy(s1, s1t, size);
            }
            if (false)
            {
                size = (npy_intp)(len2 * sizeof(NpyArray_UCS4));
                s2t = new npy_intp[size];
                memcpy(s2, s2t, size);
            }
            val = NpyArray_CompareUCS4(s1t, s2t, Math.Min(len1, len2));
            if ((val != 0) || (len1 == len2))
            {
                goto finish;
            }
            if (len2 > len1)
            {
                sptr = s2t;
                index = len1;
                val = -1;
                diff = len2 - len1;
            }
            else
            {
                sptr = s1t;
                index = len2;
                val = 1;
                diff = len1 - len2;
            }
            while (diff-- > 0)
            {
                if (sptr[index] != 0)
                {
                    goto finish;
                }
                index++;
            }
            val = 0;

            finish:
  
            return val;
        }

        /*
         * Compare s1 and s2 which are not necessarily null-terminated.
         * s1 is of length len1
         * s2 is of length len2
         * If they are null terminated, then stop comparison.
         */
        static int _mystrncmp(string s1, string s2, int len1, int len2)
        {
             return string.Compare(s1, s2);
        }


        static string _rstripw(string s, int n)
        {
            return s.TrimEnd();
        }

        static string _unistripw(string s, int n)
        {
            return s.TrimEnd();
        }

        /* Deallocs & destroy's the array object.
         *  Returns whether or not we did an artificial incref
         *  so we can keep track of the total refcount for debugging.
         */
        /* TODO: For now caller is expected to call _array_dealloc_buffer_info
                 and clear weak refs.  Need to revisit. */
        internal static int NpyArray_dealloc(NpyArray self)
        {
            int i;

            int result = 0;
            Debug.Assert(Validate(self));
            Debug.Assert(ValidateBaseArray(self));
   

            if (null != self.base_arr)
            {
                /*
                 * UPDATEIFCOPY means that base points to an
                 * array that should be updated with the contents
                 * of this array upon destruction.
                 * self.base.flags must have been WRITEABLE
                 * (checked previously) and it was locked here
                 * thus, unlock it.
                 */
                if ((self.flags & NPYARRAYFLAGS.NPY_UPDATEIFCOPY) > 0)
                {
                    self.base_arr.flags |= NPYARRAYFLAGS.NPY_WRITEABLE;
                    Npy_INCREF(self); /* hold on to self in next call */
                    if (NpyArray_CopyAnyInto(self.base_arr, self) < 0)
                    {
                        /* NpyErr_Print(); */
                        NpyErr_Clear();
                    }
                    /*
                     * Don't need to DECREF -- because we are deleting
                     *self already...
                     */
                    result = 1;
                }
                /*
                 * In any case base is pointing to something that we need
                 * to DECREF -- either a view or a buffer object
                 */
                Npy_DECREF(self.base_arr);
                self.base_arr = null;
            }
            else if (null != self.base_obj)
            {
                NpyInterface_DECREF(self.base_obj);
                self.base_obj = null;
            }

            if ((self.flags & NPYARRAYFLAGS.NPY_OWNDATA) > 0 && self.data != null)
            {
                /* Free internal references if an Object array */
                if (NpyDataType_FLAGCHK(self.descr, NpyArray_Descr_Flags.NPY_ITEM_REFCOUNT))
                {
                    Npy_INCREF(self); /* hold on to self in next call */
                    NpyArray_XDECREF(self);
                    /*
                     * Don't need to DECREF -- because we are deleting
                     * self already...
                     */
                    if (self.nob_refcnt == 1)
                    {
                        result = 1;
                    }
                }

                NpyDataMem_FREE(self.data);
                self.data = null;
            }

            if (null != self.dimensions)
            {
                NpyDimMem_FREE(self.dimensions);
            }

            Npy_DECREF(self.descr);
            /* Flag that this object is now deallocated. */
            self.nob_magic_number = npy_defs.NPY_INVALID_MAGIC;

            NpyArray_free(self);

            return result;
        }


    }





}