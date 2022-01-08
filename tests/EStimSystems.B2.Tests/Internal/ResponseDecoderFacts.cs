using EStimSystems.B2.Internal;
using FluentAssertions;
using Xunit;

namespace EStimSystems.B2.Tests.Internal
{
    public class ResponseDecoderFacts
    {
        [Fact]
        public void Given_a_valid_status_response_then_ResponseDecoder_should_return_a_B2Status()
        {
            const string response = "546:40:40:100:100:0:L:0:2.106";
            var decoder = new ResponseDecoder();

            // Act
            var result = decoder.DecodeStatus(response);

            // Assert
            result.Should().Be(new B2Status(
                new B2Level(91, "546"),
                new B2Level(20, "40"),
                new B2Level(20, "40"),
                new B2Level(50, "100"),
                new B2Level(50, "100"),
                B2Mode.Pulse,
                B2PowerSetting.Low,
                false,
                "2.106"
            ));
        }
    }
}