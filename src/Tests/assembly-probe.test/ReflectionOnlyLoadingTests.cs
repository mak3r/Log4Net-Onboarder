﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionOnlyLoadingTests.cs" company="Apprenda, Inc.">
//   The MIT License (MIT)
//   
//   Copyright (c) 2015 Apprenda Inc.
//   
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//   
//   The above copyright notice and this permission notice shall be included in all
//   copies or substantial portions of the Software.
//   
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//   SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using F2F.Sandbox;
using F2F.Testing.Xunit;

namespace assembly_probe.test
{
    using Apprenda.Integrations.Inspection;

    using CustomAttributeTestTarget;

    using log4net.Config;

    using Xunit;

    /// <summary>
    /// Tests for the AssemblyExtensions which use Mono.Cecil to inspect assemblies.
    /// </summary>
    public class ReflectionOnlyLoadingTests : TestFixture
    {
        public ReflectionOnlyLoadingTests()
        {
            Register(new FileSandbox(new ResourceFileLocator(GetType())));
        }

        [Fact]
        public void CanFindXmlConfiguratorAttribute()
        {
            var configFileValue =
                AssemblyExtensions.GetAssemblyAttributePropertyValue<XmlConfiguratorAttribute, string>(
                    Use<FileSandbox>().ProvideFile("Resources.aspnet-log4net-workload-assy-attribute.dll"), 
                    "ConfigFile");
            Assert.Equal("default.log4net", configFileValue);
        }

        [Fact]
        public void CannotFindAbsentAttribute()
        {
            var descriptionValue =
                AssemblyExtensions.GetAssemblyAttributePropertyValue<CustomAssemblyAttribute, string>(
                    Use<FileSandbox>().ProvideFile("Resources.aspnet-log4net-workload-assy-attribute.dll"), 
                    "Description");
            Assert.Null(descriptionValue);
        }
    }
}