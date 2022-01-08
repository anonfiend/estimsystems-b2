using System;

namespace EStimSystems.B2.Internal
{
    public interface IResponseDecoder
    {
        B2Status DecodeStatus(string statusLine);
    }

    public class ResponseDecoder : IResponseDecoder
    {
        public B2Status DecodeStatus(string statusLine)
        {
            // AAA:BB:CC:DD:EE:F:G:H:II
            // AAA - Battery Level
            // BB - Channel A Level x2, i.e. value is twice what is set via commands and is displayed on the LCD
            // CC - Channel B Level x2
            // DD - Channel C Setting x2
            // EE - Channel D Setting x2
            // F - Current Mode
            // G - Power Setting (L or H)
            // H - Channel A/B Joined? (0 or 1)
            // II - Firmware Version

            var parts = statusLine.Split(new[] { ':' }, 9);
            if (parts.Length != 9)
            {
                throw new B2ProtocolException($"Failed to decode status response '{statusLine}'.");
            }

            return new B2Status(
                // Assuming this is in volts.
                // 1.50v (New) and < 0.90v (Empty)
                // The difference is 0.6v, raw value for a new battery is 600.
                // Just a guess, needs testing.
                GetLevel(parts[0], i => i / 6),
                // All these levels are twice their actual levels.
                GetLevel(parts[1], i => i / 2),
                GetLevel(parts[2], i => i / 2),
                GetLevel(parts[3], i => i / 2),
                GetLevel(parts[4], i => i / 2),
                (B2Mode)int.Parse(parts[5]),
                parts[6] switch
                {
                    "L" => B2PowerSetting.Low,
                    "H" => B2PowerSetting.High,
                    _ => throw new ArgumentOutOfRangeException()
                },
                parts[7] == "1",
                parts[8]
            );
        }

        private static B2Level GetLevel(string rawValue, Func<int, int> mutator)
        {
            var value = int.Parse(rawValue);
            var normalizedValue = mutator.Invoke(value);
            return new B2Level(normalizedValue, rawValue);
        }
    }
}