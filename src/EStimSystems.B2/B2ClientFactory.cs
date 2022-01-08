using System.Collections.Generic;
using System.IO.Ports;
using EStimSystems.B2.Internal;

namespace EStimSystems.B2;

public class B2ClientFactory
{
    public IB2Client CreateClient(string comPort)
    {
        // 9600/8/N/1
        var serialPort = new SerialPort(comPort, 9600, Parity.None, 8, StopBits.One)
        {
            ReadTimeout = 500,
            WriteTimeout = 500
        };

        serialPort.Open();
        return new B2Client(serialPort, new ResponseDecoder());
    }

    public IReadOnlyCollection<string> DiscoverComPorts()
    {
        return SerialPort.GetPortNames();
    }
}