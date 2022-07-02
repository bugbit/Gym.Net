namespace Gym.Render;

public class NullEnvViewerSurface : IEnvViewerSurface
{
    private static readonly Lazy<IEnvViewerSurface> _Instance = new Lazy<IEnvViewerSurface>(() => new NullEnvViewerSurface());

    private NullEnvViewerSurface() { }

    public static IEnvViewerSurface Instance => _Instance.Value;

    public void Render(SKSurface surface) { }
    public void Close() { }
}
