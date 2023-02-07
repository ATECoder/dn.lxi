namespace cc.isr.LXI.Visa;


/// <summary>   A TCP/IP VISA INSTR address also termed resource name. </summary>
public class TcpipInstrResourceName : VisaResourceNameBase
{

    /// <summary>   (Immutable) the default protocol. </summary>
    public const string ProtocolDefault = "TCPIP";

    /// <summary>   (Immutable) the default suffix. </summary>
    public const string SuffixDefault = "INSTR";

    /// <summary>   Constructor. </summary>
    /// <exception cref="InvalidOperationException">    Thrown when the requested operation is
    ///                                                 invalid. </exception>
    /// <param name="fullResourceName"> The full resource name. </param>
    public TcpipInstrResourceName( string fullResourceName ) : base()
    {
        this.InterfaceDeviceAddressParser = new VXI11.InsterfaceDeviceStringParser( string.Empty );
        base.Address = this.ParseAddress( fullResourceName )
            ? fullResourceName
            : throw new InvalidOperationException( $"Failed parsing resource name {fullResourceName}" );

    }

    /// <summary>   Parses the VISA address (resource name). </summary>
    /// <param name="address"> The full resource name. </param>
    /// <returns>   True if it succeeds, false if it fails. </returns>
    public bool ParseAddress( string address )
    {
        VisaResourceNameParser parser = new( ProtocolDefault, SuffixDefault );
        bool result = parser.ParseAddress( address );
        if ( result )
        {
            this.Clone( parser );
            this.InterfaceDeviceAddressParser = new VXI11.InsterfaceDeviceStringParser( this.InterfaceDeviceString );
        }
        return result;
    }

    /// <summary>   Gets or sets the interface device string parser. </summary>
    /// <value> The device address. </value>
    public VXI11.InsterfaceDeviceStringParser InterfaceDeviceAddressParser { get; private set; }

}
