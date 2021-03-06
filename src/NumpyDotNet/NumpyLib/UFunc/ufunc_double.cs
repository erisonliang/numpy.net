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
using System.Collections.Concurrent;
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
    #region UFUNC DOUBLE
    internal class UFUNC_Double : UFUNC_BASE<double>, IUFUNC_Operations
    {
        public UFUNC_Double() : base(sizeof(double))
        {

        }

        protected override double ConvertToTemplate(object value)
        {
            return Convert.ToDouble(value);
        }

        protected override double PerformUFuncOperation(UFuncOperation op, double aValue, double bValue)
        {
            double destValue = 0;
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
                    destValue = boolValue ? 1 : 0;
                    break;
                case UFuncOperation.less_equal:
                    boolValue = LessEqual(aValue, bValue);
                    destValue = boolValue ? 1 : 0;
                    break;
                case UFuncOperation.equal:
                    boolValue = Equal(aValue, bValue);
                    destValue = boolValue ? 1 : 0;
                    break;
                case UFuncOperation.not_equal:
                    boolValue = NotEqual(aValue, bValue);
                    destValue = boolValue ? 1 : 0;
                    break;
                case UFuncOperation.greater:
                    boolValue = Greater(aValue, bValue);
                    destValue = boolValue ? 1 : 0;
                    break;
                case UFuncOperation.greater_equal:
                    boolValue = GreaterEqual(aValue, bValue);
                    destValue = boolValue ? 1 : 0;
                    break;
                case UFuncOperation.floor_divide:
                    destValue = FloorDivide(aValue, bValue);
                    break;
                case UFuncOperation.true_divide:
                    destValue = TrueDivide(aValue, bValue);
                    break;
                case UFuncOperation.logical_or:
                    boolValue = LogicalOr(aValue, bValue);
                    destValue = boolValue ? 1 : 0;
                    break;
                case UFuncOperation.logical_and:
                    boolValue = LogicalAnd(aValue, bValue);
                    destValue = boolValue ? 1 : 0;
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
                    destValue = boolValue ? 1 : 0;
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

        #region double specific operation handlers
        protected override double Add(double aValue, double bValue)
        {
            return aValue + bValue;
        }

        protected override double Subtract(double aValue, double bValue)
        {
            return aValue - bValue;
        }
        protected override double Multiply(double aValue, double bValue)
        {
            return aValue * bValue;
        }

        protected override double Divide(double aValue, double bValue)
        {
            if (bValue == 0)
                return 0;
            return aValue / bValue;
        }
        private double Remainder(double aValue, double bValue)
        {
            if (bValue == 0)
            {
                return 0;
            }
            var rem = aValue % bValue;
            if ((aValue > 0) == (bValue > 0) || rem == 0)
            {
                return rem;
            }
            else
            {
                return rem + bValue;
            }
        }
        private double FMod(double aValue, double bValue)
        {
            if (bValue == 0)
                return 0;
            return aValue % bValue;
        }
        protected override double Power(double aValue, double bValue)
        {
            return Math.Pow(aValue, bValue);
        }
        private double Square(double bValue, double operand)
        {
            return bValue * bValue;
        }
        private double Reciprocal(double bValue, double operand)
        {
            if (bValue == 0)
                return 0;

            return 1 / bValue;
        }
        private double OnesLike(double bValue, double operand)
        {
            return 1;
        }
        private double Sqrt(double bValue, double operand)
        {
            return Math.Sqrt(bValue);
        }
        private double Negative(double bValue, double operand)
        {
            return -bValue;
        }
        private double Absolute(double bValue, double operand)
        {
            return Math.Abs(bValue);
        }
        private double Invert(double bValue, double operand)
        {
            return bValue;
        }
        private double LeftShift(double bValue, double operand)
        {
            UInt64 dValue = (UInt64)bValue;
            return dValue << Convert.ToInt32(operand);
        }
        private double RightShift(double bValue, double operand)
        {
            UInt64 dValue = (UInt64)bValue;
            return dValue >> Convert.ToInt32(operand);
        }
        private double BitWiseAnd(double bValue, double operand)
        {
            UInt64 dValue = Convert.ToUInt64(bValue);
            return dValue & Convert.ToUInt64(operand);
        }
        private double BitWiseXor(double bValue, double operand)
        {
            UInt64 dValue = Convert.ToUInt64(bValue);
            return dValue ^ Convert.ToUInt64(operand);
        }
        private double BitWiseOr(double bValue, double operand)
        {
            UInt64 dValue = Convert.ToUInt64(bValue);
            return dValue | Convert.ToUInt64(operand);
        }
        private bool Less(double bValue, double operand)
        {
            return bValue < operand;
        }
        private bool LessEqual(double bValue, double operand)
        {
            return bValue <= operand;
        }
        private bool Equal(double bValue, double operand)
        {
            return bValue == operand;
        }
        private bool NotEqual(double bValue, double operand)
        {
            return bValue != operand;
        }
        private bool Greater(double bValue, double operand)
        {
            return bValue > operand;
        }
        private bool GreaterEqual(double bValue, double operand)
        {
            return bValue >= (dynamic)operand;
        }
        private double FloorDivide(double bValue, double operand)
        {
            if (operand == 0)
            {
                bValue = 0;
                return bValue;
            }
            return Math.Floor(bValue / operand);
        }
        private double TrueDivide(double bValue, double operand)
        {
            if (operand == 0)
                return 0;

            return bValue / operand;
        }
        private bool LogicalOr(double bValue, double operand)
        {
            return bValue != 0 || operand != 0;
        }
        private bool LogicalAnd(double bValue, double operand)
        {
            return bValue != 0 && operand != 0;
        }
        private double Floor(double bValue, double operand)
        {
            return Math.Floor(bValue);
        }
        private double Ceiling(double bValue, double operand)
        {
            return Math.Ceiling(bValue);
        }
        private double Maximum(double bValue, double operand)
        {
            return Math.Max(bValue, operand);
        }
        private double Minimum(double bValue, double operand)
        {
            return Math.Min(bValue, operand);
        }
        private double Rint(double bValue, double operand)
        {
            return Math.Round(bValue);
        }
        private double Conjugate(double bValue, double operand)
        {
            return bValue;
        }
        private bool IsNAN(double bValue, double operand)
        {
            return double.IsNaN(bValue);
        }
        private double FMax(double bValue, double operand)
        {
            if (double.IsNaN(operand))
                return bValue;
            if (double.IsNaN(bValue))
                return operand;

            return Math.Max(bValue, operand);
        }
        private double FMin(double bValue, double operand)
        {
            if (double.IsNaN(operand))
                return bValue;
            if (double.IsNaN(bValue))
                return operand;

            return Math.Min(bValue, operand);
        }
        private double Heaviside(double bValue, double operand)
        {
            if (double.IsNaN(bValue))
                return double.NaN;

            if (bValue == 0.0)
                return operand;

            if (bValue < 0.0)
                return 0.0;

            return 1.0;

        }

        #endregion

    }


    #endregion
}
