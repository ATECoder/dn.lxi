using System.ComponentModel;
using System.Reflection;

namespace cc.isr.LXI.EnumExtensions;

/// <summary>   A support class for VXI-11 IEEE 488 enum extensions. </summary>
public static class LxiEnumExtensions
{

    /// <summary>   Gets a description from an Enum. </summary>
    /// <param name="value">    An enum constant representing the value option. </param>
    /// <returns>   The description. </returns>
    public static string GetDescription( this Enum value )
    {
        return
            value
                .GetType()
                .GetMember( value.ToString() )
                .FirstOrDefault()
                ?.GetCustomAttribute<DescriptionAttribute>()
                ?.Description
            ?? value.ToString();
    }
}
