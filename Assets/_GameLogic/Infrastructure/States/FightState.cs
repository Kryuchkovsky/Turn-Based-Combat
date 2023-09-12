using _GameLogic.Gameplay.Units;
using _GameLogic.UI;

namespace _GameLogic.Infrastructure.States
{
    public class FightState : BaseGameState
    {
        public FightState(GameStateHandler gameStateHandler) : base(gameStateHandler)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Link(GameStateHandler.FightHandler.LeftUnit, GameStateHandler.UIContainer.LeftUnitInterface);
            Link(GameStateHandler.FightHandler.RightUnit, GameStateHandler.UIContainer.RightUnitInterface);
            GameStateHandler.UIContainer.SetUnitInterfaceVisibility(true);
            GameStateHandler.FightHandler.OnTurnStarted += UpdateUnitInterfaceActivity;
            GameStateHandler.FightHandler.OnBuffApplied += UpdateUnitInterfaceActivity;
            GameStateHandler.FightHandler.OnUnitDied += OnUnitDied;
            GameStateHandler.FightHandler.Start();
            UpdateStatsDisplay();
        }

        public override void OnExit()
        {
            base.OnExit();
            Unlink(GameStateHandler.FightHandler.LeftUnit, GameStateHandler.UIContainer.LeftUnitInterface);
            Unlink(GameStateHandler.FightHandler.RightUnit, GameStateHandler.UIContainer.RightUnitInterface);
            GameStateHandler.UIContainer.LeftUnitInterface.SetHpBarFilling(1);
            GameStateHandler.UIContainer.RightUnitInterface.SetHpBarFilling(1);
            GameStateHandler.FightHandler.OnTurnStarted -= UpdateUnitInterfaceActivity;
            GameStateHandler.FightHandler.OnBuffApplied -= UpdateUnitInterfaceActivity;
            GameStateHandler.FightHandler.OnUnitDied -= OnUnitDied;
        }

        private void OnUnitDied(int id)
        {
            GameStateHandler.UIContainer.SetUnitInterfaceVisibility(false);
            GameStateHandler.ChangeState<RestartingState>();
        }

        private void Link(UnitController unitController, UnitInterface unitInterface)
        {
            unitInterface.AttackButton.onClick.AddListener(GameStateHandler.FightHandler.MakeAttackCalculations);
            unitInterface.ApplyRandomBuffButton.onClick.AddListener(GameStateHandler.FightHandler.ApplyRandomBuff);
            unitController.OnStatsChanged += unitInterface.SetStats;
            unitController.OnHealthPointsPercentChanged += unitInterface.SetHpBarFilling;
        }
        
        private void Unlink(UnitController unitController, UnitInterface unitInterface)
        {
            unitInterface.AttackButton.onClick.RemoveListener(GameStateHandler.FightHandler.MakeAttackCalculations);
            unitInterface.ApplyRandomBuffButton.onClick.RemoveListener(GameStateHandler.FightHandler.ApplyRandomBuff);
            unitController.OnStatsChanged -= unitInterface.SetStats;
            unitController.OnHealthPointsPercentChanged -= unitInterface.SetHpBarFilling;
        }

        private void UpdateStatsDisplay()
        {
            GameStateHandler.UIContainer.LeftUnitInterface.SetStats(GameStateHandler.FightHandler.LeftUnit.Stats);
            GameStateHandler.UIContainer.RightUnitInterface.SetStats(GameStateHandler.FightHandler.RightUnit.Stats);
        }

        private void UpdateUnitInterfaceActivity()
        {
            var unitId = GameStateHandler.FightHandler.TurningUnitId;
            GameStateHandler.UIContainer.LeftUnitInterface.AttackButton.interactable = unitId == 0;
            GameStateHandler.UIContainer.LeftUnitInterface.ApplyRandomBuffButton.interactable = unitId == 0 && GameStateHandler.FightHandler.BuffCanBeApplied;
            GameStateHandler.UIContainer.RightUnitInterface.AttackButton.interactable = unitId == 1;
            GameStateHandler.UIContainer.RightUnitInterface.ApplyRandomBuffButton.interactable = unitId == 1 && GameStateHandler.FightHandler.BuffCanBeApplied;
        }
    }
}