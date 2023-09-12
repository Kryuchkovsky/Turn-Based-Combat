namespace _GameLogic.Infrastructure.States
{
    public abstract class BaseGameState
    {
        protected readonly GameStateHandler GameStateHandler;

        protected BaseGameState(GameStateHandler gameStateHandler)
        {
            GameStateHandler = gameStateHandler;
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnExecute()
        {
        }

        public virtual void OnExit()
        {
        }
    }
}