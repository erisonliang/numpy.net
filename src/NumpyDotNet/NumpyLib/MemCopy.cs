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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if NPY_INTP_64
using npy_intp = System.Int64;
#else
using npy_intp = System.Int32;
#endif

namespace NumpyLib
{
    public class MemCopy
    {
        // todo: take these out when performance improvements complete
        public static long MemCpy_TotalCount = 0;
        public static long MemCpy_ShortBufferCount = 0;
        public static long MemCpy_MediumBufferCount = 0;
        public static long MemCpy_LargeBufferCount = 0;
        public static long MemCpy_SameTypeCount = 0;
        public static long MemCpy_DifferentTypeCount = 0;


        #region First Level Routing
        public static bool MemCpy(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            DestOffset += Dest.data_offset;
            SrcOffset += Src.data_offset;

            MemCpy_TotalCount++;

            if (Dest.type_num == Src.type_num)
            {
                MemCpy_SameTypeCount++;
            }
            else
            {
                MemCpy_DifferentTypeCount++;
            }
            if (totalBytesToCopy <=4)
            {
                MemCpy_ShortBufferCount++;
            }
            else if (totalBytesToCopy <= 8)
            {
                MemCpy_MediumBufferCount++;
            }
            else
            {
                MemCpy_LargeBufferCount++;
            }
                

            switch (Dest.type_num)
            {
                case NPY_TYPES.NPY_BOOL:
                    return MemCpyToBools(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_BYTE:
                    return MemCpyToBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UBYTE:
                    return MemCpyToUBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT16:
                    return MemCpyToInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT16:
                    return MemCpyToUInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT32:
                    return MemCpyToInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT32:
                    return MemCpyToUInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT64:
                    return MemCpyToInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT64:
                    return MemCpyToUInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_FLOAT:
                    return MemCpyToFloats(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DOUBLE:
                    return MemCpyToDoubles(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DECIMAL:
                    return MemCpyToDecimals(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
            }
            return false;
        }
        #endregion

        #region Second level routing
        private static bool MemCpyToBools(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            switch (Src.type_num)
            {
                case NPY_TYPES.NPY_BOOL:
                    return MemCpyBoolsToBools(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_BYTE:
                    return MemCpyBytesToBools(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UBYTE:
                    return MemCpyUBytesToBools(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT16:
                    return MemCpyInt16ToBools(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT16:
                    return MemCpyUInt16ToBools(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT32:
                    return MemCpyInt32ToBools(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT32:
                    return MemCpyUInt32ToBools(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT64:
                    return MemCpyInt64ToBools(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT64:
                    return MemCpyUInt64ToBools(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_FLOAT:
                    return MemCpyFloatsToBools(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DOUBLE:
                    return MemCpyDoublesToBools(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DECIMAL:
                    return MemCpyDecimalsToBools(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
            }
            return false;
        }


        private static bool MemCpyToBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            switch (Src.type_num)
            {
                case NPY_TYPES.NPY_BOOL:
                    return MemCpyBoolsToBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_BYTE:
                    return MemCpyBytesToBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UBYTE:
                    return MemCpyUBytesToBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT16:
                    return MemCpyInt16ToBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT16:
                    return MemCpyUInt16ToBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT32:
                    return MemCpyInt32ToBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT32:
                    return MemCpyUInt32ToBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT64:
                    return MemCpyInt64ToBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT64:
                    return MemCpyUInt64ToBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_FLOAT:
                    return MemCpyFloatsToBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DOUBLE:
                    return MemCpyDoublesToBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DECIMAL:
                    return MemCpyDecimalsToBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
            }
            return false;
        }
        private static bool MemCpyToUBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            switch (Src.type_num)
            {
                case NPY_TYPES.NPY_BOOL:
                    return MemCpyBoolsToUBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_BYTE:
                    return MemCpyBytesToUBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UBYTE:
                    return MemCpyUBytesToUBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT16:
                    return MemCpyInt16ToUBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT16:
                    return MemCpyUInt16ToUBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT32:
                    return MemCpyInt32ToUBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT32:
                    return MemCpyUInt32ToUBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT64:
                    return MemCpyInt64ToUBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT64:
                    return MemCpyUInt64ToUBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_FLOAT:
                    return MemCpyFloatsToUBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DOUBLE:
                    return MemCpyDoublesToUBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DECIMAL:
                    return MemCpyDecimalsToUBytes(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
            }
            return false;
        }

        private static bool MemCpyToInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            switch (Src.type_num)
            {
                case NPY_TYPES.NPY_BOOL:
                    return MemCpyBoolsToInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_BYTE:
                    return MemCpyBytesToInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UBYTE:
                    return MemCpyUBytesToInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT16:
                    return MemCpyInt16ToInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT16:
                    return MemCpyUInt16ToInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT32:
                    return MemCpyInt32ToInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT32:
                    return MemCpyUInt32ToInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT64:
                    return MemCpyInt64ToInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT64:
                    return MemCpyUInt64ToInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_FLOAT:
                    return MemCpyFloatsToInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DOUBLE:
                    return MemCpyDoublesToInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DECIMAL:
                    return MemCpyDecimalsToInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
            }
            return false;
        }

        private static bool MemCpyToUInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            switch (Src.type_num)
            {
                case NPY_TYPES.NPY_BOOL:
                    return MemCpyBoolsToUInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_BYTE:
                    return MemCpyBytesToUInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UBYTE:
                    return MemCpyUBytesToUInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT16:
                    return MemCpyInt16ToUInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT16:
                    return MemCpyUInt16ToUInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT32:
                    return MemCpyInt32ToUInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT32:
                    return MemCpyUInt32ToUInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT64:
                    return MemCpyInt64ToUInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT64:
                    return MemCpyUInt64ToUInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_FLOAT:
                    return MemCpyFloatsToUInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DOUBLE:
                    return MemCpyDoublesToUInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DECIMAL:
                    return MemCpyDecimalsToUInt16s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
            }
            return false;
        }

        private static bool MemCpyToInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            switch (Src.type_num)
            {
                case NPY_TYPES.NPY_BOOL:
                    return MemCpyBoolsToInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_BYTE:
                    return MemCpyBytesToInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UBYTE:
                    return MemCpyUBytesToInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT16:
                    return MemCpyInt16ToInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT16:
                    return MemCpyUInt16ToInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT32:
                    return MemCpyInt32ToInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT32:
                    return MemCpyUInt32ToInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT64:
                    return MemCpyInt64ToInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT64:
                    return MemCpyUInt64ToInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_FLOAT:
                    return MemCpyFloatsToInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DOUBLE:
                    return MemCpyDoublesToInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DECIMAL:
                    return MemCpyDecimalsToInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
            }
            return false;
        }

        private static bool MemCpyToUInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            switch (Src.type_num)
            {
                case NPY_TYPES.NPY_BOOL:
                    return MemCpyBoolsToUInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_BYTE:
                    return MemCpyBytesToUInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UBYTE:
                    return MemCpyUBytesToUInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT16:
                    return MemCpyInt16ToUInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT16:
                    return MemCpyUInt16ToUInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT32:
                    return MemCpyInt32ToUInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT32:
                    return MemCpyUInt32ToUInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT64:
                    return MemCpyInt64ToUInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT64:
                    return MemCpyUInt64ToUInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_FLOAT:
                    return MemCpyFloatsToUInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DOUBLE:
                    return MemCpyDoublesToUInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DECIMAL:
                    return MemCpyDecimalsToUInt32s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
            }
            return false;
        }

        private static bool MemCpyToInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            switch (Src.type_num)
            {
                case NPY_TYPES.NPY_BOOL:
                    return MemCpyBoolsToInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_BYTE:
                    return MemCpyBytesToInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UBYTE:
                    return MemCpyUBytesToInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT16:
                    return MemCpyInt16ToInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT16:
                    return MemCpyUInt16ToInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT32:
                    return MemCpyInt32ToInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT32:
                    return MemCpyUInt32ToInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT64:
                    return MemCpyInt64ToInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT64:
                    return MemCpyUInt64ToInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_FLOAT:
                    return MemCpyFloatsToInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DOUBLE:
                    return MemCpyDoublesToInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DECIMAL:
                    return MemCpyDecimalsToInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
            }
            return false;
        }

        private static bool MemCpyToUInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            switch (Src.type_num)
            {
                case NPY_TYPES.NPY_BOOL:
                    return MemCpyBoolsToUInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_BYTE:
                    return MemCpyBytesToUInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UBYTE:
                    return MemCpyUBytesToUInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT16:
                    return MemCpyInt16ToUInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT16:
                    return MemCpyUInt16ToUInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT32:
                    return MemCpyInt32ToUInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT32:
                    return MemCpyUInt32ToUInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT64:
                    return MemCpyInt64ToUInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT64:
                    return MemCpyUInt64ToUInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_FLOAT:
                    return MemCpyFloatsToUInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DOUBLE:
                    return MemCpyDoublesToUInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DECIMAL:
                    return MemCpyDecimalsToUInt64s(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
            }
            return false;
        }

        private static bool MemCpyToFloats(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            switch (Src.type_num)
            {
                case NPY_TYPES.NPY_BOOL:
                    return MemCpyBoolsToFloats(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_BYTE:
                    return MemCpyBytesToFloats(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UBYTE:
                    return MemCpyUBytesToFloats(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT16:
                    return MemCpyInt16ToFloats(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT16:
                    return MemCpyUInt16ToFloats(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT32:
                    return MemCpyInt32ToFloats(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT32:
                    return MemCpyUInt32ToFloats(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT64:
                    return MemCpyInt64ToFloats(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT64:
                    return MemCpyUInt64ToFloats(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_FLOAT:
                    return MemCpyFloatsToFloats(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DOUBLE:
                    return MemCpyDoublesToFloats(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DECIMAL:
                    return MemCpyDecimalsToFloats(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
            }
            return false;
        }

        private static bool MemCpyToDoubles(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            switch (Src.type_num)
            {
                case NPY_TYPES.NPY_BOOL:
                    return MemCpyBoolsToDoubles(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_BYTE:
                    return MemCpyBytesToDoubles(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UBYTE:
                    return MemCpyUBytesToDoubles(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT16:
                    return MemCpyInt16ToDoubles(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT16:
                    return MemCpyUInt16ToDoubles(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT32:
                    return MemCpyInt32ToDoubles(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT32:
                    return MemCpyUInt32ToDoubles(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT64:
                    return MemCpyInt64ToDoubles(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT64:
                    return MemCpyUInt64ToDoubles(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_FLOAT:
                    return MemCpyFloatsToDoubles(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DOUBLE:
                    return MemCpyDoublesToDoubles(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DECIMAL:
                    return MemCpyDecimalsToDoubles(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);

            }
            return false;
        }

        private static bool MemCpyToDecimals(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            switch (Src.type_num)
            {
                case NPY_TYPES.NPY_BOOL:
                    return MemCpyBoolsToDecimals(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_BYTE:
                    return MemCpyBytesToDecimals(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UBYTE:
                    return MemCpyUBytesToDecimals(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT16:
                    return MemCpyInt16ToDecimals(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT16:
                    return MemCpyUInt16ToDecimals(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT32:
                    return MemCpyInt32ToDecimals(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT32:
                    return MemCpyUInt32ToDecimals(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_INT64:
                    return MemCpyInt64ToDecimals(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_UINT64:
                    return MemCpyUInt64ToDecimals(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_FLOAT:
                    return MemCpyFloatsToDecimals(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DOUBLE:
                    return MemCpyDoublesToDecimals(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);
                case NPY_TYPES.NPY_DECIMAL:
                    return MemCpyDecimalsToDecimals(Dest, DestOffset, Src, SrcOffset, totalBytesToCopy);

            }
            return false;
        }
        #endregion

        #region Common functions
        private static void CommonArrayCopy<T>(T[] destArray, npy_intp DestOffset, T[] sourceArray, npy_intp SrcOffset, long totalBytesToCopy, npy_intp DestOffsetAdjustment, npy_intp SrcOffsetAdjustment, npy_intp ItemSize)
        {
            for (npy_intp i = 0; i < DestOffsetAdjustment; i++)
            {
                byte data = MemoryAccess.GetByteT(sourceArray, i + SrcOffset);
                MemoryAccess.SetByteT(destArray, i + DestOffset, data);
            }

            totalBytesToCopy -= DestOffsetAdjustment;
            DestOffset += DestOffsetAdjustment;
            SrcOffset += SrcOffsetAdjustment;

            npy_intp LengthAdjustment = (npy_intp)(totalBytesToCopy % ItemSize);
            totalBytesToCopy -= LengthAdjustment;

            Array.Copy(sourceArray, SrcOffset / ItemSize, destArray, DestOffset / ItemSize, totalBytesToCopy / ItemSize);

            for (npy_intp i = (npy_intp)totalBytesToCopy; i < totalBytesToCopy + LengthAdjustment; i++)
            {
                byte data = MemoryAccess.GetByteT(sourceArray, i + SrcOffset);
                MemoryAccess.SetByteT(destArray, i + DestOffset, data);
            }

            return;
        }
        #endregion

        #region boolean specific
        private static bool MemCpyBoolsToBools(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {

            bool[] sourceArray = Src.datap as bool[];
            bool[] destArray = Dest.datap as bool[];

            Array.Copy(sourceArray, SrcOffset, destArray, DestOffset, totalBytesToCopy);

            return true;
        }
        private static bool MemCpyBytesToBools(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            sbyte[] sourceArray = Src.datap as sbyte[];
            bool[] destArray = Dest.datap as bool[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                sbyte data = sourceArray[i + SrcOffset];
                bool bdata = (bool)(data != 0 ? true : false);
                destArray[i + DestOffset] = bdata;
            }

            return true;
        }
        private static bool MemCpyUBytesToBools(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            byte[] sourceArray = Src.datap as byte[];
            bool[] destArray = Dest.datap as bool[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = sourceArray[i + SrcOffset];
                bool bdata = (bool)(data != 0 ? true : false);
                destArray[i + DestOffset] = bdata;
            }

            return true;
        }
        private static bool MemCpyInt16ToBools(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int16[] sourceArray = Src.datap as Int16[];
            bool[] destArray = Dest.datap as bool[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                bool bdata = (bool)(data != 0 ? true : false);
                destArray[i + DestOffset] =  bdata;
            }
            return true;
        }


        private static bool MemCpyUInt16ToBools(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {

            UInt16[] sourceArray = Src.datap as UInt16[];
            bool[] destArray = Dest.datap as bool[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                bool bdata = (bool)(data != 0 ? true : false);
                destArray[i + DestOffset] = bdata;
            }
            return true;
        }
        private static bool MemCpyInt32ToBools(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {

            Int32[] sourceArray = Src.datap as Int32[];
            bool[] destArray = Dest.datap as bool[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                bool bdata = (bool)(data != 0 ? true : false);
                destArray[i + DestOffset] = bdata;
            }
            return true;
        }
        private static bool MemCpyUInt32ToBools(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt32[] sourceArray = Src.datap as UInt32[];
            bool[] destArray = Dest.datap as bool[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                bool bdata = (bool)(data != 0 ? true : false);
                destArray[i + DestOffset] = bdata;
            }
            return true;
        }
        private static bool MemCpyInt64ToBools(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int64[] sourceArray = Src.datap as Int64[];
            bool[] destArray = Dest.datap as bool[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                bool bdata = (bool)(data != 0 ? true : false);
                destArray[i + DestOffset] = bdata;
            }
            return true;
        }
        private static bool MemCpyUInt64ToBools(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt64[] sourceArray = Src.datap as UInt64[];
            bool[] destArray = Dest.datap as bool[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                bool bdata = (bool)(data != 0 ? true : false);
                destArray[i + DestOffset] = bdata;
            }
            return true;
        }
        private static bool MemCpyFloatsToBools(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            float[] sourceArray = Src.datap as float[];
            bool[] destArray = Dest.datap as bool[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                bool bdata = (bool)(data != 0 ? true : false);
                destArray[i + DestOffset] = bdata;
            }
            return true;
        }
        private static bool MemCpyDoublesToBools(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            double[] sourceArray = Src.datap as double[];
            bool[] destArray = Dest.datap as bool[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                bool bdata = (bool)(data != 0 ? true : false);
                destArray[i + DestOffset] = bdata;
            }
            return true;
        }
        private static bool MemCpyDecimalsToBools(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            decimal[] sourceArray = Src.datap as decimal[];
            bool[] destArray = Dest.datap as bool[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                bool bdata = (bool)(data != 0 ? true : false);
                destArray[i + DestOffset] = bdata;
            }
            return true;
        }
        #endregion

        #region byte specific
        private static bool MemCpyBoolsToBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            bool[] sourceArray = Src.datap as bool[];
            sbyte[] destArray = Dest.datap as sbyte[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                bool bdata = sourceArray[i + SrcOffset];
                sbyte data = (sbyte)(bdata == true ? 1 : 0);
                destArray[i + DestOffset] = data;
            }

            return true;
        }
        private static bool MemCpyBytesToBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            sbyte[] sourceArray = Src.datap as sbyte[];
            sbyte[] destArray = Dest.datap as sbyte[];

            Array.Copy(sourceArray, SrcOffset, destArray, DestOffset, totalBytesToCopy);
 
            return true;
        }
        private static bool MemCpyUBytesToBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            byte[] sourceArray = Src.datap as byte[];
            sbyte[] destArray = Dest.datap as sbyte[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                sbyte data = (sbyte)MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                destArray[i + DestOffset] = data;
            }

            return true;
        }
        private static bool MemCpyInt16ToBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int16[] sourceArray = Src.datap as Int16[];
            sbyte[] destArray = Dest.datap as sbyte[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                sbyte data = (sbyte)MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                destArray[i + DestOffset] = data;
            }
            return true;
        }
        private static bool MemCpyUInt16ToBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt16[] sourceArray = Src.datap as UInt16[];
            sbyte[] destArray = Dest.datap as sbyte[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                sbyte data = (sbyte)MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                destArray[i + DestOffset] = data;
            }
            return true;
        }
        private static bool MemCpyInt32ToBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int32[] sourceArray = Src.datap as Int32[];
            sbyte[] destArray = Dest.datap as sbyte[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                sbyte data = (sbyte)MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                destArray[i + DestOffset] = data;
            }
            return true;
        }
        private static bool MemCpyUInt32ToBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt32[] sourceArray = Src.datap as UInt32[];
            sbyte[] destArray = Dest.datap as sbyte[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                sbyte data = (sbyte)MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                destArray[i + DestOffset] = data;
            }
            return true;
        }
        private static bool MemCpyInt64ToBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int64[] sourceArray = Src.datap as Int64[];
            sbyte[] destArray = Dest.datap as sbyte[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                sbyte data = (sbyte)MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                destArray[i + DestOffset] = data;
            }
            return true;
        }
        private static bool MemCpyUInt64ToBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt64[] sourceArray = Src.datap as UInt64[];
            sbyte[] destArray = Dest.datap as sbyte[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                sbyte data = (sbyte)MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                destArray[i + DestOffset] = data;
            }
            return true;
        }
        private static bool MemCpyFloatsToBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            float[] sourceArray = Src.datap as float[];
            sbyte[] destArray = Dest.datap as sbyte[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                sbyte data = (sbyte)MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                destArray[i + DestOffset] = data;
            }
            return true;
        }
        private static bool MemCpyDoublesToBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            double[] sourceArray = Src.datap as double[];
            sbyte[] destArray = Dest.datap as sbyte[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                sbyte data = (sbyte)MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                destArray[i + DestOffset] = data;
            }
            return true;
        }

        private static bool MemCpyDecimalsToBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            decimal[] sourceArray = Src.datap as decimal[];
            sbyte[] destArray = Dest.datap as sbyte[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                sbyte data = (sbyte)MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                destArray[i + DestOffset] = data;
            }
            return true;
        }
        #endregion

        #region ubyte specific
        private static bool MemCpyBoolsToUBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            bool[] sourceArray = Src.datap as bool[];
            byte[] destArray = Dest.datap as byte[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                bool bdata = sourceArray[i + SrcOffset];
                byte data = (byte)(bdata == true ? 1 : 0);
                destArray[i + DestOffset] = data;
            }

            return true;
        }
        private static bool MemCpyBytesToUBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            sbyte[] sourceArray = Src.datap as sbyte[];
            byte[] destArray = Dest.datap as byte[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = (byte)MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                destArray[i + DestOffset] = data;
            }
            return true;
        }
        private static bool MemCpyUBytesToUBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            byte[] sourceArray = Src.datap as byte[];
            byte[] destArray = Dest.datap as byte[];

            Array.Copy(sourceArray, SrcOffset, destArray, DestOffset, totalBytesToCopy);

            return true;
        }
        private static bool MemCpyInt16ToUBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int16[] sourceArray = Src.datap as Int16[];
            byte[] destArray = Dest.datap as byte[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                destArray[i + DestOffset] = data;
            }
            return true;
        }
        private static bool MemCpyUInt16ToUBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt16[] sourceArray = Src.datap as UInt16[];
            byte[] destArray = Dest.datap as byte[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                destArray[i + DestOffset] = data;
            }
            return true;
        }
        private static bool MemCpyInt32ToUBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int32[] sourceArray = Src.datap as Int32[];
            byte[] destArray = Dest.datap as byte[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                destArray[i + DestOffset] = data;
            }
            return true;
        }
        private static bool MemCpyUInt32ToUBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt32[] sourceArray = Src.datap as UInt32[];
            byte[] destArray = Dest.datap as byte[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                destArray[i + DestOffset] = data;
            }
            return true;
        }
        private static bool MemCpyInt64ToUBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int64[] sourceArray = Src.datap as Int64[];
            byte[] destArray = Dest.datap as byte[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                destArray[i + DestOffset] = data;
            }
            return true;
        }
        private static bool MemCpyUInt64ToUBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt64[] sourceArray = Src.datap as UInt64[];
            byte[] destArray = Dest.datap as byte[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                destArray[i + DestOffset] = data;
            }
            return true;
        }
        private static bool MemCpyFloatsToUBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            float[] sourceArray = Src.datap as float[];
            byte[] destArray = Dest.datap as byte[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                destArray[i + DestOffset] = data;
            }
            return true;
        }
        private static bool MemCpyDoublesToUBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            double[] sourceArray = Src.datap as double[];
            byte[] destArray = Dest.datap as byte[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                destArray[i + DestOffset] = data;
            }
            return true;
        }
        private static bool MemCpyDecimalsToUBytes(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            decimal[] sourceArray = Src.datap as decimal[];
            byte[] destArray = Dest.datap as byte[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                destArray[i + DestOffset] = data;
            }
            return true;
        }
        #endregion

        #region Int16 specific
        private static bool MemCpyBoolsToInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            bool[] sourceArray = Src.datap as bool[];
            Int16[] destArray = Dest.datap as Int16[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                bool bdata = sourceArray[i + SrcOffset];
                byte data = (byte)(bdata == true ? 1 : 0);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }

            return true;
        }

        private static bool MemCpyBytesToInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            sbyte[] sourceArray = Src.datap as sbyte[];
            Int16[] destArray = Dest.datap as Int16[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                sbyte data = sourceArray[i + SrcOffset];
                MemoryAccess.SetByte(destArray, i + DestOffset, (byte)data);
            }

            return true;
        }
        private static bool MemCpyUBytesToInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            byte[] sourceArray = Src.datap as byte[];
            Int16[] destArray = Dest.datap as Int16[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = sourceArray[i + SrcOffset];
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }

            return true;
        }
        private static bool MemCpyInt16ToInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int16[] sourceArray = Src.datap as Int16[];
            Int16[] destArray = Dest.datap as Int16[];
            npy_intp ItemSize = sizeof(Int16);

            npy_intp DestOffsetAdjustment = DestOffset % ItemSize;
            npy_intp SrcOffsetAdjustment = SrcOffset % ItemSize;
 

            if (DestOffsetAdjustment == SrcOffsetAdjustment)
            {
                CommonArrayCopy(destArray, DestOffset, sourceArray, SrcOffset, totalBytesToCopy, DestOffsetAdjustment, SrcOffsetAdjustment, ItemSize);
            }
            else
            {
                for (npy_intp i = 0; i < totalBytesToCopy; i++)
                {
                    byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                    MemoryAccess.SetByte(destArray, i + DestOffset, data);
                }
            }
 
            return true;
        }


        private static bool MemCpyUInt16ToInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt16[] sourceArray = Src.datap as UInt16[];
            Int16[] destArray = Dest.datap as Int16[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt32ToInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int32[] sourceArray = Src.datap as Int32[];
            Int16[] destArray = Dest.datap as Int16[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }


        private static bool MemCpyUInt32ToInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt32[] sourceArray = Src.datap as UInt32[];
            Int16[] destArray = Dest.datap as Int16[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt64ToInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int64[] sourceArray = Src.datap as Int64[];
            Int16[] destArray = Dest.datap as Int16[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt64ToInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt64[] sourceArray = Src.datap as UInt64[];
            Int16[] destArray = Dest.datap as Int16[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyFloatsToInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            float[] sourceArray = Src.datap as float[];
            Int16[] destArray = Dest.datap as Int16[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyDoublesToInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            double[] sourceArray = Src.datap as double[];
            Int16[] destArray = Dest.datap as Int16[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }

        private static bool MemCpyDecimalsToInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            decimal[] sourceArray = Src.datap as decimal[];
            Int16[] destArray = Dest.datap as Int16[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        #endregion

        #region UInt16 specific
        private static bool MemCpyBoolsToUInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            bool[] sourceArray = Src.datap as bool[];
            UInt16[] destArray = Dest.datap as UInt16[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                bool bdata = sourceArray[i + SrcOffset];
                byte data = (byte)(bdata == true ? 1 : 0);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyBytesToUInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            sbyte[] sourceArray = Src.datap as sbyte[];
            UInt16[] destArray = Dest.datap as UInt16[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                sbyte data = sourceArray[i + SrcOffset];
                MemoryAccess.SetByte(destArray, i + DestOffset, (byte)data);
            }
            return true;
        }
        private static bool MemCpyUBytesToUInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            byte[] sourceArray = Src.datap as byte[];
            UInt16[] destArray = Dest.datap as UInt16[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = sourceArray[i + SrcOffset];
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt16ToUInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int16[] sourceArray = Src.datap as Int16[];
            UInt16[] destArray = Dest.datap as UInt16[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt16ToUInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt16[] sourceArray = Src.datap as UInt16[];
            UInt16[] destArray = Dest.datap as UInt16[];
            npy_intp ItemSize = sizeof(UInt16);

            npy_intp DestOffsetAdjustment = DestOffset % ItemSize;
            npy_intp SrcOffsetAdjustment = SrcOffset % ItemSize;


            if (DestOffsetAdjustment == SrcOffsetAdjustment)
            {
                CommonArrayCopy(destArray, DestOffset, sourceArray, SrcOffset, totalBytesToCopy, DestOffsetAdjustment, SrcOffsetAdjustment, ItemSize);
            }
            else
            {
                for (npy_intp i = 0; i < totalBytesToCopy; i++)
                {
                    byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                    MemoryAccess.SetByte(destArray, i + DestOffset, data);
                }
            }

            return true;
        }
        private static bool MemCpyInt32ToUInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int32[] sourceArray = Src.datap as Int32[];
            UInt16[] destArray = Dest.datap as UInt16[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt32ToUInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt32[] sourceArray = Src.datap as UInt32[];
            UInt16[] destArray = Dest.datap as UInt16[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt64ToUInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int64[] sourceArray = Src.datap as Int64[];
            UInt16[] destArray = Dest.datap as UInt16[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt64ToUInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt64[] sourceArray = Src.datap as UInt64[];
            UInt16[] destArray = Dest.datap as UInt16[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyFloatsToUInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            float[] sourceArray = Src.datap as float[];
            UInt16[] destArray = Dest.datap as UInt16[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyDoublesToUInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            double[] sourceArray = Src.datap as double[];
            UInt16[] destArray = Dest.datap as UInt16[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyDecimalsToUInt16s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            decimal[] sourceArray = Src.datap as decimal[];
            UInt16[] destArray = Dest.datap as UInt16[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        #endregion

        #region Int32 specific
        private static bool MemCpyBoolsToInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            bool[] sourceArray = Src.datap as bool[];
            Int32[] destArray = Dest.datap as Int32[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                bool bdata = sourceArray[i + SrcOffset];
                byte data = (byte)(bdata == true ? 1 : 0);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;

        }
        private static bool MemCpyBytesToInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            sbyte[] sourceArray = Src.datap as sbyte[];
            Int32[] destArray = Dest.datap as Int32[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                sbyte data = sourceArray[i + SrcOffset];
                MemoryAccess.SetByte(destArray, i + DestOffset, (byte)data);
            }
            return true;
        }
        private static bool MemCpyUBytesToInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            byte[] sourceArray = Src.datap as byte[];
            Int32[] destArray = Dest.datap as Int32[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = sourceArray[i + SrcOffset];
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt16ToInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int16[] sourceArray = Src.datap as Int16[];
            Int32[] destArray = Dest.datap as Int32[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt16ToInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt16[] sourceArray = Src.datap as UInt16[];
            Int32[] destArray = Dest.datap as Int32[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt32ToInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int32[] sourceArray = Src.datap as Int32[];
            Int32[] destArray = Dest.datap as Int32[];
            npy_intp ItemSize = sizeof(Int32);

            npy_intp DestOffsetAdjustment = DestOffset % ItemSize;
            npy_intp SrcOffsetAdjustment = SrcOffset % ItemSize;


            if (DestOffsetAdjustment == SrcOffsetAdjustment)
            {
                CommonArrayCopy(destArray, DestOffset, sourceArray, SrcOffset, totalBytesToCopy, DestOffsetAdjustment, SrcOffsetAdjustment, ItemSize);
            }
            else
            {
                for (npy_intp i = 0; i < totalBytesToCopy; i++)
                {
                    byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                    MemoryAccess.SetByte(destArray, i + DestOffset, data);
                }
            }
            return true;
        }
        private static bool MemCpyUInt32ToInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt32[] sourceArray = Src.datap as UInt32[];
            Int32[] destArray = Dest.datap as Int32[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt64ToInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int64[] sourceArray = Src.datap as Int64[];
            Int32[] destArray = Dest.datap as Int32[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt64ToInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt64[] sourceArray = Src.datap as UInt64[];
            Int32[] destArray = Dest.datap as Int32[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyFloatsToInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            float[] sourceArray = Src.datap as float[];
            Int32[] destArray = Dest.datap as Int32[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyDoublesToInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            double[] sourceArray = Src.datap as double[];
            Int32[] destArray = Dest.datap as Int32[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyDecimalsToInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            decimal[] sourceArray = Src.datap as decimal[];
            Int32[] destArray = Dest.datap as Int32[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        #endregion

        #region UInt32 specific
        private static bool MemCpyBoolsToUInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            bool[] sourceArray = Src.datap as bool[];
            UInt32[] destArray = Dest.datap as UInt32[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                bool bdata = sourceArray[i + SrcOffset];
                byte data = (byte)(bdata == true ? 1 : 0);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyBytesToUInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            sbyte[] sourceArray = Src.datap as sbyte[];
            UInt32[] destArray = Dest.datap as UInt32[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                sbyte data = sourceArray[i + SrcOffset];
                MemoryAccess.SetByte(destArray, i + DestOffset, (byte)data);
            }
            return true;
        }
        private static bool MemCpyUBytesToUInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            byte[] sourceArray = Src.datap as byte[];
            UInt32[] destArray = Dest.datap as UInt32[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = sourceArray[i + SrcOffset];
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt16ToUInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int16[] sourceArray = Src.datap as Int16[];
            UInt32[] destArray = Dest.datap as UInt32[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt16ToUInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt16[] sourceArray = Src.datap as UInt16[];
            UInt32[] destArray = Dest.datap as UInt32[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt32ToUInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int32[] sourceArray = Src.datap as Int32[];
            UInt32[] destArray = Dest.datap as UInt32[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt32ToUInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt32[] sourceArray = Src.datap as UInt32[];
            UInt32[] destArray = Dest.datap as UInt32[];
            npy_intp ItemSize = sizeof(UInt32);

            npy_intp DestOffsetAdjustment = DestOffset % ItemSize;
            npy_intp SrcOffsetAdjustment = SrcOffset % ItemSize;


            if (DestOffsetAdjustment == SrcOffsetAdjustment)
            {
                CommonArrayCopy(destArray, DestOffset, sourceArray, SrcOffset, totalBytesToCopy, DestOffsetAdjustment, SrcOffsetAdjustment, ItemSize);
            }
            else
            {
                for (npy_intp i = 0; i < totalBytesToCopy; i++)
                {
                    byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                    MemoryAccess.SetByte(destArray, i + DestOffset, data);
                }
            }
            return true;
        }
        private static bool MemCpyInt64ToUInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int64[] sourceArray = Src.datap as Int64[];
            UInt32[] destArray = Dest.datap as UInt32[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt64ToUInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt64[] sourceArray = Src.datap as UInt64[];
            UInt32[] destArray = Dest.datap as UInt32[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyFloatsToUInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            float[] sourceArray = Src.datap as float[];
            UInt32[] destArray = Dest.datap as UInt32[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyDoublesToUInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            double[] sourceArray = Src.datap as double[];
            UInt32[] destArray = Dest.datap as UInt32[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyDecimalsToUInt32s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            decimal[] sourceArray = Src.datap as decimal[];
            UInt32[] destArray = Dest.datap as UInt32[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        #endregion

        #region Int64 specific
        private static bool MemCpyBoolsToInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            bool[] sourceArray = Src.datap as bool[];
            Int64[] destArray = Dest.datap as Int64[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                bool bdata = sourceArray[i + SrcOffset];
                byte data = (byte)(bdata == true ? 1 : 0);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyBytesToInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            sbyte[] sourceArray = Src.datap as sbyte[];
            Int64[] destArray = Dest.datap as Int64[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                sbyte data = sourceArray[i + SrcOffset];
                MemoryAccess.SetByte(destArray, i + DestOffset, (byte)data);
            }
            return true;
        }
        private static bool MemCpyUBytesToInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            byte[] sourceArray = Src.datap as byte[];
            Int64[] destArray = Dest.datap as Int64[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = sourceArray[i + SrcOffset];
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt16ToInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int16[] sourceArray = Src.datap as Int16[];
            Int64[] destArray = Dest.datap as Int64[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt16ToInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt16[] sourceArray = Src.datap as UInt16[];
            Int64[] destArray = Dest.datap as Int64[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt32ToInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int32[] sourceArray = Src.datap as Int32[];
            Int64[] destArray = Dest.datap as Int64[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt32ToInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt32[] sourceArray = Src.datap as UInt32[];
            Int64[] destArray = Dest.datap as Int64[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt64ToInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int64[] sourceArray = Src.datap as Int64[];
            Int64[] destArray = Dest.datap as Int64[];
            npy_intp ItemSize = sizeof(Int64);

            npy_intp DestOffsetAdjustment = DestOffset % ItemSize;
            npy_intp SrcOffsetAdjustment = SrcOffset % ItemSize;


            if (DestOffsetAdjustment == SrcOffsetAdjustment)
            {
                CommonArrayCopy(destArray, DestOffset, sourceArray, SrcOffset, totalBytesToCopy, DestOffsetAdjustment, SrcOffsetAdjustment, ItemSize);
            }
            else
            {
                for (npy_intp i = 0; i < totalBytesToCopy; i++)
                {
                    byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                    MemoryAccess.SetByte(destArray, i + DestOffset, data);
                }
            }
            return true;
        }
        private static bool MemCpyUInt64ToInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt64[] sourceArray = Src.datap as UInt64[];
            Int64[] destArray = Dest.datap as Int64[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyFloatsToInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            float[] sourceArray = Src.datap as float[];
            Int64[] destArray = Dest.datap as Int64[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyDoublesToInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            double[] sourceArray = Src.datap as double[];
            Int64[] destArray = Dest.datap as Int64[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyDecimalsToInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            decimal[] sourceArray = Src.datap as decimal[];
            Int64[] destArray = Dest.datap as Int64[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        #endregion

        #region UInt64 specific
        private static bool MemCpyBoolsToUInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            bool[] sourceArray = Src.datap as bool[];
            UInt64[] destArray = Dest.datap as UInt64[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                bool bdata = sourceArray[i + SrcOffset];
                byte data = (byte)(bdata == true ? 1 : 0);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyBytesToUInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            sbyte[] sourceArray = Src.datap as sbyte[];
            UInt64[] destArray = Dest.datap as UInt64[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                sbyte data = sourceArray[i + SrcOffset];
                MemoryAccess.SetByte(destArray, i + DestOffset, (byte)data);
            }
            return true;
        }
        private static bool MemCpyUBytesToUInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            byte[] sourceArray = Src.datap as byte[];
            UInt64[] destArray = Dest.datap as UInt64[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = sourceArray[i + SrcOffset];
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt16ToUInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int16[] sourceArray = Src.datap as Int16[];
            UInt64[] destArray = Dest.datap as UInt64[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt16ToUInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt16[] sourceArray = Src.datap as UInt16[];
            UInt64[] destArray = Dest.datap as UInt64[];

            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt32ToUInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int32[] sourceArray = Src.datap as Int32[];
            UInt64[] destArray = Dest.datap as UInt64[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt32ToUInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt32[] sourceArray = Src.datap as UInt32[];
            UInt64[] destArray = Dest.datap as UInt64[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt64ToUInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int64[] sourceArray = Src.datap as Int64[];
            UInt64[] destArray = Dest.datap as UInt64[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt64ToUInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt64[] sourceArray = Src.datap as UInt64[];
            UInt64[] destArray = Dest.datap as UInt64[];
            npy_intp ItemSize = sizeof(UInt64);

            npy_intp DestOffsetAdjustment = DestOffset % ItemSize;
            npy_intp SrcOffsetAdjustment = SrcOffset % ItemSize;


            if (DestOffsetAdjustment == SrcOffsetAdjustment)
            {
                CommonArrayCopy(destArray, DestOffset, sourceArray, SrcOffset, totalBytesToCopy, DestOffsetAdjustment, SrcOffsetAdjustment, ItemSize);
            }
            else
            {
                for (npy_intp i = 0; i < totalBytesToCopy; i++)
                {
                    byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                    MemoryAccess.SetByte(destArray, i + DestOffset, data);
                }
            }
    
            return true;
        }
        private static bool MemCpyFloatsToUInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            float[] sourceArray = Src.datap as float[];
            UInt64[] destArray = Dest.datap as UInt64[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyDoublesToUInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            double[] sourceArray = Src.datap as double[];
            UInt64[] destArray = Dest.datap as UInt64[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }

        private static bool MemCpyDecimalsToUInt64s(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            decimal[] sourceArray = Src.datap as decimal[];
            UInt64[] destArray = Dest.datap as UInt64[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        #endregion

        #region Float specific
        private static bool MemCpyBoolsToFloats(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            bool[] sourceArray = Src.datap as bool[];
            float[] destArray = Dest.datap as float[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                bool bdata = sourceArray[i + SrcOffset];
                byte data = (byte)(bdata == true ? 1 : 0);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyBytesToFloats(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            sbyte[] sourceArray = Src.datap as sbyte[];
            float[] destArray = Dest.datap as float[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                sbyte data = sourceArray[i + SrcOffset];
                MemoryAccess.SetByte(destArray, i + DestOffset, (byte)data);
            }
            return true;
        }
        private static bool MemCpyUBytesToFloats(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            byte[] sourceArray = Src.datap as byte[];
            float[] destArray = Dest.datap as float[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = sourceArray[i + SrcOffset];
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt16ToFloats(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int16[] sourceArray = Src.datap as Int16[];
            float[] destArray = Dest.datap as float[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt16ToFloats(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt16[] sourceArray = Src.datap as UInt16[];
            float[] destArray = Dest.datap as float[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt32ToFloats(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int32[] sourceArray = Src.datap as Int32[];
            float[] destArray = Dest.datap as float[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt32ToFloats(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt32[] sourceArray = Src.datap as UInt32[];
            float[] destArray = Dest.datap as float[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt64ToFloats(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int64[] sourceArray = Src.datap as Int64[];
            float[] destArray = Dest.datap as float[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt64ToFloats(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt64[] sourceArray = Src.datap as UInt64[];
            float[] destArray = Dest.datap as float[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyFloatsToFloats(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            float[] sourceArray = Src.datap as float[];
            float[] destArray = Dest.datap as float[];
            npy_intp ItemSize = sizeof(float);

            npy_intp DestOffsetAdjustment = DestOffset % ItemSize;
            npy_intp SrcOffsetAdjustment = SrcOffset % ItemSize;


            if (DestOffsetAdjustment == SrcOffsetAdjustment)
            {
                CommonArrayCopy(destArray, DestOffset, sourceArray, SrcOffset, totalBytesToCopy, DestOffsetAdjustment, SrcOffsetAdjustment, ItemSize);
            }
            else
            {
                for (npy_intp i = 0; i < totalBytesToCopy; i++)
                {
                    byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                    MemoryAccess.SetByte(destArray, i + DestOffset, data);
                }
            }
            return true;
        }
        private static bool MemCpyDoublesToFloats(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            double[] sourceArray = Src.datap as double[];
            float[] destArray = Dest.datap as float[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }

        private static bool MemCpyDecimalsToFloats(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            decimal[] sourceArray = Src.datap as decimal[];
            float[] destArray = Dest.datap as float[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        #endregion

        #region Double specific
        private static bool MemCpyBoolsToDoubles(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            bool[] sourceArray = Src.datap as bool[];
            double[] destArray = Dest.datap as double[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                bool bdata = sourceArray[i + SrcOffset];
                byte data = (byte)(bdata == true ? 1 : 0);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyBytesToDoubles(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            sbyte[] sourceArray = Src.datap as sbyte[];
            double[] destArray = Dest.datap as double[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                sbyte data = sourceArray[i + SrcOffset];
                MemoryAccess.SetByte(destArray, i + DestOffset, (byte)data);
            }
            return true;
        }
        private static bool MemCpyUBytesToDoubles(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            byte[] sourceArray = Src.datap as byte[];
            double[] destArray = Dest.datap as double[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = sourceArray[i + SrcOffset];
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt16ToDoubles(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int16[] sourceArray = Src.datap as Int16[];
            double[] destArray = Dest.datap as double[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt16ToDoubles(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt16[] sourceArray = Src.datap as UInt16[];
            double[] destArray = Dest.datap as double[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt32ToDoubles(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int32[] sourceArray = Src.datap as Int32[];
            double[] destArray = Dest.datap as double[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt32ToDoubles(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt32[] sourceArray = Src.datap as UInt32[];
            double[] destArray = Dest.datap as double[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt64ToDoubles(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int64[] sourceArray = Src.datap as Int64[];
            double[] destArray = Dest.datap as double[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt64ToDoubles(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt64[] sourceArray = Src.datap as UInt64[];
            double[] destArray = Dest.datap as double[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyFloatsToDoubles(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            float[] sourceArray = Src.datap as float[];
            double[] destArray = Dest.datap as double[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyDoublesToDoubles(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            double[] sourceArray = Src.datap as double[];
            double[] destArray = Dest.datap as double[];
            npy_intp ItemSize = sizeof(double);

            npy_intp DestOffsetAdjustment = DestOffset % ItemSize;
            npy_intp SrcOffsetAdjustment = SrcOffset % ItemSize;


            if (DestOffsetAdjustment == SrcOffsetAdjustment)
            {
                CommonArrayCopy(destArray, DestOffset, sourceArray, SrcOffset, totalBytesToCopy, DestOffsetAdjustment, SrcOffsetAdjustment, ItemSize);
            }
            else
            {
                for (npy_intp i = 0; i < totalBytesToCopy; i++)
                {
                    byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                    MemoryAccess.SetByte(destArray, i + DestOffset, data);
                }
            }
            return true;
        }

        private static bool MemCpyDecimalsToDoubles(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            decimal[] sourceArray = Src.datap as decimal[];
            double[] destArray = Dest.datap as double[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        #endregion

        #region Decimal specific
        private static bool MemCpyBoolsToDecimals(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            bool[] sourceArray = Src.datap as bool[];
            decimal[] destArray = Dest.datap as decimal[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                bool bdata = sourceArray[i + SrcOffset];
                byte data = (byte)(bdata == true ? 1 : 0);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyBytesToDecimals(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            sbyte[] sourceArray = Src.datap as sbyte[];
            decimal[] destArray = Dest.datap as decimal[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                sbyte data = sourceArray[i + SrcOffset];
                MemoryAccess.SetByte(destArray, i + DestOffset, (byte)data);
            }
            return true;
        }
        private static bool MemCpyUBytesToDecimals(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            byte[] sourceArray = Src.datap as byte[];
            decimal[] destArray = Dest.datap as decimal[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = sourceArray[i + SrcOffset];
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt16ToDecimals(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int16[] sourceArray = Src.datap as Int16[];
            decimal[] destArray = Dest.datap as decimal[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt16ToDecimals(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt16[] sourceArray = Src.datap as UInt16[];
            decimal[] destArray = Dest.datap as decimal[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt32ToDecimals(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int32[] sourceArray = Src.datap as Int32[];
            decimal[] destArray = Dest.datap as decimal[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt32ToDecimals(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt32[] sourceArray = Src.datap as UInt32[];
            decimal[] destArray = Dest.datap as decimal[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyInt64ToDecimals(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            Int64[] sourceArray = Src.datap as Int64[];
            decimal[] destArray = Dest.datap as decimal[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyUInt64ToDecimals(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            UInt64[] sourceArray = Src.datap as UInt64[];
            decimal[] destArray = Dest.datap as decimal[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyFloatsToDecimals(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            float[] sourceArray = Src.datap as float[];
            decimal[] destArray = Dest.datap as decimal[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }
        private static bool MemCpyDoublesToDecimals(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            decimal[] sourceArray = Src.datap as decimal[];
            decimal[] destArray = Dest.datap as decimal[];


            for (npy_intp i = 0; i < totalBytesToCopy; i++)
            {
                byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                MemoryAccess.SetByte(destArray, i + DestOffset, data);
            }
            return true;
        }

        private static bool MemCpyDecimalsToDecimals(VoidPtr Dest, npy_intp DestOffset, VoidPtr Src, npy_intp SrcOffset, long totalBytesToCopy)
        {
            decimal[] sourceArray = Src.datap as decimal[];
            decimal[] destArray = Dest.datap as decimal[];
            npy_intp ItemSize = sizeof(decimal);

            npy_intp DestOffsetAdjustment = DestOffset % ItemSize;
            npy_intp SrcOffsetAdjustment = SrcOffset % ItemSize;


            if (DestOffsetAdjustment == SrcOffsetAdjustment)
            {
                CommonArrayCopy(destArray, DestOffset, sourceArray, SrcOffset, totalBytesToCopy, DestOffsetAdjustment, SrcOffsetAdjustment, ItemSize);
            }
            else
            {
                for (npy_intp i = 0; i < totalBytesToCopy; i++)
                {
                    byte data = MemoryAccess.GetByte(sourceArray, i + SrcOffset);
                    MemoryAccess.SetByte(destArray, i + DestOffset, data);
                }
            }
            return true;
        }
        #endregion

    }

    class MemSet
    {

        public static void memset(VoidPtr Dest, npy_intp DestOffset, byte setValue, long Length)
        {
            switch (Dest.type_num)
            {
                case NPY_TYPES.NPY_BOOL:
                    MemSetToBools(Dest, DestOffset, setValue, Length);
                    break;
                case NPY_TYPES.NPY_BYTE:
                    MemSetToBytes(Dest, DestOffset, setValue, Length);
                    break;
                case NPY_TYPES.NPY_UBYTE:
                    MemSetToUBytes(Dest, DestOffset, setValue, Length);
                    break;
                case NPY_TYPES.NPY_INT16:
                    MemSetToInt16s(Dest, DestOffset, setValue, Length);
                    break;
                case NPY_TYPES.NPY_UINT16:
                    MemSetToUInt16s(Dest, DestOffset, setValue, Length);
                    break;
                case NPY_TYPES.NPY_INT32:
                    MemSetToInt32s(Dest, DestOffset, setValue, Length);
                    break;
                case NPY_TYPES.NPY_UINT32:
                    MemSetToUInt32s(Dest, DestOffset, setValue, Length);
                    break;
                case NPY_TYPES.NPY_INT64:
                    MemSetToInt64s(Dest, DestOffset, setValue, Length);
                    break;
                case NPY_TYPES.NPY_UINT64:
                    MemSetToUInt64s(Dest, DestOffset, setValue, Length);
                    break;
                case NPY_TYPES.NPY_FLOAT:
                    MemSetToFloats(Dest, DestOffset, setValue, Length);
                    break;
                case NPY_TYPES.NPY_DOUBLE:
                    MemSetToDoubles(Dest, DestOffset, setValue, Length);
                    break;
                case NPY_TYPES.NPY_DECIMAL:
                    MemSetToDecimals(Dest, DestOffset, setValue, Length);
                    break;

            }
            return;
        }
        private static void MemSetToBools(VoidPtr dest, npy_intp destOffset, byte setValue, long length)
        {
            bool[] destArray = dest.datap as bool[];

            for (npy_intp i = 0; i < length; i++)
            {
                destArray[i + destOffset] = setValue != 0 ? true : false;
            }
        }

        private static void MemSetToBytes(VoidPtr dest, npy_intp destOffset, byte setValue, long length)
        {
            sbyte[] destArray = dest.datap as sbyte[];

            for (npy_intp i = 0; i < length; i++)
            {
                destArray[i + destOffset] = (sbyte)setValue;
            }
        }
        private static void MemSetToUBytes(VoidPtr dest, npy_intp destOffset, byte setValue, long length)
        {
            byte[] destArray = dest.datap as byte[];

            for (npy_intp i = 0; i < length; i++)
            {
                destArray[i + destOffset] = setValue;
            }
        }
        private static void MemSetToInt16s(VoidPtr dest, npy_intp destOffset, byte setValue, long length)
        {
            Int16[] destArray = dest.datap as Int16[];

            for (npy_intp i = 0; i < length; i++)
            {
                MemoryAccess.SetByte(destArray, i + destOffset, setValue);
            }
        }
        private static void MemSetToUInt16s(VoidPtr dest, npy_intp destOffset, byte setValue, long length)
        {
            UInt16[] destArray = dest.datap as UInt16[];

            for (npy_intp i = 0; i < length; i++)
            {
                MemoryAccess.SetByte(destArray, i + destOffset, setValue);
            }
        }

        private static void MemSetToInt32s(VoidPtr dest, npy_intp destOffset, byte setValue, long length)
        {
            Int32[] destArray = dest.datap as Int32[];

            for (npy_intp i = 0; i < length; i++)
            {
                MemoryAccess.SetByte(destArray, i + destOffset, setValue);
            }
        }
        private static void MemSetToUInt32s(VoidPtr dest, npy_intp destOffset, byte setValue, long length)
        {
            UInt32[] destArray = dest.datap as UInt32[];

            for (npy_intp i = 0; i < length; i++)
            {
                MemoryAccess.SetByte(destArray, i + destOffset, setValue);
            }
        }
        private static void MemSetToInt64s(VoidPtr dest, npy_intp destOffset, byte setValue, long length)
        {
            Int64[] destArray = dest.datap as Int64[];

            for (npy_intp i = 0; i < length; i++)
            {
                MemoryAccess.SetByte(destArray, i + destOffset, setValue);
            }
        }
        private static void MemSetToUInt64s(VoidPtr dest, npy_intp destOffset, byte setValue, long length)
        {
            UInt64[] destArray = dest.datap as UInt64[];

            for (npy_intp i = 0; i < length; i++)
            {
                MemoryAccess.SetByte(destArray, i + destOffset, setValue);
            }
        }
        private static void MemSetToFloats(VoidPtr dest, npy_intp destOffset, byte setValue, long length)
        {
            float[] destArray = dest.datap as float[];

            for (npy_intp i = 0; i < length; i++)
            {
                MemoryAccess.SetByte(destArray, i + destOffset, setValue);
            }
        }
        private static void MemSetToDoubles(VoidPtr dest, npy_intp destOffset, byte setValue, long length)
        {
            double[] destArray = dest.datap as double[];

            for (npy_intp i = 0; i < length; i++)
            {
                MemoryAccess.SetByte(destArray, i + destOffset, setValue);
            }
        }

        private static void MemSetToDecimals(VoidPtr dest, npy_intp destOffset, byte setValue, long length)
        {
            decimal[] destArray = dest.datap as decimal[];

            for (npy_intp i = 0; i < length; i++)
            {
                MemoryAccess.SetByte(destArray, i + destOffset, setValue);
            }
        }

    }
}