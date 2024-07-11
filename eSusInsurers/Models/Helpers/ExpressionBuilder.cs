using eSusInsurers.Models.enums;
using System.Linq.Expressions;

namespace eSusInsurers.Models.Helpers
{
    public static class ExpressionBuilder<T>
    {
        public static Expression<Func<T, bool>> BuildFilterExpression(Dictionary<string, Models.Common.Filter> filters)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression body = Expression.Constant(true);

            foreach (var filter in filters)
            {
                var propertyName = filter.Key;
                var propertyType = GetPropertyType(typeof(T), propertyName);
                var property = GetProperty(parameter, propertyName);

                object filterValue = null;
                List<object> filterValues = new List<object>();

                if (filter.Value.Operation != SearchOperationEnum.LContains)
                {
                    filterValue = ConvertToType(filter.Value.Value, propertyType);
                }
                else
                {
                    var items = filter.Value.Value.Split(',');
                    foreach (var item in items)
                    {
                        var convertedItem = ConvertToType(item, propertyType);
                        filterValues.Add(convertedItem);
                    }
                }

                var constant = Expression.Constant(filterValue, propertyType);
                Expression? equality = null;

                if (filter.Value.Operation == SearchOperationEnum.Equal)
                {
                    var convertedConstant = Expression.Convert(constant, property.Type);
                    equality = Expression.Equal(property, convertedConstant);
                }
                if (filter.Value.Operation == SearchOperationEnum.NotEqual)
                {
                    var convertedConstant = Expression.Convert(constant, property.Type);
                    equality = Expression.NotEqual(property, convertedConstant);
                }
                if (filter.Value.Operation == SearchOperationEnum.Contains)
                {
                    var nullCheck = Expression.NotEqual(property, Expression.Constant(null));
                    var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    var containsCall = Expression.Call(property, containsMethod, Expression.Constant(filterValue));

                    equality = Expression.AndAlso(nullCheck, containsCall);

                }
                else if (filter.Value.Operation == SearchOperationEnum.LContains)
                {
                    var containsMethod = typeof(Enumerable).GetMethods()
                        .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                        .MakeGenericMethod(propertyType);

                    var constantArray = Expression.NewArrayInit(propertyType, filterValues.Select(Expression.Constant));
                    equality = Expression.Call(containsMethod, constantArray, property);
                }
                else if (filter.Value.Operation == SearchOperationEnum.LessEqualThan)
                {
                    var parsedDecimal = Decimal.Parse(filter.Value.Value);
                    var constantDecimal = Expression.Constant(parsedDecimal);
                    var convertedConstant = Expression.Convert(constantDecimal, typeof(Decimal));
                    equality = Expression.LessThanOrEqual(property, convertedConstant);
                }
                else if (filter.Value.Operation == SearchOperationEnum.LessThan)
                {
                    var parsedDateTime = DateTime.Parse(filter.Value.Value);
                    var constantDate = Expression.Constant(parsedDateTime);

                    var hasValue = Expression.Property(property, "HasValue");
                    var getValueOrDefault = Expression.Property(property, "Value");

                    var nullCheck = Expression.NotEqual(hasValue, Expression.Constant(false));
                    var convertedConstant = Expression.Convert(constantDate, typeof(DateTime));
                    var comparison = Expression.LessThan(getValueOrDefault, convertedConstant);
                    equality = Expression.AndAlso(nullCheck, comparison);
                }
                else if (filter.Value.Operation == SearchOperationEnum.GreaterEqualThan)
                {
                    var parsedDecimal = Decimal.Parse(filter.Value.Value);
                    var constantDecimal = Expression.Constant(parsedDecimal);
                    var convertedConstant = Expression.Convert(constantDecimal, typeof(Decimal));
                    equality = Expression.GreaterThanOrEqual(property, convertedConstant);
                }

                body = (body is ConstantExpression constantExpression && constantExpression.Value.Equals(true))
                    ? equality
                    : filter.Value.UseOrLogic ? Expression.Or(body, equality) : Expression.AndAlso(body, equality);
            }

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        private static Type GetPropertyType(Type entityType, string propertyName)
        {
            var propertyNames = propertyName.Split('.');
            Type currentType = entityType;

            foreach (var propName in propertyNames)
            {
                var property = currentType.GetProperty(propName) ?? throw new ArgumentException(
                        $"Property '{propName}' not found on type '{currentType.FullName}'.");
                currentType = property.PropertyType;
            }

            if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                currentType = Nullable.GetUnderlyingType(currentType);
            }

            return currentType;
        }

        private static Expression GetProperty(ParameterExpression parameter, string propertyName)
        {
            var propertyNames = propertyName.Split('.');
            Expression propertyAccess = parameter;

            foreach (var propName in propertyNames)
            {
                propertyAccess = Expression.PropertyOrField(propertyAccess, propName);
            }

            return propertyAccess;
        }

        private static object ConvertToType(string value, Type targetType)
        {
            if (targetType == null || string.IsNullOrEmpty(value))
            {
                return value;
            }

            try
            {
                return Convert.ChangeType(value, targetType);
            }
            catch (InvalidCastException)
            {
                // Handle conversion failure, return original value
                return value;
            }
        }
    }
}
