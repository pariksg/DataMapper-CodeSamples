// ----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ----------------------------------------------------------------------------

// Usage: MapTester <XsltMapFilePath> <TestInputFilePath> <OutputFilePath>
using System;
using System.IO;
using Microsoft.Azure.Tests.BuiltIn.Xslt.Netfx.Utils;

namespace MapTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var output = TestMapHelper.TestMap(args[0], args[1]);

            File.WriteAllText(args[2], output);

            Console.WriteLine("Input:");
            Console.WriteLine(File.ReadAllText(args[1]));
            Console.WriteLine("Output:");
            Console.WriteLine(output);
        }
    }
}
