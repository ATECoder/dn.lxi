using System.Text.RegularExpressions;
namespace cc.isr.LXI.MSTest;

/// <summary>   (Unit Test Class) a visa resource names tests. </summary>
/// <remarks>   2023-02-08. </remarks>
[TestClass]
public class VisaResourceNamesTests
{

    #region " construction and cleanup "

    /// <summary> Initializes the test class before running the first test. </summary>
    /// <param name="testContext"> Gets or sets the test context which provides information about
    /// and functionality for the current test run. </param>
    /// <remarks>Use ClassInitialize to run code before running the first test in the class</remarks>
    [ClassInitialize()]
    public static void InitializeTestClass( TestContext testContext )
    {
        try
        {
            string methodFullName = $"{testContext.FullyQualifiedTestClassName}.{System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType?.Name}";
            if ( Logger is null )
                Console.WriteLine( methodFullName );
            else
                Logger?.LogMemberInfo( methodFullName );
        }
        catch ( Exception ex )
        {
            if ( Logger is null )
                Console.WriteLine( $"Failed initializing the test class: {ex}" );
            else
                Logger.LogMemberError( "Failed initializing the test class:", ex );

            // cleanup to meet strong guarantees

            try
            {
                CleanupTestClass();
            }
            finally
            {
            }
        }
    }

    /// <summary> Cleans up the test class after all tests in the class have run. </summary>
    /// <remarks> Use <see cref="CleanupTestClass"/> to run code after all tests in the class have run. </remarks>
    [ClassCleanup()]
    public static void CleanupTestClass()
    { }

    private IDisposable? _loggerScope;

    private LoggerTraceListener<VisaResourceNamesTests>? _traceListener;

    /// <summary> Initializes the test class instance before each test runs. </summary>
    [TestInitialize()]
    public void InitializeBeforeEachTest()
    {
        if ( Logger is not null )
        {
            this._loggerScope = Logger.BeginScope( this.TestContext?.TestName ?? string.Empty );
            this._traceListener = new LoggerTraceListener<VisaResourceNamesTests>( Logger );
            _ = Trace.Listeners.Add( this._traceListener );
        }
    }

    /// <summary> Cleans up the test class instance after each test has run. </summary>
    [TestCleanup()]
    public void CleanupAfterEachTest()
    {
        Assert.IsFalse( this._traceListener?.Any( TraceEventType.Error ),
            $"{nameof( this._traceListener )} should have no {TraceEventType.Error} messages" );
        this._loggerScope?.Dispose();
        this._traceListener?.Dispose();
        Trace.Listeners.Clear();
    }

    /// <summary>
    /// Gets or sets the test context which provides information about and functionality for the
    /// current test run.
    /// </summary>
    /// <value> The test context. </value>
    public TestContext? TestContext { get; set; }

    /// <summary>   Gets a logger instance for this category. </summary>
    /// <value> The logger. </value>
    public static ILogger<VisaResourceNamesTests>? Logger { get; } = LoggerProvider.InitLogger<VisaResourceNamesTests>();

    #endregion

    #region " visa resource name tests "

    private readonly string[] _addresses = { "TCPIP::10.0.0.1::INSTR",
                                             "TCPIP0::10.0.0.1::INSTR",
                                             "TCPIP0::10.0.0.1::INST0::INSTR",
                                             "TCPIP::10.0.0.1::gpib,5::INSTR",
                                             "TCPIP0::10.0.0.1::gpib,5::INSTR",
                                             "TCPIP0::10.0.0.1::gpib,5,10::INSTR",
                                             "TCPIP0::10.0.0.1::usb0::INSTR",
                                             "TCPIP0::10.0.0.1::usb0[0x5678::0x33::SN999::1]::INSTR",
                                             "TCPIP0::10.0.0.1::usb0[1234::5678::MYSERIAL::0]::INSTR" };

    private static void AssertVisaAddressShouldParse( string pattern, string address )
    {
        var m = Regex.Match( address, pattern, RegexOptions.IgnoreCase );
        Assert.IsNotNull( m );
        Assert.IsTrue( m.Groups.Keys.Any() );
        Logger?.LogInformation( $"\nParse of: {address}" );
        foreach ( var key in m.Groups.Keys ) { Logger?.LogInformation( $"{key} {m.Groups[key]}" ); }
    }

    /// <summary>   (Unit Test Method) TCP/IP visa address should parse. </summary>
    /// <remarks>   
    /// <code>
    /// Standard Output: 
    /// 2023-02-02 09:45:58.583,
    /// Parse of: TCPIP::10.0.0.1::INSTR
    /// 2023-02-02 09:45:58.583,0 TCPIP::10.0.0.1::INSTR
    /// 2023-02-02 09:45:58.583,1 ::10.0.0.1
    /// 2023-02-02 09:45:58.583,2
    /// 2023-02-02 09:45:58.583,3
    /// 2023-02-02 09:45:58.583,4 ::INSTR
    /// 2023-02-02 09:45:58.583,board TCPIP
    /// 2023-02-02 09:45:58.583, protocol TCPIP
    /// 2023-02-02 09:45:58.583,host 10.0.0.1
    /// 2023-02-02 09:45:58.583,device
    /// 2023-02-02 09:45:58.583,suffix INSTR
    /// 2023-02-02 09:45:58.583,
    /// Parse of: TCPIP0::10.0.0.1::INSTR
    /// 2023-02-02 09:45:58.583,0 TCPIP0::10.0.0.1::INSTR
    /// 2023-02-02 09:45:58.583,1 ::10.0.0.1
    /// 2023-02-02 09:45:58.583,2
    /// 2023-02-02 09:45:58.583,3
    /// 2023-02-02 09:45:58.583,4 ::INSTR
    /// 2023-02-02 09:45:58.583,board TCPIP0
    /// 2023-02-02 09:45:58.583, protocol TCPIP
    /// 2023-02-02 09:45:58.583,host 10.0.0.1
    /// 2023-02-02 09:45:58.583,device
    /// 2023-02-02 09:45:58.583,suffix INSTR
    /// 2023-02-02 09:45:58.583,
    /// Parse of: TCPIP0::10.0.0.1::INST0::INSTR
    /// 2023-02-02 09:45:58.583,0 TCPIP0::10.0.0.1::INST0::INSTR
    /// 2023-02-02 09:45:58.583,1 ::10.0.0.1
    /// 2023-02-02 09:45:58.583,2 ::INST0
    /// 2023-02-02 09:45:58.583,3
    /// 2023-02-02 09:45:58.583,4 ::INSTR
    /// 2023-02-02 09:45:58.583,board TCPIP0
    /// 2023-02-02 09:45:58.583, protocol TCPIP
    /// 2023-02-02 09:45:58.583,host 10.0.0.1
    /// 2023-02-02 09:45:58.583,device INST0
    /// 2023-02-02 09:45:58.583, suffix INSTR
    /// 2023-02-02 09:45:58.583,
    /// Parse of: TCPIP::10.0.0.1::gpib,5::INSTR
    /// 2023-02-02 09:45:58.583,0 TCPIP::10.0.0.1::gpib,5::INSTR
    /// 2023-02-02 09:45:58.583,1 ::10.0.0.1
    /// 2023-02-02 09:45:58.583,2 ::gpib,5
    /// 2023-02-02 09:45:58.583,3
    /// 2023-02-02 09:45:58.583,4 ::INSTR
    /// 2023-02-02 09:45:58.583,board TCPIP
    /// 2023-02-02 09:45:58.583, protocol TCPIP
    /// 2023-02-02 09:45:58.583,host 10.0.0.1
    /// 2023-02-02 09:45:58.583,device gpib,5
    /// 2023-02-02 09:45:58.583, suffix INSTR
    /// 2023-02-02 09:45:58.583,
    /// Parse of: TCPIP0::10.0.0.1::gpib,5::INSTR
    /// 2023-02-02 09:45:58.583,0 TCPIP0::10.0.0.1::gpib,5::INSTR
    /// 2023-02-02 09:45:58.583,1 ::10.0.0.1
    /// 2023-02-02 09:45:58.583,2 ::gpib,5
    /// 2023-02-02 09:45:58.583,3
    /// 2023-02-02 09:45:58.583,4 ::INSTR
    /// 2023-02-02 09:45:58.583,board TCPIP0
    /// 2023-02-02 09:45:58.583, protocol TCPIP
    /// 2023-02-02 09:45:58.583,host 10.0.0.1
    /// 2023-02-02 09:45:58.583,device gpib,5
    /// 2023-02-02 09:45:58.583, suffix INSTR
    /// 2023-02-02 09:45:58.583,
    /// Parse of: TCPIP0::10.0.0.1::gpib,5,10::INSTR
    /// 2023-02-02 09:45:58.583,0 TCPIP0::10.0.0.1::gpib,5,10::INSTR
    /// 2023-02-02 09:45:58.583,1 ::10.0.0.1
    /// 2023-02-02 09:45:58.583,2 ::gpib,5,10
    /// 2023-02-02 09:45:58.583,3
    /// 2023-02-02 09:45:58.583,4 ::INSTR
    /// 2023-02-02 09:45:58.583,board TCPIP0
    /// 2023-02-02 09:45:58.583, protocol TCPIP
    /// 2023-02-02 09:45:58.583,host 10.0.0.1
    /// 2023-02-02 09:45:58.583,device gpib,5,10
    /// 2023-02-02 09:45:58.583, suffix INSTR
    /// 2023-02-02 09:45:58.583,
    /// Parse of: TCPIP0::10.0.0.1::usb0::INSTR
    /// 2023-02-02 09:45:58.583,0 TCPIP0::10.0.0.1::usb0::INSTR
    /// 2023-02-02 09:45:58.583,1 ::10.0.0.1
    /// 2023-02-02 09:45:58.583,2 ::usb0
    /// 2023-02-02 09:45:58.583,3
    /// 2023-02-02 09:45:58.583,4 ::INSTR
    /// 2023-02-02 09:45:58.583,board TCPIP0
    /// 2023-02-02 09:45:58.583, protocol TCPIP
    /// 2023-02-02 09:45:58.583,host 10.0.0.1
    /// 2023-02-02 09:45:58.583,device usb0
    /// 2023-02-02 09:45:58.583, suffix INSTR
    /// 2023-02-02 09:45:58.583,
    /// Parse of: TCPIP0::10.0.0.1::usb0[0x5678::0x33::SN999::1]::INSTR
    /// 2023-02-02 09:45:58.583,0 TCPIP0::10.0.0.1::usb0[0x5678::0x33::SN999::1]::INSTR
    /// 2023-02-02 09:45:58.583,1 ::10.0.0.1
    /// 2023-02-02 09:45:58.583,2 ::usb0[0x5678::0x33::SN999::1]
    /// 2023-02-02 09:45:58.583,3 [0x5678::0x33::SN999::1]
    /// 2023-02-02 09:45:58.583,4 ::INSTR
    /// 2023-02-02 09:45:58.583,board TCPIP0
    /// 2023-02-02 09:45:58.583, protocol TCPIP
    /// 2023-02-02 09:45:58.583,host 10.0.0.1
    /// 2023-02-02 09:45:58.583,device usb0[0x5678::0x33::SN999::1]
    /// 2023-02-02 09:45:58.583, suffix INSTR
    /// 2023-02-02 09:45:58.583,
    /// Parse of: TCPIP0::10.0.0.1::usb0[1234::5678::MYSERIAL::0]::INSTR
    /// 2023-02-02 09:45:58.583,0 TCPIP0::10.0.0.1::usb0[1234::5678::MYSERIAL::0]::INSTR
    /// 2023-02-02 09:45:58.583,1 ::10.0.0.1
    /// 2023-02-02 09:45:58.583,2 ::usb0[1234::5678::MYSERIAL::0]
    /// 2023-02-02 09:45:58.583,3 [1234::5678::MYSERIAL::0]
    /// 2023-02-02 09:45:58.583,4 ::INSTR
    /// 2023-02-02 09:45:58.583,board TCPIP0
    /// 2023-02-02 09:45:58.583, protocol TCPIP
    /// 2023-02-02 09:45:58.583,host 10.0.0.1
    /// 2023-02-02 09:45:58.583,device usb0[1234::5678::MYSERIAL::0]
    /// 2023-02-02 09:45:58.583, suffix INSTR
    /// </code>
    /// </remarks>
    [TestMethod]
    public void TcpIpVisaAddressShouldParse()
    {
        string pattern = @"^(?<board>(?<protocol>TCPIP)\d*)(::(?<host>[^\s:]+))(::(?<device>[^\s:]+(\[.+\])?))?(::(?<suffix>INSTR))$";
        foreach ( string address in this._addresses )
        {
            AssertVisaAddressShouldParse( pattern, address );
        }
    }

    private static void AssertTcpIpInstrumentAddressShouldParse( string visaResourceName )
    {
        cc.isr.LXI.Visa.VisaResourceNameParser instrumentAddress = new( visaResourceName );
        string actual = instrumentAddress.BuildResourceName();
        cc.isr.LXI.Visa.VisaResourceNameParser actualAddress = new( actual );
        Assert.IsTrue( actualAddress.Equals( instrumentAddress ), $"{visaResourceName} not equals {actual}" );
        if ( !instrumentAddress.DeviceNameParser.IsValid() )
        {
            // instrumentAddress = new( address );
            _ = instrumentAddress.DeviceNameParser.IsValid();
        }
        Assert.IsTrue( instrumentAddress.DeviceNameParser.IsValid(), $"{instrumentAddress.DeviceName} is invalid in {visaResourceName}" );
        Logger?.LogInformation( $"device is {(string.IsNullOrEmpty( instrumentAddress.DeviceName ) ? "empty" : instrumentAddress.DeviceName)} for {visaResourceName} " );
    }

    /// <summary>   (Unit Test Method) TCP/IP instrument address should parse. </summary>
    /// <remarks>  
    /// <code>
    /// Standard Output: 
    /// 2023-02-02 09:45:58.570,device is INST0 for TCPIP::10.0.0.1::INSTR
    /// 2023-02-02 09:45:58.573,device is INST0 for TCPIP0::10.0.0.1::INSTR
    /// 2023-02-02 09:45:58.573,device is INST0 for TCPIP0::10.0.0.1::INST0::INSTR
    /// 2023-02-02 09:45:58.574,device is gpib,5 for TCPIP::10.0.0.1::gpib,5::INSTR
    /// 2023-02-02 09:45:58.574,device is gpib,5 for TCPIP0::10.0.0.1::gpib,5::INSTR
    /// 2023-02-02 09:45:58.574,device is gpib,5,10 for TCPIP0::10.0.0.1::gpib,5,10::INSTR
    /// 2023-02-02 09:45:58.574,device is usb0 for TCPIP0::10.0.0.1::usb0::INSTR
    /// 2023-02-02 09:45:58.574,device is usb0[0x5678::0x33::SN999::1] for TCPIP0::10.0.0.1::usb0[0x5678::0x33::SN999::1]::INSTR
    /// 2023-02-02 09:45:58.574,device is usb0[1234::5678::MYSERIAL::0] for TCPIP0::10.0.0.1::usb0[1234::5678::MYSERIAL::0]::INSTR
    /// </code>
    /// </remarks>
    [TestMethod]
    public void TcpIpInstrumentAddressShouldParse()
    {
        foreach ( string address in this._addresses )
        {
            AssertTcpIpInstrumentAddressShouldParse( address );
        }
    }


    #endregion

}
