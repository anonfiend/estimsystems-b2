using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EStimSystems.B2
{
    public static class B2ClientExtensions
    {
        public static B2Response<B2Snapshot> GetSnapshot(this IB2Client client)
        {
            var (timeSpan, status) = client.GetStatus();

            return new B2Response<B2Snapshot>(
                timeSpan,
                new B2Snapshot(
                    status.ChannelALevel.Percentage,
                    status.ChannelBLevel.Percentage,
                    status.ChannelCLevel.Percentage,
                    status.ChannelDLevel.Percentage,
                    status.Mode,
                    status.PowerSetting,
                    status.AreChannelsJoined
                )
            );
        }

        public static B2Response<B2Snapshot> RestoreSnapshot(this IB2Client client, B2Snapshot snapshot)
        {
            var (timeSpan, currentStatus) = client.GetStatus();

            if (currentStatus.ChannelALevel.Percentage != snapshot.ChannelALevel)
            {
                client.SetChannelALevel(snapshot.ChannelALevel);
            }

            if (currentStatus.ChannelBLevel.Percentage != snapshot.ChannelBLevel)
            {
                client.SetChannelBLevel(snapshot.ChannelBLevel);
            }

            if (currentStatus.ChannelCLevel.Percentage != snapshot.ChannelCLevel)
            {
                client.SetChannelCLevel(snapshot.ChannelCLevel);
            }

            if (currentStatus.ChannelDLevel.Percentage != snapshot.ChannelDLevel)
            {
                client.SetChannelDLevel(snapshot.ChannelDLevel);
            }

            if (currentStatus.Mode != snapshot.Mode)
            {
                client.SetMode(snapshot.Mode);
            }

            if (currentStatus.PowerSetting != snapshot.PowerSetting)
            {
                client.SetPowerMode(snapshot.PowerSetting);
            }

            if (currentStatus.AreChannelsJoined != snapshot.AreChannelsJoined)
            {
                client.SetChannelsJoined(snapshot.AreChannelsJoined);
            }

            return new B2Response<B2Snapshot>(
                timeSpan,
                new B2Snapshot(
                    currentStatus.ChannelALevel.Percentage,
                    currentStatus.ChannelBLevel.Percentage,
                    currentStatus.ChannelCLevel.Percentage,
                    currentStatus.ChannelDLevel.Percentage,
                    currentStatus.Mode,
                    currentStatus.PowerSetting,
                    currentStatus.AreChannelsJoined
                )
            );
        }

        public static async Task<B2Response> ExecuteAsync(this IB2Client client, Func<IB2Client, Task> func)
        {
            var stopwatch = Stopwatch.StartNew();
            var (_, snapshot) = client.GetSnapshot();
            try
            {
                await func.Invoke(client);
            }
            finally
            {
                client.RestoreSnapshot(snapshot);
            }

            return new B2Response(stopwatch.Elapsed);
        }

        public static B2Response<B2Status> SetPowerMode(this IB2Client client, B2PowerSetting powerSetting)
        {
            return powerSetting switch
            {
                B2PowerSetting.High => client.SetPowerModeHigh(),
                B2PowerSetting.Low => client.SetPowerModeLow(),
                _ => throw new ArgumentOutOfRangeException(nameof(powerSetting), powerSetting, null)
            };
        }

        public static B2Response<B2Status> SetChannelsJoined(this IB2Client client, bool areChannelsJoined)
        {
            return areChannelsJoined switch
            {
                true => client.JoinChannels(),
                false => client.UnJoinChannels()
            };
        }
    }
}