
namespace Gym.Render;

public delegate IEnvViewerSurface EnvViewerSurfaceFactoryHandler(int width, int height, string? title = null);

public interface IEnvViewerSurface
{
    void Render(SKSurface surface);
    void Close();
}
