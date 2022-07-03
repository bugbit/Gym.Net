namespace Gym.Envs;

public abstract class Env : IEnv
{
    protected SKSurface? SurfaceFrame;
    protected List<float[]>? RGBArray;
    protected Lazy<IEnvViewerSurface> ViewerSurface;
    protected IEnvToFloatArraySurface ToFloatArray;

    public bool UseRGBArray { get; }
    public IDictionary? Metadata { get; set; }
    public (float From, float To) RewardRange { get; set; }
    public Space? ActionSpace { get; set; }
    public Space? ObservationSpace { get; set; }

    public Env(bool useRGBArray = false, EnvViewerSurfaceFactoryHandler? viewerSurfaceFactory = null, IEnvToFloatArraySurface? toFloatArray = null)
    {
        UseRGBArray = useRGBArray;
        ViewerSurface = new Lazy<IEnvViewerSurface>(() => CreateViewerSurface(viewerSurfaceFactory ?? NullEnvViewerSurface.Factory));
        ToFloatArray = toFloatArray ?? NullEnvToFloatArraySurface.Instance;
    }

    public NDArray Reset()
    {
        ResetFrames();

        return ResetInternal();
    }

    public (NDArray state, float reward, bool done, IDictionary? info) Step(int action)
    {
        AddFrame();
        ResetFrame();

        return StepInternal(action);
    }

    public object? Render(RenderModes mode)
    {
        var ret = mode switch
        {
            RenderModes.Human => RenderHuman(),
            RenderModes.Skia => RenderSkia(),
            _ => null
        };

        if (UseRGBArray && SurfaceFrame == null)
            RenderSkia();

        return ret;
    }

    public object? RenderHuman()
    {
        var surface = RenderSurface();

        ViewerSurface.Value.Render(surface);

        return null;
    }

    public SKSurface RenderSkia() => (SurfaceFrame ??= RenderSurface());

    public virtual void Close()
    {
        ResetFrames();
        if (RGBArray != null)
            RGBArray = null;
        if (ViewerSurface.IsValueCreated)
            ViewerSurface.Value.Close();
    }

    public abstract void Seed(int seed);

    void IEnv.RenderHuman() => RenderHuman();

    protected abstract NDArray ResetInternal();
    protected abstract (NDArray state, float reward, bool done, IDictionary? info) StepInternal(int action);
    protected abstract IEnvViewerSurface CreateViewerSurface(EnvViewerSurfaceFactoryHandler handler);
    protected abstract SKSurface RenderSurface();

    protected void ResetFrames()
    {
        ResetFrame();
        if (UseRGBArray)
        {
            if (RGBArray != null)
            {
                RGBArray.Clear();
            }
            else
                RGBArray = new List<float[]>();
        }
    }

    protected void ResetFrame() => SurfaceFrame = null;

    protected void AddFrame()
    {
        if (SurfaceFrame != null)
        {
            if (UseRGBArray)
            {
                if (RGBArray != null)
                    RGBArray.Add(ToFloatArray.ToFrameArray(SurfaceFrame));
            }
        }
    }
}
