
using cc.isr.VXI11;

namespace cc.isr.LXI.Visa;


/// <summary>   A VISA resource address base class. </summary>
public abstract class VisaResourceNameBase : IEquatable<VisaResourceNameBase>
{

    /// <summary>   (Immutable) the TCP/IP protocol name. </summary>
    public const string TcpIpProtocolName = "TCPIP";

    /// <summary>   (Immutable) the interface resource class name. </summary>
    public const string InterfaceResourceClassName = "INTFC";

    /// <summary>   (Immutable) the instrument resource class name. </summary>
    public const string InstrumentResourceClassName = "INSTR";


    /// <summary>   Specialized default constructor for use only by derived class. </summary>
    protected VisaResourceNameBase()
    {
        this.DeviceNameParser = new DeviceNameParser( string.Empty );
        this.Board = string.Empty;
        this.Protocol = string.Empty;
        this.DeviceName = string.Empty;
        this._deviceName = string.Empty;
        this.Host = string.Empty;
        this.ResourceClass = string.Empty;
        this.ResourceName = string.Empty;
    }

    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <remarks>   2023-02-07. </remarks>
    /// <param name="board">        The board, e.g., TCPIP0. </param>
    /// <param name="host">         The host, e.g., 192.168.0.144. </param>
    /// <param name="deviceName">   The device name, e.g., inst0. </param>
    protected VisaResourceNameBase( string board, string host, string deviceName ) : this()
    {
        this.Board = board;
        this.Host = host;
        this.DeviceName = deviceName;
    }

    /// <summary>   Makes a deep copy of this object. </summary>
    /// <param name="address"> The address of the VISA resource. </param>
    public void Clone( VisaResourceNameBase address )
    {
        this.ResourceName = address.ResourceName;
        this.Board = address.Board;
        this.DeviceName = address.DeviceName;
        this.Host = address.Host;
        this.Protocol = address.Protocol;
        this.ResourceClass = address.ResourceClass;
    }

    /// <summary>   Builds the VISA resource name of the instrument. </summary>
    /// <returns>   A string. </returns>
    public virtual string BuildResourceName()
    {
        StringBuilder builder = new();
        if ( !string.IsNullOrEmpty( this.Board ) )
            _ = builder.Append( this.Board );

        if ( !string.IsNullOrEmpty( this.Host ) )
        {
            if ( builder.Length > 0 )
                _ = builder.Append( "::" );

            _ = builder.Append( this.Host );
        }

        if ( !string.IsNullOrEmpty( this.DeviceName ) )
        {
            if ( builder.Length > 0 )
                _ = builder.Append( "::" );

            _ = builder.Append( this.DeviceName );
        }

        if ( !string.IsNullOrEmpty( this.ResourceClass ) )
        {
            if ( builder.Length > 0 )
                _ = builder.Append( "::" );

            _ = builder.Append( this.ResourceClass );
        }

        return builder.ToString();
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">    An object to compare with this object. </param>
    /// <returns>
    /// true if the current object is equal to the <paramref name="other">other</paramref> parameter;
    /// otherwise, false.
    /// </returns>
    public bool Equals( VisaResourceNameBase other )
    {
        return other != null && string.Equals( this.Board, other.Board, StringComparison.OrdinalIgnoreCase ) &&
               (string.Equals( this.DeviceName, other.DeviceName, StringComparison.OrdinalIgnoreCase ) ||
                 this.DeviceName is null && other.DeviceName is null ||
                 this.DeviceName is null && string.Equals( other.DeviceName, $"{DeviceNameParser.GenericInterfaceFamily}0", StringComparison.OrdinalIgnoreCase ) ||
                 string.Equals( this.DeviceName, $"{DeviceNameParser.GenericInterfaceFamily}0", StringComparison.OrdinalIgnoreCase ) && other.DeviceName is null) &&
               string.Equals( this.Host, other.Host, StringComparison.OrdinalIgnoreCase ) &&
               string.Equals( this.Protocol, other.Protocol, StringComparison.OrdinalIgnoreCase ) &&
               string.Equals( this.ResourceClass, other.ResourceClass, StringComparison.OrdinalIgnoreCase );
        throw new NotImplementedException();
    }

    /// <summary>   Gets or sets the name of the VISA resource, which is also called resource name. </summary>
    /// <remarks> The VISA resource name format is as follows: <para>
    /// ‘Communication/Board Type( USB, GPIB, etc.)::Resource Information( Vendor ID, Product ID, Serial Number, IP address, etc..)::Resource Class’ </para>
    /// </remarks>
    /// <value> The address. </value>
    public string ResourceName { get; protected set; }

    /// <summary>   Gets or sets the board, e.g., TCPIP0. </summary>
    /// <value> The board. </value>
    public string Board { get; set; }

    /// <summary>   Gets or sets the protocol, e.g., TCPIP </summary>
    /// <value> The protocol. </value>
    public string Protocol { get; set; }

    /// <summary>   Gets or sets the host, e.g., 192.168.1.123. </summary>
    /// <value> The host. </value>
    public string Host { get; set; }

    private string _deviceName;
    /// <summary>   Gets or sets the device name also termed device name, e.g., inst0 or gpib0,5 </summary>
    /// <value> The device name. </value>
    public string DeviceName
    {
        get => this._deviceName;
        set {
            if ( !string.Equals( this.DeviceName, value, StringComparison.OrdinalIgnoreCase ) )
            {
                this._deviceName = value;
                _ = this.DeviceNameParser.Parse( this.DeviceName );
            }
        }
    }

    /// <summary>   Gets or sets the resource class, e.g., INSTR, INTFC, or SOCKET. </summary>
    /// <value> The suffix. </value>
    public string ResourceClass { get; set; }

    /// <summary>   Gets or sets the device name parser. </summary>
    /// <value> The device name parser. </value>
    public DeviceNameParser DeviceNameParser { get; }

}
