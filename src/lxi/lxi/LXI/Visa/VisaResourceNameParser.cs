using System.Text.RegularExpressions;

using cc.isr.VXI11;

namespace cc.isr.LXI.Visa;

/// <summary>   A parser for a VISA resource name. </summary>
public class VisaResourceNameParser : VisaResourceNameBase
{

    /// <summary>   Constructor. </summary>
    /// <remarks>   2023-02-07. </remarks>
    /// <param name="defaultProtocol">      The default protocol. </param>
    /// <param name="defaultResourceClass"> The default resource class. </param>
    private VisaResourceNameParser( string defaultProtocol, string defaultResourceClass ) : base()
    {
        this.ProtocolDefault = defaultProtocol;
        this.ResourceClassDefault = defaultResourceClass;
        this.ResourceClass = this.ResourceClassDefault;
        this.Protocol = this.ProtocolDefault;
        this.RegexPattern = string.Empty;
        this.BuildRegexPattern();
    }

    /// <summary>   Constructor. </summary>
    /// <remarks>   2023-02-07. </remarks>
    /// <param name="resourceName">         Address of the VISA resource. </param>
    /// <param name="defaultProtocol">      The default protocol. </param>
    /// <param name="defaultResourceClass"> The default resource class. </param>
    public VisaResourceNameParser( string resourceName, string defaultProtocol = VisaResourceNameBase.TcpIpProtocolName,
                                   string defaultResourceClass = VisaResourceNameBase.InstrumentResourceClassName ) : this( defaultProtocol, defaultResourceClass )
    {
        _ = this.ParseResourceName( resourceName );
    }

    /// <summary>   Constructor. </summary>
    /// <param name="defaultProtocol">  The default protocol. </param>
    /// <param name="defaultSuffix">    The default suffix. </param>
    /// <param name="board">            The board. </param>
    /// <param name="host">             The host. </param>
    /// <param name="device">           The device. </param>
    public VisaResourceNameParser( string defaultProtocol, string defaultSuffix,
                                   string board, string host, string device ) : this( defaultProtocol, defaultSuffix )
    {
        this.Board = board;
        this.Host = host;
        this.DeviceName = device;
        this.ResourceName = this.BuildResourceName();
    }

    /// <summary>   Gets or sets the RegEx pattern. </summary>
    /// <value> The RegEx pattern. </value>
    public string RegexPattern { get; set; }

    /// <summary>   Gets or sets the default protocol. </summary>
    /// <value> The default protocol. </value>
    public string ProtocolDefault { get; set; }

    /// <summary>   Gets or sets the resource class default. </summary>
    /// <value> The resource class default. </value>
    public string ResourceClassDefault { get; set; }

    /// <summary>   Builds the RegEx pattern for parsing the VISA address. </summary>
    private void BuildRegexPattern()
    {
        StringBuilder builder = new();
        _ = builder.Append( @$"^(?<{nameof( this.Board )}>(?<{nameof( VisaResourceNameBase.Protocol )}>{this.ProtocolDefault})\d*)" );
        _ = builder.Append( @$"(::(?<{nameof( VisaResourceNameBase.Host )}>[^\s:]+))" );
        _ = builder.Append( @$"(::(?<{nameof( VisaResourceNameBase.DeviceName )}>[^\s:]+(\[.+\])?))" );
        _ = builder.Append( @$"?(::(?<{nameof( VisaResourceNameBase.ResourceClass )}>{this.ResourceClassDefault}))$" );
        this.RegexPattern = builder.ToString();
        // this.RegexPattern = @$"^(?<Board>(?<Protocol>TCPIP)\d*)(::(?<Host>[^\s:]+))(::(?<Device>[^\s:]+(\[.+\])?))?(::(?<Suffix>INSTR))$";
        // this.RegexPattern = @$"^(?<{nameof( Board )}>(?<{nameof( AddressBase.Protocol )}>{DefaultProtocol})\d*)(::(?<{nameof( AddressBase.Host )}>)>[^\s:]+))(::(?<{nameof( AddressBase.Device )}>[^\s:]+(\[.+\])?))?(::(?<{nameof( AddressBase.Suffix )}>{DefaultSuffix}))$";
    }

    /// <summary>   Parse the VISA resource name. </summary>
    /// <param name="resourceName"> Address of the VISA resource. </param>
    /// <returns>   <see langword="true"/> if it succeeds; otherwise, <see langword="false"/>. </returns>
    public bool ParseResourceName( string resourceName )
    {
        if ( resourceName == null ) { return false; }
        var m = Regex.Match( resourceName, this.RegexPattern, RegexOptions.IgnoreCase );
        if ( m == null ) { return false; }
        this.ResourceName = resourceName;
        this.Board = m.Groups[nameof( VisaResourceNameBase.Board )].Value;
        this.Protocol = m.Groups[nameof( VisaResourceNameBase.Protocol )].Value;
        this.Host = m.Groups[nameof( VisaResourceNameBase.Host )].Value;
        this.DeviceName = m.Groups[nameof( VisaResourceNameBase.DeviceName )].Value;
        this.DeviceName = string.IsNullOrEmpty( this.DeviceName ) ? $"{DeviceNameParser.GenericInterfaceFamily}0" : this.DeviceName;
        this.ResourceClass = m.Groups[nameof( VisaResourceNameBase.ResourceClass )].Value;
        return true;
    }


}
