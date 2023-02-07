// See https://aka.ms/new-console-template for more information

using cc.isr.VXI11;
using cc.isr.LXI.Logging;
using cc.isr.LXI.Server;
using cc.isr.LXI.Client;

Console.WriteLine( $"VXI-11 {nameof( LxiInstrumentClient)} Tester" );

string ipv4Address = "192.168.0.144"; // "127.0.0.1";

bool ready = false;
while ( !ready )
{
    Console.Write( $"Enter IP Address, e.g., '127.0.0.1' [{ipv4Address}]: " );
    string? enteredIp = Console.ReadLine();
    ipv4Address = string.IsNullOrWhiteSpace( enteredIp ) ? ipv4Address : enteredIp;
    Console.WriteLine();
    Console.Write( $"Connect to {ipv4Address}? " );
    var yesno = Console.ReadKey();
    ready = yesno.KeyChar == 'y' || yesno.KeyChar == 'Y';
}

using LxiInstrumentClient instrument = new();

instrument.ThreadExceptionOccurred += OnThreadExcetion;

Console.WriteLine();
Console.Write( $"Press key to Connect to {ipv4Address}: " );
Console.ReadKey();

// client.connect("127.0.0.1", "inst0");
Console.WriteLine( $"Connecting to {ipv4Address}" );

instrument.Connect( ipv4Address, InsterfaceDeviceStringParser.BuildInterfaceDeviceString( InsterfaceDeviceStringParser.GenericInterfaceFamily, 0) );

if ( ipv4Address == "127.0.0.1" )
{
    string command = LxiInstrumentCommands.IDNRead;
    SendCommand( command );

    // closing client throws an exception when using the local mock server.
    // 
}
else
{
    string command = LxiInstrumentCommands.RST;
    SendCommand( command );

    command = LxiInstrumentCommands.CLS;
    SendCommand( command );

    command = "SYST:CLE";
    SendCommand( command );

    command = LxiInstrumentCommands.IDNRead;
    SendCommand( command );

    Console.WriteLine( $"closing {ipv4Address}" );

    instrument.Close();

}

Console.Write( "\n Press key to end" );
Console.ReadKey();

void SendCommand( string command )
{
    Console.WriteLine( $"Hit any key to send {command} to {ipv4Address}" );
    _ = Console.ReadKey();
    int sentCount = instrument.WriteLine( command );
    if ( sentCount == 0 )
        Console.WriteLine( $"{command} not sent" );
    else if ( instrument.IsQuery( command ) )
    {
        string response = instrument.Read();
        Console.WriteLine( $"{command} sent{(string.IsNullOrEmpty( response ) ? string.Empty : $"; received: {response}")}" );
    }
    else
        Console.WriteLine( $"{command} sent" );
}

static void OnThreadExcetion( object sender, ThreadExceptionEventArgs e )
{
    string name = "unknown";
    if ( sender is LxiInstrumentClient ) name = nameof( LxiInstrumentClient );

    Logger.Writer.LogError( $"{name} encountered an exception during an asynchronous operation", e.Exception );
}
