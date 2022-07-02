namespace Gym.Envs;

public interface IEnv
{
    bool UseRGBArray { get; }
    IDictionary? Metadata { get; set; }
    (float From, float To) RewardRange { get; set; }
    Space? ActionSpace { get; set; }
    Space? ObservationSpace { get; set; }
    NDArray Reset();
    (NDArray state, float reward, bool done, IDictionary? info) Step(int action);
    object? Render(RenderModes mode);
    SKSurface RenderSkia();
    void RenderHuman();
    void Close();
    void Seed(int seed);
}