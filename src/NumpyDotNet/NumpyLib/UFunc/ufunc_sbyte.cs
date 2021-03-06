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
using static NumpyLib.numpyinternal;
#if NPY_INTP_64
using npy_intp = System.Int64;
#else
using npy_intp = System.Int32;
#endif

namespace NumpyLib
{

    #region UFUNC SBYTE

    internal class UFUNC_SByte : UFUNC_BASE<sbyte>, IUFUNC_Operations
    {
        public UFUNC_SByte() : base(sizeof(sbyte))
        {

        }

        protected override sbyte ConvertToTemplate(object value)
        {
            return Convert.ToSByte(value);
        }

        protected override sbyte PerformUFuncOperation(UFuncOperation op, sbyte aValue, sbyte bValue)
        {
            sbyte destValue = 0;
            bool boolValue = false;

            switch (op)
            {
                case UFuncOperation.add:
                    destValue = Add(aValue, bValue);
                    break;
                case UFuncOperation.subtract:
                    destValue = Subtract(aValue, bValue);
                    break;
                case UFuncOperation.multiply:
                    destValue = Multiply(aValue, bValue);
                    break;
                case UFuncOperation.divide:
                    destValue = Divide(aValue, bValue);
                    break;
                case UFuncOperation.remainder:
                    destValue = Remainder(aValue, bValue);
                    break;
                case UFuncOperation.fmod:
                    destValue = FMod(aValue, bValue);
                    break;
                case UFuncOperation.power:
                    destValue = Power(aValue, bValue);
                    break;
                case UFuncOperation.square:
                    destValue = Square(aValue, bValue);
                    break;
                case UFuncOperation.reciprocal:
                    destValue = Reciprocal(aValue, bValue);
                    break;
                case UFuncOperation.ones_like:
                    destValue = OnesLike(aValue, bValue);
                    break;
                case UFuncOperation.sqrt:
                    destValue = Sqrt(aValue, bValue);
                    break;
                case UFuncOperation.negative:
                    destValue = Negative(aValue, bValue);
                    break;
                case UFuncOperation.absolute:
                    destValue = Absolute(aValue, bValue);
                    break;
                case UFuncOperation.invert:
                    destValue = Invert(aValue, bValue);
                    break;
                case UFuncOperation.left_shift:
                    destValue = LeftShift(aValue, bValue);
                    break;
                case UFuncOperation.right_shift:
                    destValue = RightShift(aValue, bValue);
                    break;
                case UFuncOperation.bitwise_and:
                    destValue = BitWiseAnd(aValue, bValue);
                    break;
                case UFuncOperation.bitwise_xor:
                    destValue = BitWiseXor(aValue, bValue);
                    break;
                case UFuncOperation.bitwise_or:
                    destValue = BitWiseOr(aValue, bValue);
                    break;
                case UFuncOperation.less:
                    boolValue = Less(aValue, bValue);
                    destValue = (sbyte)(boolValue ? 1 : 0);
                    break;
                case UFuncOperation.less_equal:
                    boolValue = LessEqual(aValue, bValue);
                    destValue = (sbyte)(boolValue ? 1 : 0);
                    break;
                case UFuncOperation.equal:
                    boolValue = Equal(aValue, bValue);
                    destValue = (sbyte)(boolValue ? 1 : 0);
                    break;
                case UFuncOperation.not_equal:
                    boolValue = NotEqual(aValue, bValue);
                    destValue = (sbyte)(boolValue ? 1 : 0);
                    break;
                case UFuncOperation.greater:
                    boolValue = Greater(aValue, bValue);
                    destValue = (sbyte)(boolValue ? 1 : 0);
                    break;
                case UFuncOperation.greater_equal:
                    boolValue = GreaterEqual(aValue, bValue);
                    destValue = (sbyte)(boolValue ? 1 : 0);
                    break;
                case UFuncOperation.floor_divide:
                    destValue = FloorDivide(aValue, bValue);
                    break;
                case UFuncOperation.true_divide:
                    destValue = TrueDivide(aValue, bValue);
                    break;
                case UFuncOperation.logical_or:
                    boolValue = LogicalOr(aValue, bValue);
                    destValue = (sbyte)(boolValue ? 1 : 0);
                    break;
                case UFuncOperation.logical_and:
                    boolValue = LogicalAnd(aValue, bValue);
                    destValue = (sbyte)(boolValue ? 1 : 0);
                    break;
                case UFuncOperation.floor:
                    destValue = Floor(aValue, bValue);
                    break;
                case UFuncOperation.ceil:
                    destValue = Ceiling(aValue, bValue);
                    break;
                case UFuncOperation.maximum:
                    destValue = Maximum(aValue, bValue);
                    break;
                case UFuncOperation.minimum:
                    destValue = Minimum(aValue, bValue);
                    break;
                case UFuncOperation.rint:
                    destValue = Rint(aValue, bValue);
                    break;
                case UFuncOperation.conjugate:
                    destValue = Conjugate(aValue, bValue);
                    break;
                case UFuncOperation.isnan:
                    boolValue = IsNAN(aValue, bValue);
                    destValue = (sbyte)(boolValue ? 1 : 0);
                    break;
                case UFuncOperation.fmax:
                    destValue = FMax(aValue, bValue);
                    break;
                case UFuncOperation.fmin:
                    destValue = FMin(aValue, bValue);
                    break;
                case UFuncOperation.heaviside:
                    destValue = Heaviside(aValue, bValue);
                    break;
                default:
                    destValue = 0;
                    break;

            }

            return destValue;
        }

        #region sbyte specific operation handlers
        protected override sbyte Add(sbyte aValue, sbyte bValue)
        {
            return (sbyte)(aValue + bValue);
        }

        protected override sbyte Subtract(sbyte aValue, sbyte bValue)
        {
            return (sbyte)(aValue - bValue);
        }
        protected override sbyte Multiply(sbyte aValue, sbyte bValue)
        {
            return (sbyte)(aValue * bValue);
        }

        protected override sbyte Divide(sbyte aValue, sbyte bValue)
        {
            if (bValue == 0)
                return 0;
            return (sbyte)(aValue / bValue);
        }
        private sbyte Remainder(sbyte aValue, sbyte bValue)
        {
            if (bValue == 0)
            {
                return 0;
            }
            var rem = aValue % bValue;
            if ((aValue > 0) == (bValue > 0) || rem == 0)
            {
                return (sbyte)(rem);
            }
            else
            {
                return (sbyte)(rem + bValue);
            }
        }
        private sbyte FMod(sbyte aValue, sbyte bValue)
        {
            if (bValue == 0)
                return 0;
            return (sbyte)(aValue % bValue);
        }
        protected override sbyte Power(sbyte aValue, sbyte bValue)
        {
            return Convert.ToSByte(Math.Pow(aValue, bValue));
        }
        private sbyte Square(sbyte bValue, sbyte operand)
        {
            return (sbyte)(bValue * bValue);
        }
        private sbyte Reciprocal(sbyte bValue, sbyte operand)
        {
            if (bValue == 0)
                return 0;

            return (sbyte)(1 / bValue);
        }
        private sbyte OnesLike(sbyte bValue, sbyte operand)
        {
            return 1;
        }
        private sbyte Sqrt(sbyte bValue, sbyte operand)
        {
            return Convert.ToSByte(Math.Sqrt(bValue));
        }
        private sbyte Negative(sbyte bValue, sbyte operand)
        {
            return (sbyte)(-bValue);
        }
        private sbyte Absolute(sbyte bValue, sbyte operand)
        {
            return Convert.ToSByte(Math.Abs(bValue));
        }
        private sbyte Invert(sbyte bValue, sbyte operand)
        {
            return (sbyte)(~bValue);
        }
        private sbyte LeftShift(sbyte bValue, sbyte operand)
        {
            return (sbyte)(bValue << Convert.ToInt32(operand));
        }
        private sbyte RightShift(sbyte bValue, sbyte operand)
        {
            return (sbyte)(bValue >> Convert.ToInt32(operand));
        }
        private sbyte BitWiseAnd(sbyte bValue, sbyte operand)
        {
            return (sbyte)(bValue & operand);
        }
        private sbyte BitWiseXor(sbyte bValue, sbyte operand)
        {
            return (sbyte)(bValue ^ operand);
        }
        private sbyte BitWiseOr(sbyte bValue, sbyte operand)
        {
            return (sbyte)(bValue | operand);
        }
        private bool Less(sbyte bValue, sbyte operand)
        {
            return bValue < operand;
        }
        private bool LessEqual(sbyte bValue, sbyte operand)
        {
            return bValue <= operand;
        }
        private bool Equal(sbyte bValue, sbyte operand)
        {
            return bValue == operand;
        }
        private bool NotEqual(sbyte bValue, sbyte operand)
        {
            return bValue != operand;
        }
        private bool Greater(sbyte bValue, sbyte operand)
        {
            return bValue > operand;
        }
        private bool GreaterEqual(sbyte bValue, sbyte operand)
        {
            return bValue >= (dynamic)operand;
        }
        private sbyte FloorDivide(sbyte bValue, sbyte operand)
        {
            if (operand == 0)
            {
                bValue = 0;
                return bValue;
            }
            return Convert.ToSByte(Math.Floor(Convert.ToDouble(bValue) / Convert.ToDouble(operand)));
        }
        private sbyte TrueDivide(sbyte bValue, sbyte operand)
        {
            if (operand == 0)
                return 0;

            return (sbyte)(bValue / operand);
        }
        private bool LogicalOr(sbyte bValue, sbyte operand)
        {
            return bValue != 0 || operand != 0;
        }
        private bool LogicalAnd(sbyte bValue, sbyte operand)
        {
            return bValue != 0 && operand != 0;
        }
        private sbyte Floor(sbyte bValue, sbyte operand)
        {
            return Convert.ToSByte(Math.Floor(Convert.ToDouble(bValue)));
        }
        private sbyte Ceiling(sbyte bValue, sbyte operand)
        {
            return Convert.ToSByte(Math.Ceiling(Convert.ToDouble(bValue)));
        }
        private sbyte Maximum(sbyte bValue, sbyte operand)
        {
            return Math.Max(bValue, operand);
        }
        private sbyte Minimum(sbyte bValue, sbyte operand)
        {
            return Math.Min(bValue, operand);
        }
        private sbyte Rint(sbyte bValue, sbyte operand)
        {
            return Convert.ToSByte(Math.Round(Convert.ToDouble(bValue)));
        }
        private sbyte Conjugate(sbyte bValue, sbyte operand)
        {
            return bValue;
        }
        private bool IsNAN(sbyte bValue, sbyte operand)
        {
            return false;
        }
        private sbyte FMax(sbyte bValue, sbyte operand)
        {
            return Math.Max(bValue, operand);
        }
        private sbyte FMin(sbyte bValue, sbyte operand)
        {
            return Math.Min(bValue, operand);
        }
        private sbyte Heaviside(sbyte bValue, sbyte operand)
        {
            if (bValue == 0)
                return operand;

            if (bValue < 0)
                return 0;

            return 1;

        }

        #endregion

    }

    #endregion
}
