// ----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ----------------------------------------------------------------------------

// Usage:
// MapperAPIClient generate <MapCodeFilePath> <OutputXsltFilePath>
// MapperAPIClient test <MapName> <TestMapInputFilePath> <OutputFilePath>

using Microsoft.Azure.Workflows.Common.Constants;
using Microsoft.Azure.Workflows.Data.Entities;
using Microsoft.Azure.Workflows.Templates.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.ResourceStack.Common.Extensions;
using Newtonsoft.Json;
using System.Net;

if (args[0].EqualsInsensitively("generate"))
{
    using (var client = new HttpClient())
    {
        var mapContent = await File
            .ReadAllTextAsync(args[1])
            .ConfigureAwait(continueOnCapturedContext: false);

        var generateXsltInput = new GenerateXsltInput { MapContent = mapContent };
        var content = new StringContent(JsonConvert.SerializeObject(generateXsltInput), System.Text.Encoding.UTF8, "application/json");
        var response = await client
            .PostAsync($"http://localhost:8001/runtime/webhooks/workflow/api/management/generateXslt", content)
            .ConfigureAwait(continueOnCapturedContext: false);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsAsync<GenerateXsltResponse>().ConfigureAwait(continueOnCapturedContext: false);
        Assert.IsNotNull(responseContent);
        Assert.IsNotNull(responseContent.XsltContent);
        Assert.AreNotEqual(string.Empty, responseContent.XsltContent.Trim());

        await File
            .WriteAllTextAsync(args[2], responseContent.XsltContent)
            .ConfigureAwait(continueOnCapturedContext: false);
        Console.WriteLine("Map code:");
        Console.WriteLine(mapContent);
        Console.WriteLine("Generated Xslt:");
        Console.WriteLine(responseContent.XsltContent);
    }
}
else if (args[0].EqualsInsensitively("test"))
{
    var testInputInstance = await File
        .ReadAllTextAsync(args[2])
        .ConfigureAwait(continueOnCapturedContext: false);

    var testInputFile = new FileInfo(args[2]);

    var testMapApiInput = new TestMapInput
    {
        InputInstanceMessage = new ContentEnvelope
        {
            Content = testInputInstance.EncodeToBase64String(),
            ContentType = testInputFile.Extension.EqualsOrdinalInsensitively(".xml")
                ? FlowConstants.XmlContentType
                : testInputFile.Extension.EqualsOrdinalInsensitively(".json")
                    ? FlowConstants.JsonContentType
                    : FlowConstants.XmlContentType,
        },
    };

    var content = new StringContent(JsonConvert.SerializeObject(testMapApiInput), System.Text.Encoding.UTF8, FlowConstants.JsonContentType);

    using (var client = new HttpClient())
    {
        var response = await client
            .PostAsync($"http://localhost:8001/runtime/webhooks/workflow/api/management/maps/{args[1]}/testMap", content)
            .ConfigureAwait(continueOnCapturedContext: false);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var responseContent = await response.Content.ReadAsAsync<TestMapResponse>().ConfigureAwait(continueOnCapturedContext: false);

        Assert.IsNotNull(responseContent);
        Assert.IsNotNull(responseContent.OutputInstance);

        var outputInstance = responseContent.OutputInstance.Content.DecodeFromBase64String();

        Assert.AreNotEqual(string.Empty, outputInstance.Trim());

        await File
            .WriteAllTextAsync(args[3], outputInstance)
            .ConfigureAwait(continueOnCapturedContext: false);
        Console.WriteLine("Input:");
        Console.WriteLine(testInputInstance);
        Console.WriteLine("Output:");
        Console.WriteLine(outputInstance);
    }
}
