using System.Reflection;
using AutoMapper;

namespace eSusInsurers.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var mapFromType = typeof(IMapFrom<>);

        var mappingMethodName = nameof(IMapFrom<object>.Mapping);

        bool HasInterface(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;
        }

        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

        var argumentTypes = new[] { typeof(Profile) };

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(mappingMethodName);

            if (methodInfo != null)
            {
                methodInfo.Invoke(instance, new object[] { this });
            }
            else
            {
                var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                if (interfaces.Count > 0)
                    foreach (var @interface in interfaces)
                    {
                        var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                        interfaceMethodInfo?.Invoke(instance, new object[] { this });
                    }
            }
        }
    }
    
    public void ApplyMappingsFromAssembly(Assembly assembly, Func<Type, bool> filter)
    {
        var mapFromType = typeof(IMapFrom<>);
        var mappingMethodName = nameof(IMapFrom<object>.Mapping);

        bool HasInterface(Type t) =>
            t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == mapFromType);

        var types = assembly.GetExportedTypes()
            .Where(t => HasInterface(t) && (filter?.Invoke(t) ?? true))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod(mappingMethodName);

            if (methodInfo != null)
            {
                methodInfo.Invoke(instance, new object[] { this });
            }
            else
            {
                var interfaces = type.GetInterfaces().Where(HasInterface);
                foreach (var @interface in interfaces)
                {
                    var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, new[] { typeof(Profile) });
                    interfaceMethodInfo?.Invoke(instance, new object[] { this });
                }
            }
        }
    }
}