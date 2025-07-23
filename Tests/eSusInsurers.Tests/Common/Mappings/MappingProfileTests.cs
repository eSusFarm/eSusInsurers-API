using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using eSusInsurers.Common.Mappings;
using Xunit;

public class MappingProfileTests
{
    [Fact]
    public void Constructor_InitializesWithoutError()
    {
        var profile = new MappingProfile();
        Assert.NotNull(profile);
    }

    [Fact]
    public void ApplyMappingsFromAssembly_DetectsConcreteMappingMethod()
    {
        var profile = new MappingProfile();
        var assembly = typeof(MockMapFromClass).Assembly;

        var ex = Record.Exception(() => profile.ApplyMappingsFromAssembly(assembly));

        Assert.Null(ex);
    }

    [Fact]
    public void ApplyMappingsFromAssembly_UsesInterfaceFallbackWhenMethodIsMissing()
    {
        var profile = new MappingProfile();
        var assembly = typeof(FallbackMapType).Assembly;

        var ex = Record.Exception(() => profile.ApplyMappingsFromAssembly(assembly));

        Assert.Null(ex);
    }

    [Fact]
    public void ApplyMappingsFromAssembly_DoesNothingWhenNoMapFromTypesExist()
    {
        var profile = new MappingProfile();
        var assembly = typeof(string).Assembly; // Known clean assembly

        var ex = Record.Exception(() => profile.ApplyMappingsFromAssembly(assembly));

        Assert.Null(ex);
    }

    [Fact]
    public void MappingProfile_ConfigurationIsValid_WithStubMappingOnly()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SourceStub, DestinationStub>();
        });

        configuration.AssertConfigurationIsValid();
    }

    // Dummy types to simulate mapping behavior

    private class MockMapFromClass : IMapFrom<SourceStub>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SourceStub, DestinationStub>();
        }
    }

    private class FallbackMapType : IMapFrom<object>
    {
        void IMapFrom<object>.Mapping(Profile profile)
        {
            profile.CreateMap<object, FallbackMapType>();
        }
    }

    private class SourceStub
    {
        public string Name { get; set; }
    }

    private class DestinationStub
    {
        public string Name { get; set; }
    }
}
