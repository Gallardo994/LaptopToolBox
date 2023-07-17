using System;

namespace LaptopToolBox.Helpers;

public static class EnumHelper
{
    public static T GetAttribute<TEnum, T>(TEnum enumVal) where T : Attribute where TEnum : Enum
    {
        var type = enumVal.GetType();
        var memInfo = type.GetMember(enumVal.ToString());
        var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
        return attributes.Length > 0 ? (T) attributes[0] : null;
    }
}