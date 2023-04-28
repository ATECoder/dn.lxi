### LXI - A standard for LAN equipped instrumentation 


#### Syntax of the Device Address
LXI IVI Drivers shall accept VISA resource names.  

The IVI driver provided with an LXI Device shall accept any valid VISA resource name as the network resource location as described in this section.  

Specifically, valid VISA resource names for LXI Devices are:  
* TCPIP[board]::host address[::LAN device name][::INSTR]
* TCPIP[board]::host address::port::SOCKET
* TCPIP[board]::host address[::HiSLIP device name[,HiSLIP
port]][::INSTR]
* TCPIP[board]::credential information@host address::HiSLIP device
name[,HiSLIP port]][::INSTR]
* TCPIP[board]::host address::USB[board]::manufacturer ID::model code::serial number[::USB interface number]::INSTR

Where:
* _board_ is an integer representing a physical network interface card in the computer;
* _host address_ is either a host name or IP address (4 bytes in decimal separated by “.”);
* _“INSTR”_ is the resource class. It implies a protocol that supports read, write, trigger, status, and clear;
* _“SOCKET”_ is the resource class. It implies a protocol based on a raw TCP/IP connection
that may only support read/write;
* _HiSLIP device name indicates the sub-address of the HiSLIP server within the device. It
begins with _‘hislip’_. _’hislip0’_ is typically used when there is only a single sub-address.
* _HiSLIP port_ is the port number to use for connections, the default value shall be the
IANA assigned port 4880.
* _credential information_ specifies to the driver what credentials to use to securely connect to the driver. The way that drivers acquire the credentials from this token is driver-specific. The credential information may be preceded by a hash (#) or dollar sign ($).

For details of the use of this information, see VPP4.3.

Although VISA does not specify that the data being read/written to the device is an ASCII
instrument control language (such as SCPI), it is implied by the INSTR and SOCKET resource
classes.  

If the driver supports control of the device via either the SOCKET or INSTR protocols, the driver shall use the specified protocol, unless a subsequent driver call or initialization string alters that behavior.  

The driver shall choose the most appropriate protocol for controlling that device. For the INSTR resource class the LXI Device name may be used to specify a port. If the IP port, the LXI Device name, or resource class is not relevant for that protocol, the driver shall ignore the irrelevant parameters.

Note that this resource descriptor may be passed directly by the customer to the open call or it may be extracted from the IVI Configuration Store.

#### SCPI Is Not Required
The LXI spec does not require an underlying SCPI interface to the device. LXI presumes the primary control interface is IVI. The actual communication between the driver and the device is at the discretion of the device designer to optimize the performance and price of the device.

##### References

[ICS Electronics]: https://www.icselect.com/
[LXI]: https://www.lxistandard.org/About/AboutLXI.aspx
[VXI Consortium]: http://www.vxibus.org
[VXI-11 Specifications]: https://vxibus.org/specifications.html
[RPC]: https://en.wikipedia.org/wiki/Sun_RPC
[Making sense of T&M protocols]: https://tomverbeure.github.io/2020/06/07/Making-Sense-of-Test-and-Measurement-Protocols.html
[ONC Remote Procedure Call (RPC)]: https://en.wikipedia.org/wiki/Open_Network_Computing_Remote_Procedure_Call
[Visa Resource Name]: https://www.ni.com/docs/en-US/bundle/labview/page/lvinstio/visa_resource_name_generic.html
