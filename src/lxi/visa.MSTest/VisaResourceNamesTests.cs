using System.Text.RegularExpressions;

using cc.isr.LXI.Logging;
namespace cc.isr.LXI.Visa.MSTest
{
    /// <summary>   (Unit Test Class) a visa resource names tests. </summary>
    /// <remarks>   2023-02-08. </remarks>
    [TestClass]
    public class VisaResourceNamesTests
    {


        private readonly string[] _addresses = { "TCPIP::10.0.0.1::INSTR",
                                                 "TCPIP0::10.0.0.1::INSTR",
                                                 "TCPIP0::10.0.0.1::inst0::INSTR",
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
            Logger.Writer.LogInformation( $"\nParse of: {address}" );
            foreach ( var key in m.Groups.Keys ) { Logger.Writer.LogInformation( $"{key} {m.Groups[key]}" ); }
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
        /// Parse of: TCPIP0::10.0.0.1::inst0::INSTR
        /// 2023-02-02 09:45:58.583,0 TCPIP0::10.0.0.1::inst0::INSTR
        /// 2023-02-02 09:45:58.583,1 ::10.0.0.1
        /// 2023-02-02 09:45:58.583,2 ::inst0
        /// 2023-02-02 09:45:58.583,3
        /// 2023-02-02 09:45:58.583,4 ::INSTR
        /// 2023-02-02 09:45:58.583,board TCPIP0
        /// 2023-02-02 09:45:58.583, protocol TCPIP
        /// 2023-02-02 09:45:58.583,host 10.0.0.1
        /// 2023-02-02 09:45:58.583,device inst0
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
        public void TcpipVisaAddressShouldParse()
        {
            string pattern = @"^(?<board>(?<protocol>TCPIP)\d*)(::(?<host>[^\s:]+))(::(?<device>[^\s:]+(\[.+\])?))?(::(?<suffix>INSTR))$";
            foreach ( string address in this._addresses )
            {
                AssertVisaAddressShouldParse( pattern, address );
            }
        }

        private static void AssertTcpipInstrAddressShouldParse( string visaResourceName )
        {
            cc.isr.LXI.Visa.VisaResourceNameParser instrAddress = new( visaResourceName );
            string actual = instrAddress.BuildResourceName();
            cc.isr.LXI.Visa.VisaResourceNameParser actualAddress = new( actual );
            Assert.IsTrue( actualAddress.Equals( instrAddress ), $"{visaResourceName} not equals {actual}" );
            if ( !instrAddress.DeviceNameParser.IsValid() )
            {
                // instrAddress = new( address );
                _ = instrAddress.DeviceNameParser.IsValid();
            }
            Assert.IsTrue( instrAddress.DeviceNameParser.IsValid(), $"{instrAddress.DeviceName} is invalid in {visaResourceName}" );
            Logger.Writer.LogInformation( $"device is {(string.IsNullOrEmpty( instrAddress.DeviceName ) ? "empty" : instrAddress.DeviceName)} for {visaResourceName} " );
        }

        /// <summary>   (Unit Test Method) TCP/IP instr address should parse. </summary>
        /// <remarks>  
        /// <code>
        /// Standard Output: 
        /// 2023-02-02 09:45:58.570,device is inst0 for TCPIP::10.0.0.1::INSTR
        /// 2023-02-02 09:45:58.573,device is inst0 for TCPIP0::10.0.0.1::INSTR
        /// 2023-02-02 09:45:58.573,device is inst0 for TCPIP0::10.0.0.1::inst0::INSTR
        /// 2023-02-02 09:45:58.574,device is gpib,5 for TCPIP::10.0.0.1::gpib,5::INSTR
        /// 2023-02-02 09:45:58.574,device is gpib,5 for TCPIP0::10.0.0.1::gpib,5::INSTR
        /// 2023-02-02 09:45:58.574,device is gpib,5,10 for TCPIP0::10.0.0.1::gpib,5,10::INSTR
        /// 2023-02-02 09:45:58.574,device is usb0 for TCPIP0::10.0.0.1::usb0::INSTR
        /// 2023-02-02 09:45:58.574,device is usb0[0x5678::0x33::SN999::1] for TCPIP0::10.0.0.1::usb0[0x5678::0x33::SN999::1]::INSTR
        /// 2023-02-02 09:45:58.574,device is usb0[1234::5678::MYSERIAL::0] for TCPIP0::10.0.0.1::usb0[1234::5678::MYSERIAL::0]::INSTR
        /// </code>
        /// </remarks>
        [TestMethod]
        public void TcpipInstrAddressShouldParse()
        {
            foreach ( string address in this._addresses )
            {
                AssertTcpipInstrAddressShouldParse( address );
            }
        }




    }
}