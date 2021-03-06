using System;
using System.Diagnostics;
using System.IO.Ports;
using EStimSystems.B2.Internal;

namespace EStimSystems.B2
{
    public interface IB2Client : IDisposable
    {
        B2Response<B2Status> GetStatus();
        B2Response<B2Status> SetChannelALevel(int level);
        B2Response<B2Status> SetChannelBLevel(int level);
        B2Response<B2Status> SetChannelCLevel(int level);
        B2Response<B2Status> SetChannelDLevel(int level);
        B2Response<B2Status> SetPowerModeHigh();
        B2Response<B2Status> SetPowerModeLow();
        B2Response<B2Status> JoinChannels();
        B2Response<B2Status> UnJoinChannels();
        B2Response<B2Status> ResetChannelsABLevels();
        B2Response<B2Status> ResetToDefaults();
        B2Response<B2Status> SetMode(B2Mode mode);
    }

    public class B2Client : IB2Client
    {
        private readonly SerialPort _serialPort;
        private readonly IResponseDecoder _responseDecoder;
        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();

        public B2Client(SerialPort serialPort, IResponseDecoder responseDecoder)
        {
            _serialPort = serialPort;
            _responseDecoder = responseDecoder;
        }

        public B2Response<B2Status> GetStatus()
        {
            return Write("V");
        }

        public B2Response<B2Status> SetChannelALevel(int level)
        {
            // Axx   Sets Channel A Power % to xx. Range is 0 to 100

            if (level is < 0 or > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(level));
            }

            return Write($"A{level}");
        }

        public B2Response<B2Status> SetChannelBLevel(int level)
        {
            // Bxx   Sets Channel B Power % to xx. Range is 0 to 100

            if (level is < 0 or > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(level));
            }

            return Write($"B{level}");
        }

        public B2Response<B2Status> SetChannelCLevel(int level)
        {
            // Cxx   Sets Channel C Setting to xx. Range is 2 to 100

            if (level is < 2 or > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(level));
            }

            return Write($"C{level}");
        }

        public B2Response<B2Status> SetChannelDLevel(int level)
        {
            // Dxx   Sets Channel D Setting to xx. Range is 1 to 100

            if (level is < 1 or > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(level));
            }

            return Write($"D{level}");
        }

        public B2Response<B2Status> SetPowerModeHigh()
        {
            // H   Switch to High Power Mode, turns A/B back to 0%

            return Write("H");
        }

        public B2Response<B2Status> SetPowerModeLow()
        {
            // L   Switch to Lower Power Mode, turns A/B back to 0%

            return Write("L");
        }

        public B2Response<B2Status> JoinChannels()
        {
            // J   Join Channels A/B. A is master.

            return Write("L");
        }

        public B2Response<B2Status> UnJoinChannels()
        {
            // U   Unlink Channels A/B

            return Write("L");
        }

        public B2Response<B2Status> ResetChannelsABLevels()
        {
            // K   Set A/B to 0%

            return Write("K");
        }

        public B2Response<B2Status> ResetToDefaults()
        {
            // E   Set all Channels to defaults (A/B: 0%, C/D: 50, Mode: Pulse)

            return Write("E");
        }

        public B2Response<B2Status> SetMode(B2Mode mode)
        {
            // Mxx   Set mode to xx (See mode table)

            return Write($"M{mode:D}");
        }

        private B2Response<B2Status> Write(string command)
        {
            var start = _stopwatch.Elapsed;
            _serialPort.Write($"{command}{Constants.NewLine}");
            var response = _serialPort.ReadLine();
            var end = _stopwatch.Elapsed;

            var data = _responseDecoder.DecodeStatus(response);
            return new B2Response<B2Status>(end - start, data);
        }

        public void Dispose()
        {
            _serialPort.Dispose();
        }
    }
}