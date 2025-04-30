namespace HomeLabDashboard.Services;

public class SpdkService(Spdk.SpdkClient spdkClient)
{
    public async Task<SpdkVersion> SpdkGetVersion()
    {
        var response = await spdkClient.SpdkGetVersionAsync(new Google.Protobuf.WellKnownTypes.Empty());
        return response ?? new SpdkVersion
        {
            Version = "Unknown"
        };
    }

    public async Task<FrameworkReactors> FrameworkGetReactors()
    {
        var response = await spdkClient.FrameworkGetReactorsAsync(new Google.Protobuf.WellKnownTypes.Empty());
        return response ?? new FrameworkReactors
        {
            Reactors = { }
        };
    }

    public async Task<NvmfSubsystems> NvmfGetSubsystems()
    {
        var response = await spdkClient.NvmfGetSubsystemsAsync(new Google.Protobuf.WellKnownTypes.Empty());
        return response ?? new NvmfSubsystems
        {
            Subsystems = { }
        };
    }
}