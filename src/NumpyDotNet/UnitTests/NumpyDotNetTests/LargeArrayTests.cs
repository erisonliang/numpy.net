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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumpyDotNet;
using System;
#if NPY_INTP_64
#else
using npy_intp = System.Int32;
#endif

namespace NumpyDotNetTests
{
    [TestClass]
    public class LargeArrayTests : TestBaseClass
    {

        [TestMethod]
        public void test_largearray_matmul_INT64_1()
        {
            int width = 1024;
            int height = 1024;

            var x_range = np.arange(0, width, 1, dtype : np.Int64);
            var y_range = np.arange(0, height * 2, 2, dtype : np.Int64);

            var x_mat = np.matmul(x_range.reshape(width, 1), y_range.reshape(1, height));
            var z = np.sum(x_mat);
            print(z);

            Assert.AreEqual(548682596352, z.GetItem(0));

            return;

        }

        [TestMethod]
        public void test_largearray_matmul_INT64_2()
        {
            int width = 1024;
            int height = 1024;

            var x_range = np.arange(0, width, 1, dtype: np.Int64);
            var y_range = np.arange(0, height * 2, 2, dtype: np.Int64);

            var x_mat = np.matmul(x_range.reshape(width, 1), y_range.reshape(1, height));

            var z = np.sum(x_mat, axis: 0);
            var z1 = np.sum(z);
            print(z1);
            Assert.AreEqual(548682596352, z1.GetItem(0));

            z = np.sum(x_mat, axis: 1);
            z1 = np.sum(z);
            print(z1);
            Assert.AreEqual(548682596352, z1.GetItem(0));

            return;

        }


        [TestMethod]
        public void test_largearray_add_INT64_1()
        {
            int width = 1024;
            int height = 1024;

            var x_range = np.arange(0, width, 1, dtype: np.Int64);
            var y_range = np.arange(0, height * 2, 2, dtype: np.Int64);

            var x_mat = np.add(x_range.reshape(width, 1), y_range.reshape(1, height));

            var z = np.sum(x_mat, axis: 0);
            var z1 = np.sum(z);
            print(z1);
            Assert.AreEqual((Int64)1609039872, z1.GetItem(0));

            z = np.sum(x_mat, axis: 1);
            z1 = np.sum(z);
            print(z1);
            Assert.AreEqual((Int64)1609039872, z1.GetItem(0));

            return;

        }


        [TestMethod]
        public void test_largearray_add_INT64_2()
        {
            int width = 1024;
            int height = 1024;

            var x_range = np.arange(0, width, 1, dtype: np.Int64);
            var y_range = np.arange(0, height * 2, 2, dtype: np.Int64);

            var x_mat = np.add(x_range.reshape(1, width, 1), y_range.reshape(1, height, 1));

            var z = np.sum(x_mat, axis: 0);
            var z1 = np.sum(z);
            print(z1);
            Assert.AreEqual((Int64)1571328, z1.GetItem(0));

            z = np.sum(x_mat, axis: 1);
            z1 = np.sum(z);
            print(z1);
            Assert.AreEqual((Int64)1571328, z1.GetItem(0));

            z = np.sum(x_mat, axis: 1);
            z1 = np.sum(z);
            print(z1);
            Assert.AreEqual((Int64)1571328, z1.GetItem(0));

            return;

        }



    }
}