using System;
using System.Linq;

public static class Type_Extensions
{
    public static TValue GetAttributeValue<TAttribute, TValue>(this Type type, Func<TAttribute, TValue> valueSelector) where TAttribute : Attribute
    {
        var att = type.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
        if (att != null)
        {
            return valueSelector(att);
        }
        return default(TValue);
    }
}
