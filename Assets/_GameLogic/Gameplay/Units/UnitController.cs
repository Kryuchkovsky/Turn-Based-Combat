using System;
using _GameLogic.Gameplay.Units.Buffs;

namespace _GameLogic.Gameplay.Units
{
    public class UnitController
    {
        public event Action<UnitStats> OnStatsChanged;
        public event Action<int> OnDied;
        public event Action<float> OnHealthPointsPercentChanged;

        private readonly UnitRepresentation _representation;
        private readonly UnitData _data;
        private readonly int _id;

        public UnitStats Stats => _data.CurrentUnitStats;
        public int BuffNumber => _data.BuffNumber;

        public UnitController(UnitRepresentation representation, UnitData data, int id)
        {
            _representation = representation;
            _data = data;
            _id = id;
        }

        public void AddBuff(BaseBuff baseBuff)
        {
            _data.AddBuff(baseBuff);
            OnStatsChanged?.Invoke(_data.CurrentUnitStats);
        }
        
        public bool HasBuff<T>(T buffType) where T : BaseBuff => _data.HasBuff<T>(buffType);

        public void OnTurnCompleted()
        {
            _data.OnTurnCompleted();
            OnStatsChanged?.Invoke(_data.CurrentUnitStats);
        }

        public void ChangeHealthPoints(int value)
        {
            _data.ChangeHealthPoints(value);
            OnHealthPointsPercentChanged?.Invoke(_data.GetHealthPointPercent());
            var isAlive = _data.CurrentUnitStats.HealthPoints > 0;
            _representation.SetModelVisibility(isAlive);

            if (!isAlive)
            {
                OnDied?.Invoke(_id);
            }
            else if (value < 0)
            {
                _representation.OnDamageGot();
            }
        }

        public void Restore()
        {
            _data.Reset();
            _representation.SetModelVisibility(true);
            OnHealthPointsPercentChanged?.Invoke(_data.GetHealthPointPercent());
            OnStatsChanged?.Invoke(_data.CurrentUnitStats);
        }
    }
}