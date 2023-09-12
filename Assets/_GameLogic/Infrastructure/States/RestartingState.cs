namespace _GameLogic.Infrastructure.States
{
    public class RestartingState : BaseGameState
    {
        public RestartingState(GameStateHandler gameStateHandler) : base(gameStateHandler)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            GameStateHandler.UIContainer.RestartButton.onClick.AddListener(Reload);
            GameStateHandler.UIContainer.RestartButton.gameObject.SetActive(true);
            GameStateHandler.UIContainer.SetUnitInterfaceVisibility(false);
        }

        public override void OnExit()
        {
            base.OnExit();
            GameStateHandler.UIContainer.RestartButton.onClick.RemoveListener(Reload);
            GameStateHandler.UIContainer.RestartButton.gameObject.SetActive(false);
            GameStateHandler.UIContainer.SetUnitInterfaceVisibility(true);
        }

        private void Reload()
        {
            GameStateHandler.FightHandler.LeftUnit.Restore();
            GameStateHandler.FightHandler.RightUnit.Restore();
            GameStateHandler.ChangeState<FightState>();
        }
    }
}