namespace Gym.Render;

public class NullEnvViewerSurface : IEnvViewerSurface
{
    private static readonly Lazy<IEnvViewerSurface> _Instance = new Lazy<IEnvViewerSurface>(() => new NullEnvViewerSurface());

    private NullEnvViewerSurface() { }

    public static EnvViewerSurfaceFactoryHandler Factory = Run;

    public static IEnvViewerSurface Instance => _Instance.Value;

    public void Render(SKSurface surface) { }
    public void Close() { }

    public static IEnvViewerSurface Run(int width, int height, string? title = null)
    {
        return Instance;
    }
}
