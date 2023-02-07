using System.Text.RegularExpressions;

using cc.isr.VXI11;

namespace cc.isr.LXI.Visa;

/// <summary>   A parser for a VISA resource name. </summary>
public class VisaResourceNameParser : VisaResourceNameBase
{

    /// <summary>   Constructor. </summary>
    /// <param name="defaultProtocol">  The default protocol. </param>
    /// <param name="defaultSuffix">    The default suffix. </param>
    public VisaResourceNameParser( string defaultProtocol, string defaultSuffix ) : base()
    {
        this.ProtocolDefault = defaultProtocol;
        this.SuffixDefault = defaultSuffix;
        this.Suffix = this.SuffixDefault;
        this.Protocol = this.ProtocolDefault;
        this.RegexPattern = string.Empty;
        this.BuildRegexPattern();
    }

    /// <summary>   Constructor. </summary>
    /// <param name="defaultProtocol">  The default protocol. </param>
    /// <param name="defaultSuffix">    The default suffix. </param>
    /// <param name="board">            The board. </param>
    /// <param name="host">             The host. </param>
    /// <param name="device">           The device. </param>
    public VisaResourceNameParser( string defaultProtocol, string defaultSuffix, string board, string host, string device ) : this( defaultProtocol, defaultSuffix )
    {
        this.Board = board;
        this.Host = host;
        this.InterfaceDeviceString = device;
    }

    /// <summary>   Gets or sets the RegEx pattern. </summary>
    /// <value> The RegEx pattern. </value>
    public string RegexPattern { get; set; }

    /// <summary>   Gets or sets the default protocol. </summary>
    /// <value> The default protocol. </value>
    public string ProtocolDefault { get; set; }

    /// <summary>   Gets or sets the default suffix. </summary>
    /// <value> The default suffix. </value>
    public string SuffixDefault { get; set; }

    /// <summary>   Builds the RegEx pattern for parsing the VISA address. </summary>
    private void BuildRegexPattern()
    {
        StringBuilder builder = new();
        _ = builder.Append( @$"^(?<{nameof( this.Board )}>(?<{nameof( VisaResourceNameBase.Protocol )}>{this.ProtocolDefault})\d*)" );
        _ = builder.Append( @$"(::(?<{nameof( VisaResourceNameBase.Host )}>[^\s:]+))" );
        _ = builder.Append( @$"(::(?<{nameof( VisaResourceNameBase.InterfaceDeviceString )}>[^\s:]+(\[.+\])?))" );
        _ = builder.Append( @$"?(::(?<{nameof( VisaResourceNameBase.Suffix )}>{this.SuffixDefault}))$" );
        this.RegexPattern = builder.ToString();
        // this.RegexPattern = @$"^(?<Board>(?<Protocol>TCPIP)\d*)(::(?<Host>[^\s:]+))(::(?<Device>[^\s:]+(\[.+\])?))?(::(?<Suffix>INSTR))$";
        // this.RegexPattern = @$"^(?<{nameof( Board )}>(?<{nameof( AddressBase.Protocol )}>{DefaultProtocol})\d*)(::(?<{nameof( AddressBase.Host )}>)>[^\s:]+))(::(?<{nameof( AddressBase.Device )}>[^\s:]+(\[.+\])?))?(::(?<{nameof( AddressBase.Suffix )}>{DefaultSuffix}))$";
    }

    /// <summary>   Parse the address of the VISA resource. </summary>
    /// <param name="address"> Address of the VISA resource. </param>
    /// <returns>   True if it succeeds, false if it fails. </returns>
    public bool ParseAddress( string address )
    {
        if ( address == null ) { return false; }
        var m = Regex.Match( address, this.RegexPattern, RegexOptions.IgnoreCase );
        if ( m == null ) { return false; }
        this.Address = address;
        this.Board = m.Groups[nameof( VisaResourceNameBase.Board )].Value;
        this.Protocol = m.Groups[nameof( VisaResourceNameBase.Protocol )].Value;
        this.Host = m.Groups[nameof( VisaResourceNameBase.Host )].Value;
        this.InterfaceDeviceString = m.Groups[nameof( VisaResourceNameBase.InterfaceDeviceString )].Value;
        this.InterfaceDeviceString = string.IsNullOrEmpty( this.InterfaceDeviceString ) ? $"{InsterfaceDeviceStringParser.GenericInterfaceFamily}0" : this.InterfaceDeviceString;
        this.Suffix = m.Groups[nameof( VisaResourceNameBase.Suffix )].Value;
        return true;
    }
}
