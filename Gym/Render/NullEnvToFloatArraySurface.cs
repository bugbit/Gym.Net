namespace Gym.Render;

public class NullEnvToFloatArraySurface : IEnvToFloatArraySurface
{
    private static readonly Lazy<IEnvToFloatArraySurface> _Instance = new Lazy<IEnvToFloatArraySurface>(() => new NullEnvToFloatArraySurface());

    private NullEnvToFloatArraySurface() { }

    public static IEnvToFloatArraySurface Instance => _Instance.Value;

    public float[] ToFrameArray(SKSurface surface) => new float[0];
}
