using System.Reflection;
using System;

namespace ZH.App
{
    public static class Validator
    {
        public static bool IsValid<T>(this T instance, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException($"'{nameof(propertyName)}' cannot be null or whitespace.", nameof(propertyName));
            }
            //var markedProperties = typeof(T).GetProperties().Where(propInfo => propInfo.GetCustomAttributes(typeof(StringRangeAttribute), false).Length > 0);

            PropertyInfo requestedProperty = typeof(T).GetProperty(propertyName);
            if (requestedProperty is null)
            {
                throw new ArgumentException($"'{nameof(requestedProperty)}' cannot be null.", nameof(requestedProperty));
            }

            StringRangeAttribute strRangeAttr = requestedProperty.GetCustomAttribute<StringRangeAttribute>();
            if (strRangeAttr is null)
            {
                throw new ArgumentException($"'{nameof(strRangeAttr)}' cannot be null.", nameof(strRangeAttr));
            }

            return strRangeAttr.WhiteList.Contains(requestedProperty.GetValue(instance).ToString());
        }
    }
}
