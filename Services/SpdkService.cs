namespace HomeLabPortal.Services;

public class SpdkService(Spdk.SpdkClient spdkClient)
{
    public async Task<SpdkVersion> GetVersion()
    {
        var response = await spdkClient.GetSpdkVersionAsync(new Google.Protobuf.WellKnownTypes.Empty());
        return response ?? new SpdkVersion
        {
            Version = "Unknown"
        };
    }
}