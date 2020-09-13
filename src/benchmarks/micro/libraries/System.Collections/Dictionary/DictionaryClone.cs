// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;

using BenchmarkDotNet.Attributes;

using MicroBenchmarks;

namespace System.Collections.Tests
{
    [BenchmarkCategory(Categories.Libraries, Categories.Collections, Categories.GenericCollections)]
    public class Clone_Dictionary
    {
        [Params(3_000)]
        public int InitialCount;

        private Dictionary<int, int> _intDict;
        private Dictionary<string, string> _stringDict;

        [GlobalSetup(Target = nameof(Clone_Int_Full))]
        public void InitializeClone_Int_Full()
        {
            _intDict = Enumerable.Range(0, InitialCount).ToDictionary(i => i);
        }

        [GlobalSetup(Target = nameof(Clone_Int_HalfRemoved))]
        public void InitializeClone_Int_HalfRemoved()
        {
            _intDict = Enumerable.Range(0, InitialCount).ToDictionary(i => i);
            foreach (var key in Enumerable.Range(0, InitialCount / 2))
            {
                _intDict.Remove(key * 2);
            }
        }

        [GlobalSetup(Target = nameof(Clone_String_Full))]
        public void InitializeClone_String_Full()
        {
            _stringDict = Enumerable.Range(0, InitialCount).ToDictionary(i => i.ToString("X8"), i => i.ToString("X8"));
        }

        [GlobalSetup(Target = nameof(Clone_String_HalfRemoved))]
        public void InitializeClone_String_HalfRemoved()
        {
            _stringDict = Enumerable.Range(0, InitialCount).ToDictionary(i => i.ToString("X8"), i => i.ToString("X8"));
            foreach (var key in Enumerable.Range(0, InitialCount / 2))
            {
                _stringDict.Remove((key * 2).ToString("X8"));
            }
        }

        [Benchmark]
        public Dictionary<int, int> Clone_Int_Full()
        {
            return new Dictionary<int, int>(_intDict, _intDict.Comparer);
        }

        [Benchmark]
        public Dictionary<int, int> Clone_Int_HalfRemoved()
        {
            return new Dictionary<int, int>(_intDict, _intDict.Comparer);
        }

        [Benchmark]
        public Dictionary<string, string> Clone_String_Full()
        {
            return new Dictionary<string, string>(_stringDict, _stringDict.Comparer);
        }

        [Benchmark]
        public Dictionary<string, string> Clone_String_HalfRemoved()
        {
            return new Dictionary<string, string>(_stringDict, _stringDict.Comparer);
        }
    }
}
