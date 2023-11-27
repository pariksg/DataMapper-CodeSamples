using Microsoft.Azure.Tests.BuiltIn.Xslt.Netfx.Utils;
using Microsoft.Azure.Workflows.BuiltIn.Xslt.NetFx.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace DataMapperMapTests
{
    [TestClass]
    public class MapTests
    {
        /// <summary>
        /// XSLT builtin operations provider.
        /// </summary>
        private static readonly XsltNetFxBuiltInOperationsProvider TestProviderObject = new XsltNetFxBuiltInOperationsProvider(new TestBuiltInHostConfiguration
        {
            BuiltInLogger = new TestBuiltInLogger(),
            Configuration = new ConfigurationRoot(new[] { new EnvironmentVariablesConfigurationProvider() })
        });

        /// <summary>
        /// Transform with XSLT for Data Mapper test map.
        /// </summary>
        /// <param name="xsltFilePath"></param>
        /// <param name="inputFilePath"></param>
        /// <param name="expectedOutputPath"></param>
        [DataTestMethod]
        [DataRow("X12ToOrder.xslt", "Inputs\\X12_00401_850-input1.xml", "Outputs\\X12ToOrder-output.xml")]
        [DataRow("X12ToOrder.xslt", "Inputs\\X12_00401_850-input2.xml", "Outputs\\X12ToOrder-output.xml")]
        public void XmlToXmlTransformationTests(string xsltFilePath, string inputFilePath, string expectedOutputPath)
        {
            var output = TestMapHelper.TestMap(xsltFilePath, inputFilePath);
            try
            {
                var actualXmlNode = XDocument.Parse(output);
                var expectedXmlNode = XDocument.Parse(File.ReadAllText(expectedOutputPath));
                Assert.IsTrue(XNode.DeepEquals(actualXmlNode, expectedXmlNode));
            }
            catch(AssertFailedException)
            {
                if (!Directory.Exists("FailedOutputs"))
                {
                    Directory.CreateDirectory("FailedOutputs");
                }

                var failedOutputFileName = $"{Guid.NewGuid()}-{Path.GetFileName(expectedOutputPath)}";
                Console.WriteLine($"Output file comparision failed. Look for generated output in FailedOutputs folder, file name: {failedOutputFileName}");
                File.WriteAllText($"FailedOutputs\\{failedOutputFileName}", output);
                throw;
            }
        }

        /// <summary>
        /// Transform with XSLT for Data Mapper test map.
        /// </summary>
        /// <param name="xsltFilePath"></param>
        /// <param name="inputFilePath"></param>
        /// <param name="expectedOutputPath"></param>
        [DataTestMethod]
        [DataRow("X12ToOrderJson.xslt", "Inputs\\X12_00401_850-input1.xml", "Outputs\\X12ToOrder-output.json")]
        [DataRow("X12ToOrderJson.xslt", "Inputs\\X12_00401_850-input2.xml", "Outputs\\X12ToOrder-output.json")]
        public void XmlToJsonTransformationTests(string xsltFilePath, string inputFilePath, string expectedOutputPath)
        {
            var output = TestMapHelper.TestMap(xsltFilePath, inputFilePath);
            try
            {
                var actualJToken = JToken.Parse(output);
                var expectedJToken = JToken.Parse(File.ReadAllText(expectedOutputPath));
                Assert.IsTrue(JToken.DeepEquals(actualJToken, expectedJToken));
            }
            catch (AssertFailedException)
            {
                if (!Directory.Exists("FailedOutputs"))
                {
                    Directory.CreateDirectory("FailedOutputs");
                }

                var failedOutputFileName = $"{Guid.NewGuid()}-{Path.GetFileName(expectedOutputPath)}";
                Console.WriteLine($"Output file comparision failed. Look for generated output in FailedOutputs folder, file name: {failedOutputFileName}");
                File.WriteAllText($"FailedOutputs\\{failedOutputFileName}", output);
                throw;
            }
        }
    }
}
