// ----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ----------------------------------------------------------------------------

// Usage: GenerateXslt <ProjectDirectoryPath> <MapCodeFilePath> <OutputXsltFilePath>

using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.Workflows.Data.Utilities.DataMapper;
using Microsoft.Azure.Workflows.WebJobs.Extensions.Configuration;
using Microsoft.Extensions.Options;

var mapContent = await File
    .ReadAllTextAsync(args[1])
    .ConfigureAwait(continueOnCapturedContext: false);

_ = new EdgeFlowFunctionConfigurationManager(
    hostConfiguration: null,
    workflowExtensionOptions: new OptionsWrapper<WorkflowExtensionOptions>(new WorkflowExtensionOptions()));

var flowConfiguration = new FlowFunctionConfiguration(
                configurationManager: null,
                executionContextOptions: new OptionsWrapper<ExecutionContextOptions>(new ExecutionContextOptions()),
                loggerFactory: null,
                telemetryClient: null,
                configuration: null,
                services: null,
                hostNameProvider: null);

Environment.SetEnvironmentVariable("ProjectDirectoryPath", args[0]);

var generateXsltOutput = await DataMapperUtility.GenerateXslt(flowConfiguration, mapContent, CancellationToken.None).ConfigureAwait(false);

await File
    .WriteAllTextAsync(args[2], generateXsltOutput.XsltContent)
    .ConfigureAwait(false);

Console.WriteLine(generateXsltOutput.XsltContent);