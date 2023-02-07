using System.ComponentModel;
using System.Reflection;

using cc.isr.LXI.Server;

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

    /// <summary>   An int extension method that converts a value to a <see cref="LxiInstrumentOperationType"/>. </summary>
    /// <exception cref="ArgumentException">    Thrown when one or more arguments have unsupported or
    ///                                         illegal values. </exception>
    /// <param name="value">    An enum constant representing the enum value. </param>
    /// <returns>   Value as the Ieee4888OperationType. </returns>
    public static LxiInstrumentOperationType ToLxiInstrumentOperationType( this int value )
    {
        return Enum.IsDefined( typeof( LxiInstrumentOperationType ), value )
            ? ( LxiInstrumentOperationType ) value
            : throw new ArgumentException( $"{typeof( int )} value of {value} cannot be cast to {nameof( LxiInstrumentOperationType )}" );
    }

    /// <summary>   An int extension method that converts a value to a <see cref="InterfaceCommand"/>. </summary>
    /// <exception cref="ArgumentException">    Thrown when one or more arguments have unsupported or
    ///                                         illegal values. </exception>
    /// <param name="value">    An enum constant representing the enum value. </param>
    /// <returns>   Value as the Ieee4888InterfaceCommand. </returns>
    public static InterfaceCommand ToInterfaceCommand( this int value )
    {
        return Enum.IsDefined( typeof( InterfaceCommand ), value )
            ? ( InterfaceCommand ) value
            : throw new ArgumentException( $"{typeof( int )} value of {value} cannot be cast to {nameof( InterfaceCommand )}" );
    }

    /// <summary>   An int extension method that converts a value to a <see cref="InterfaceCommandOption"/>. </summary>
    /// <exception cref="ArgumentException">    Thrown when one or more arguments have unsupported or
    ///                                         illegal values. </exception>
    /// <param name="value">    An enum constant representing the enum value. </param>
    /// <returns>   Value as the Ieee4888InterfaceCommandOption. </returns>
    public static InterfaceCommandOption ToInterfaceCommandOption( this int value )
    {
        return Enum.IsDefined( typeof( InterfaceCommandOption ), value )
            ? ( InterfaceCommandOption ) value
            : throw new ArgumentException( $"{typeof( int )} value of {value} cannot be cast to {nameof( InterfaceCommandOption )}" );
    }

    /// <summary>   An int extension method that converts a value to a <see cref="GpibCommandArgument"/>. </summary>
    /// <exception cref="ArgumentException">    Thrown when one or more arguments have unsupported or
    ///                                         illegal values. </exception>
    /// <param name="value">    An enum constant representing the enum value. </param>
    /// <returns>   Value as the Ieee4888OperationType. </returns>
    public static GpibCommandArgument ToGpibCommandArgument( this int value )
    {
        return Enum.IsDefined( typeof( GpibCommandArgument ), value )
            ? ( GpibCommandArgument ) value
            : throw new ArgumentException( $"{typeof( int )} value of {value} cannot be cast to {nameof( GpibCommandArgument )}" );
    }

}
