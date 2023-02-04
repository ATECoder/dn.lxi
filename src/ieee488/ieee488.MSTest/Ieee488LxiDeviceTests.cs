using cc.isr.VXI11;
using cc.isr.VXI11.Codecs;
using cc.isr.LXI.Logging;
using cc.isr.LXI.IEEE488;
using cc.isr.LXI.IEEE488.EnumExtensions;
using cc.isr.LXI.IEEE488.Mock;
using cc.isr.LXI;
using cc.isr.LXI.Mock;
using System.Text;

namespace cc.isr.LXI.IEEE488.MSTest;

/// <summary>   (Unit Test Class) a support tests. </summary>
[TestClass]
public class Ieee488LxiDeviceTests
{

    #region " fixture construction and cleanup "

    /// <summary>   Initializes the fixture. </summary>
    /// <param name="context">  The context. </param>
    [ClassInitialize]
    public static void InitializeFixture( TestContext context )
    {
        try
        {
            _classTestContext = context;
            Logger.Writer.LogInformation( $"{_classTestContext.FullyQualifiedTestClassName}.{System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType?.Name}" );

            _mockDevice = new Ieee488Device();
            _vxi11Device = new LxiDevice( _mockDevice );

            int clientId = 1;
            _vxi11Device.ClientId = clientId;
        }
        catch ( Exception ex )
        {
            Logger.Writer.LogMemberError( $"Failed initializing fixture:", ex );
            CleanupFixture();
        }
    }

    public TestContext? TestContext { get; set; }

    private static TestContext? _classTestContext;

    /// <summary>   Cleanup fixture. </summary>
    [ClassCleanup]
    public static void CleanupFixture()
    {
        AssertShouldDestroyLink();
    }

    private static IIeee488Device? _mockDevice;

    private static IVxi11Device? _vxi11Device;

    #endregion

    #region " client emulations "
    private static CreateLinkResp CreateLink( IVxi11Device? lxiDevice, string interfaceDeviceString, bool lockEnabled = false, int lockTimeout = 1000 )
    {
        if ( lxiDevice is null )
            return new CreateLinkResp() { ErrorCode = DeviceErrorCode.ChannelNotEstablished };

        CreateLinkParms createLinkParam = new() {
            InterfaceDeviceString = interfaceDeviceString,
            LockDevice = lockEnabled,
            LockTimeout = lockTimeout,
        };
        CreateLinkResp linkResp = lxiDevice.CreateLink( createLinkParam );
        if ( linkResp.ErrorCode == DeviceErrorCode.NoError )
        {
            lxiDevice.DeviceLink = linkResp.DeviceLink;
            lxiDevice.MaxReceiveLength = linkResp.MaxReceiveSize;
            lxiDevice.LastDeviceError = linkResp.ErrorCode;
            lxiDevice.AbortPortNumber = linkResp.AbortPort;

            lxiDevice.InterfaceDeviceString = interfaceDeviceString;
        }

        lxiDevice.RemoteEnabled = true;

        return linkResp;
    }

    /// <summary>
    /// Calls remote procedure <see cref="Vxi11Message.DestroyLinkProcedure"/>;
    /// Closes a link to a device.
    /// </summary>
    /// <returns>
    /// A Result from remote procedure call of type <see cref="Codecs.DeviceError"/>.
    /// </returns>
    public static DeviceError DestroyLink( IVxi11Device? vxi11Device )
    {
        if ( vxi11Device is null )
            return new DeviceError() { ErrorCode = DeviceErrorCode.ChannelNotEstablished };
        DeviceLink? link = vxi11Device.DeviceLink;
        try
        {
            return link is not null ? vxi11Device.DestroyLink( link ) : new DeviceError();
        }
        catch ( Exception )
        {
            throw;
        }
        finally
        {
        }
    }


    public static DeviceWriteResp Send( IVxi11Device? vxi11Device, string message )
    {
        return vxi11Device is null
            ? new DeviceWriteResp() { ErrorCode = DeviceErrorCode.ChannelNotEstablished }
            : Send( vxi11Device, vxi11Device.CharacterEncoding.GetBytes( message ) );
    }

    public static DeviceWriteResp Send( IVxi11Device? vxi11Device, byte[] data )
    {
        if ( vxi11Device is null )
            return new DeviceWriteResp() { ErrorCode = DeviceErrorCode.ChannelNotEstablished };

        if ( vxi11Device.DeviceLink is null )
            return new DeviceWriteResp() { ErrorCode = DeviceErrorCode.ChannelNotEstablished };

        if ( data is null || data.Length == 0 ) return new DeviceWriteResp();

        if ( data.Length > vxi11Device.MaxReceiveLength )
            throw new DeviceException( $"Data size {data.Length} exceed {nameof( LxiDevice.MaxReceiveLength )}({vxi11Device.MaxReceiveLength})", DeviceErrorCode.IOError );

        DeviceWriteParms writeParam = new() {
            Link = vxi11Device.DeviceLink,
            IOTimeout = vxi11Device.IOTimeout, // in ms
            LockTimeout = vxi11Device.LockTimeout, // in ms
            Flags = vxi11Device.Eoi ? DeviceOperationFlags.EndIndicator : DeviceOperationFlags.None,
        };
        writeParam.SetData( data );
        return vxi11Device.DeviceWrite( writeParam );
    }

    public static DeviceReadResp Receive( IVxi11Device? vxi11Device, int byteCount )
    {
        if ( vxi11Device is null )
            return new DeviceReadResp() { ErrorCode = DeviceErrorCode.ChannelNotEstablished };

        if ( vxi11Device.DeviceLink is null )
            return new DeviceReadResp() { ErrorCode = DeviceErrorCode.ChannelNotEstablished };

        DeviceReadParms readParam = new() {
            Link = vxi11Device.DeviceLink,
            RequestSize = byteCount,
            IOTimeout = vxi11Device.IOTimeout,
            LockTimeout = vxi11Device.LockTimeout,
            Flags = vxi11Device.ReadTermination > 0 ? DeviceOperationFlags.TerminationCharacterSet : DeviceOperationFlags.None,
            TermChar = vxi11Device.ReadTermination
        };
        return vxi11Device.DeviceRead( readParam );
    }

    #endregion

    /// <summary>   Assert should create link. </summary>
    /// <remarks>   2023-02-03. </remarks>
    private static void AssertShouldCreateLink()
    {
        if ( _vxi11Device is not null && _vxi11Device.CanCreateNewDeviceLink() )
        {
            CreateLinkResp linkResp = CreateLink( _vxi11Device, "inst0" );
            Assert.AreEqual( DeviceErrorCode.NoError, linkResp.ErrorCode );
        }
    }

    /// <summary>   Assert should destroy link. </summary>
    private static void AssertShouldDestroyLink()
    {
        if ( _vxi11Device is not null && !_vxi11Device.CanCreateNewDeviceLink() )
        {
            DeviceError deviceError = DestroyLink( _vxi11Device );
            Assert.IsNotNull( deviceError );
            Assert.AreEqual( DeviceErrorCode.NoError, deviceError.ErrorCode );
        }
    }

    /// <summary>   (Unit Test Method) should read identity. </summary>
    /// <remarks>
    /// <code>
    /// Standard Output: 
    ///   2023-02-03 20:09:12.193,cc.isr.LXI.IEEE488.MSTest.Ieee488LxiDevice.Ieee488LxiDevice
    ///   2023-02-03 20:09:12.275,creating link to inst0
    ///   2023-02-03 20:09:12.279, link ID: 1 -> Received：*IDN?
    ///   
    ///   2023-02-03 20:09:12.279,Process the instruction： *IDN?
    ///   2023-02-03 20:09:12.280,Query results： INTEGRATED SCIENTIFIC RESOURCES,MODEL IEEE488Mock,001,1.0.8434。
    /// </code>
    /// </remarks>
    [TestMethod]
    public void ShouldReadIdentity()
    {
        if ( _mockDevice is null ) return;         AssertShouldCreateLink();

        string command = $"{Ieee488Commands.IDNRead}\n";

        DeviceWriteResp writeResp = Send( _vxi11Device, command );
        Assert.AreEqual( DeviceErrorCode.NoError, writeResp.ErrorCode );
        Assert.AreEqual( command.Length, writeResp.Size );

        string expectedValue = _mockDevice.Identity;
        DeviceReadResp readResp = Receive( _vxi11Device, _vxi11Device!.MaxReceiveLength );
        Assert.AreEqual( DeviceErrorCode.NoError, readResp.ErrorCode );
        Assert.AreEqual( expectedValue, _vxi11Device!.CharacterEncoding.GetString( readResp.GetData() ) );
    }
}
