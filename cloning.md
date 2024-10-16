### Cloning

* [Source Code](#Source-Code)
  * [Repositories](#Repositories)
  * [Global Configuration Files](#Global-Configuration-Files)
  * [Packages](#Packages)

#### Source Code
Clone the repository along with its requisite repositories to their respective relative path.

##### Repositories
The repositories listed in [external repositories] are required:
* [IDE Repository] - IDE support files.
* [ISR'S XDR] - eXternal Data Representation.
* [ISR'S ONC RPC] - ONC/RPC.
* [ISR'S VXI-11] - VXI-11.
* [ISR'S LXI] - LXI.

```
git clone git@bitbucket.org:davidhary/vs.ide.git
git clone https://github.com/ATECoder/dn.xdr.git
git clone https://github.com/ATECoder/dn.onc.rpc.git
git clone https://github.com/ATECoder/dn.vxi11.git
git clone https://github.com/ATECoder/dn.lxi.git
```

Clone the repositories into the following folders (parents of the .git folder):
```
%vslib%\core\ide
%dnlib%\iot\xdr
%dnlib%\iot\oncrpc
%dnlib%\iot\vxi
%dnlib%\iot\lxi
```
where %dnlib% and %vslib% are  the root folders of the .NET libraries, e.g., %my%\lib\vs 
and %my%\libraries\vs, respectively, and %my% is the root folder of the .NET solutions

##### Global Configuration Files
ISR libraries use a global editor configuration file and a global test Runs settings file. 
These files can be found in the [IDE Repository].

Restoring Editor Configuration:
```
xcopy /Y %my%\.editorconfig %my%\.editorconfig.bak
xcopy /Y %vslib%\core\ide\code\.editorconfig %my%\.editorconfig
```

Restoring Runs Settings:
```
xcopy /Y %userprofile%\.runsettings %userprofile%\.runsettings.bak
xcopy /Y %vslib%\core\ide\code\.runsettings %userprofile%\.runsettings
```
where %userprofile% is the root user folder.

##### Packages
TBA

[external repositories]: ExternalReposCommits.csv

[IDE Repository]: https://www.bitbucket.org/davidhary/vs.ide
[ISR's ONC RPC]: https://github.com/ATECoder/dn.onc.rpc
[ISR's XDR]: https://github.com/ATECoder/dn.xdr
[ISR's VXI-11]: https://github.com/ATECoder/dn.vxi11
[ISR's LXI]: https://github.com/ATECoder/dn.lxi


[LXI]: https://www.lxistandard.org/About/AboutLXI.aspx

[Python VXI-11]: https://github.com/alexforencich/python-vxi11.git 

[VXI Bus Specification]: https://vxibus.org/specifications.html
[Sun RPC]: https://en.wikipedia.org/wiki/Sun_RPC
[XDR: External Data Representation Standard (May 2006)]: http://tools.ietf.org/html/rfc4506

[VXI11.CSharp]: https://github.com/Xanliang/VXI11.CSharp 
[Jay Walter's SourceForge repository]: https://sourceforge.net/p/remoteteanet
[Wes Day's RemoteTea.Net]: https://github.com/wespday/RemoteTea.Net
[GB1.RemoteTea.Net]: https://github.com/galenbancroft/RemoteTea.Net
[org.acplt.oncrpc package]: https://people.eecs.berkeley.edu/~jonah/javadoc/org/acplt/oncrpc/package-summary.html
[Java ONC RPC]: https://github.com/remotetea/remotetea/tree/master/src/tests/org/acplt/oncrpc

[Jay Walter's SourceForge repository]: https://sourceforge.net/p/remoteteanet
[Wes Day's RemoteTea.Net]: https://github.com/wespday/RemoteTea.Net
[GB1.RemoteTea.Net]: https://github.com/galenbancroft/RemoteTea.Net
[org.acplt.oncrpc package]: https://people.eecs.berkeley.edu/~jonah/javadoc/org/acplt/oncrpc/package-summary.html
[Java ONC RPC]: https://github.com/remotetea/remotetea/tree/master/src/tests/org/acplt/oncrpc
[VXI11.CSharp]: https://github.com/Xanliang/VXI11.CSharp 
[VXI Bus Specification]: https://vxibus.org/specifications.html

