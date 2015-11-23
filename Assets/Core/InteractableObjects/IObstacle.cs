namespace Assets.Core.InteractableObjects
{
    public interface IObstacle
    {
        /// <summary>
        /// Used whenever a i.e. all the levers has been pulled
        /// </summary>
        void ChallengesDone();

        /// <summary>
        /// Used when that no longer is the case
        /// </summary>
        void ChallengesUnDone();
    }
}
