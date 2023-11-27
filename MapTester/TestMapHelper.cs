// ----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ----------------------------------------------------------------------------

namespace Microsoft.Azure.Tests.BuiltIn.Xslt.Netfx.Utils
{
    using System.IO;
    using System.Threading;
    using Microsoft.Azure.Workflows.BuiltIn.Xslt.Common.Entities;
    using Microsoft.Azure.Workflows.BuiltIn.Xslt.NetFx.Providers;
    using Microsoft.Azure.Workflows.Flow.Common.NetStandard.Entities;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Configuration.EnvironmentVariables;
    using Microsoft.WindowsAzure.ResourceStack.Common.Extensions;
    using Microsoft.WindowsAzure.ResourceStack.Common.Json;
    using Newtonsoft.Json;

    /// <summary>
    /// The helper method for test map.
    /// </summary>
    public static class TestMapHelper
    {
        /// <summary>
        /// Invokes xslt worker code to execute the xslt against provided input.
        /// </summary>
        public static string TestMap(string mapPath, string inputFilePath)
        {
            var xsltInput = new XsltInput
            {
                Content = File.ReadAllBytes(inputFilePath),
                MapFilePath = mapPath,
                Map = new ArtifactInformation() { Name = Path.GetFileNameWithoutExtension(mapPath), Source = ArtifactSource.LogicApp },
                IsDataMapperTest = true,
            };

            var XsltNetFxBuiltInOperationsTestProviderObject = new XsltNetFxBuiltInOperationsProvider(new TestBuiltInHostConfiguration
            {
                BuiltInLogger = new TestBuiltInLogger(),
                Configuration = new ConfigurationRoot(new[] { new EnvironmentVariablesConfigurationProvider() })
            });

            var builtInOperationResponse = XsltNetFxBuiltInOperationsTestProviderObject
                .InvokeBuiltInOperation(
                    actionInput: xsltInput.ToJToken(),
                    cancellationToken: CancellationToken.None)
                .Result;

            var envelopedContent = builtInOperationResponse.GetProperty("body").TryFromJToken<ContentEnvelope>();
            if (envelopedContent != null)
            {
                return envelopedContent.Content.DecodeFromBase64String();
            }

            return builtInOperationResponse.GetProperty("body").ToJson(new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
    }
}
