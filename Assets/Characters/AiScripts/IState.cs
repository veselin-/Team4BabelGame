namespace Assets.Characters.AiScripts
{
    public interface IState
    {
        float WaitingTime { get; set; }

        void ExecuteState();

        bool IsDoneExecuting();
    }
}
