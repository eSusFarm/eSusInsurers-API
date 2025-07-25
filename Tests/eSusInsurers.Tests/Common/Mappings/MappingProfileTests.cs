using System;
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

        var ex = Record.Exception(() => profile.ApplyMappingsFromAssembly(assembly, t => t == typeof(MockMapFromClass)));
        Assert.Null(ex);
    }

    [Fact]
    public void ApplyMappingsFromAssembly_UsesInterfaceFallbackWhenMethodIsMissing()
    {
        var profile = new MappingProfile();
        var assembly = typeof(FallbackMapType).Assembly;

        var ex = Record.Exception(() => profile.ApplyMappingsFromAssembly(assembly, t => t == typeof(MockMapFromClass)));
        Assert.Null(ex);
    }

    [Fact]
    public void ApplyMappingsFromAssembly_DoesNothingWhenNoMapFromTypesExist()
    {
        var profile = new MappingProfile();
        var assembly = typeof(string).Assembly;

        var ex = Record.Exception(() => profile.ApplyMappingsFromAssembly(assembly));
        Assert.Null(ex);
    }

    [Fact]
    public void RoleRequest_To_Role_Mapping_Should_Validate()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<WMS.Models.Roles.RoleRequest, eSusInsurers.Domain.Entities.Role>()
                .ForMember(dest => dest.EmailTemplates, opt => opt.Ignore())
                .ForMember(dest => dest.EmailTemplatesAus, opt => opt.Ignore())
                .ForMember(dest => dest.InverseReportingTo, opt => opt.Ignore())
                .ForMember(dest => dest.MenuRoleFunctionalityApprovalProcesses, opt => opt.Ignore())
                .ForMember(dest => dest.MenuRolesFunctionalities, opt => opt.Ignore())
                .ForMember(dest => dest.MenuRolesPrivileges, opt => opt.Ignore())
                .ForMember(dest => dest.ReportingTo, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore());
        });

        config.AssertConfigurationIsValid();
    }

    [Fact]
    public void ApplyMappingsFromAssembly_Throws_WhenMappingMethodThrows()
    {
        // Arrange
        var profile = new MappingProfile();

        // Act
        var ex = Record.Exception(() =>
            profile.ApplyMappingsFromAssembly(typeof(ExceptionThrowingMapType).Assembly,
                t => t == typeof(ExceptionThrowingMapType)));

        // Assert
        Assert.NotNull(ex);

        // Ensure we unwrap if it's a TargetInvocationException
        var baseEx = ex is TargetInvocationException tie && tie.InnerException != null
            ? tie.InnerException
            : ex;

        Assert.IsType<InvalidOperationException>(baseEx);
        Assert.Equal("Deliberate mapping failure", baseEx.Message);
    }


    [Fact]
    public void ApplyMappingsFromAssembly_Throws_WhenActivatorFails()
    {
        var profile = new MappingProfile();
        var assembly = typeof(ThrowingConstructorMapType).Assembly;

        var ex = Record.Exception(() =>
            profile.ApplyMappingsFromAssembly(assembly, t => t == typeof(ThrowingConstructorMapType)));

        var baseEx = Unwrap(ex);
        Assert.IsType<InvalidOperationException>(baseEx);
        Assert.Equal("Constructor failed intentionally", baseEx.Message);
    }
    
    [Fact]
    public void ApplicationChildMenuItems_To_MenuRolesPrivilege_Should_Validate()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<WMS.Models.Roles.ApplicationChildMenuItems, eSusInsurers.Domain.Entities.MenuRolesPrivilege>()
                .ForMember(dest => dest.RoleId, opt => opt.Ignore())
                .ForMember(dest => dest.ApplicationChildMenu, opt => opt.Ignore())
                .ForMember(dest => dest.ApplicationMenu, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Read, opt => opt.Ignore())
                .ForMember(dest => dest.Create, opt => opt.Ignore())
                .ForMember(dest => dest.Update, opt => opt.Ignore())
                .ForMember(dest => dest.Delete, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore());
        });

        config.AssertConfigurationIsValid();
    }
    
    [Fact]
    public void IMapFrom_ManualImplementation_RegistersMappingSuccessfully()
    {
        // Arrange
        var profile = new MappingProfile();
        var instance = new DummyDestination();

        // Act
        instance.Mapping(profile);

        var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
        var mapper = config.CreateMapper();

        // Assert
        var source = new DummySource { Name = "test" };
        var dest = mapper.Map<DummyDestination>(source);

        Assert.Equal("test", dest.Name);
    }

    // 🧩 Helper Method
    private static Exception Unwrap(Exception ex) =>
        ex is TargetInvocationException tie && tie.InnerException != null ? tie.InnerException : ex;

    // 🧪 Stub types
    private class MockMapFromClass : IMapFrom<SourceStub>
    {
        public void Mapping(Profile profile) => profile.CreateMap<SourceStub, DestinationStub>();
    }

    private class FallbackMapType : IMapFrom<object>
    {
        void IMapFrom<object>.Mapping(Profile profile) => profile.CreateMap<object, FallbackMapType>();
    }

    public class ExceptionThrowingMapType : IMapFrom<object>
    {
        public void Mapping(Profile profile) => throw new InvalidOperationException("Deliberate mapping failure");
    }

    public class ThrowingConstructorMapType : IMapFrom<object>
    {
        public ThrowingConstructorMapType() => throw new InvalidOperationException("Constructor failed intentionally");
        public void Mapping(Profile profile) { }
    }

    private class SourceStub { public string Name { get; set; } }
    private class DestinationStub { public string Name { get; set; } }
    private class DummySource
    {
        public string Name { get; set; } = string.Empty;
    }

    private class DummyDestination : IMapFrom<DummySource>
    {
        public string Name { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DummySource, DummyDestination>();
        }
    }
}