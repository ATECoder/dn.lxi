using System.Net;

using cc.isr.LXI.Visa;
using cc.isr.VXI11;
using cc.isr.VXI11.Codecs;

namespace cc.isr.LXI.Client;

/// <summary>   An LXI instrument client. </summary>
public class LxiInstrumentClient : VXI11.Client.Vxi11InstrumentClient
{

    #region " Construction and Cleanup "

    /// <summary>   Connects. </summary>
    /// <param name="resourceName">     Name of the resource. </param>
    /// <param name="connectTimeout">   (Optional) The connect timeout. </param>
    public void Connect( string resourceName, int connectTimeout = 3000 )
    {
        VisaResourceNameParser parser = new( resourceName, VisaResourceNameBase.TcpIpProtocolName,
                                                                 VisaResourceNameBase.InterfaceResourceClassName );
        this.Connect( parser.Host, parser.DeviceName, connectTimeout );
    }

    /// <summary>   Connects. </summary>
    /// <param name="hostAddress">      The host address. </param>
    /// <param name="deviceName">       The device name. </param>
    /// <param name="connectTimeout">   (Optional) The connect timeout. </param>
    public override void Connect( string hostAddress, string deviceName, int connectTimeout = 3000 )
    {
        base.Connect( hostAddress, deviceName, connectTimeout );
    }

    #endregion
}
