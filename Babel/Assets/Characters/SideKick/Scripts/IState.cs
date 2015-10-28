namespace Assets.Characters.SideKick.Scripts
{
    public interface IState
    {
        void ExecuteState();

        bool IsDoneExecuting();
    }
}
