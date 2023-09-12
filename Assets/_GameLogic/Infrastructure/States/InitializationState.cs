namespace _GameLogic.Infrastructure.States
{
    public class InitializationState : BaseGameState
    {
        public InitializationState(GameStateHandler gameStateHandler) : base(gameStateHandler)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            var arena = GameStateHandler.ArenaGenerator.GetArena();
            var leftUnit = GameStateHandler.UnitCreator.Create(arena, 0);
            var rightUnit = GameStateHandler.UnitCreator.Create(arena, 1);
            GameStateHandler.FightHandler.Init(leftUnit, rightUnit);
            GameStateHandler.UIContainer.RestartButton.gameObject.SetActive(false);
            GameStateHandler.ChangeState<FightState>();
        }
    }
}