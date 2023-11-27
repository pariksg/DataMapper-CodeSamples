// ----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ----------------------------------------------------------------------------

namespace Microsoft.Azure.Tests.BuiltIn.Xslt.Netfx.Utils
{
    using Microsoft.Azure.Workflows.BuiltIn.Abstractions.NetFx.Configuration;
    using Microsoft.Azure.Workflows.BuiltIn.Abstractions.Xslt.Logging;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// The service provider host configuration for tests.
    /// </summary>
    public class TestBuiltInHostConfiguration : IBuiltInHostConfiguration
    {
        /// <summary>
        /// Gets or sets the configuration provider.
        /// </summary>
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// Buit in logger
        /// </summary>
        public IBuiltInLogger BuiltInLogger { get; set; }
    }
}
