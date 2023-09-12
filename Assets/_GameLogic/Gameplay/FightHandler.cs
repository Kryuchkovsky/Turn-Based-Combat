using System;
using _GameLogic.Gameplay.Units;
using _GameLogic.Gameplay.Units.Buffs;
using UnityEngine;

namespace _GameLogic.Gameplay
{
    public class FightHandler
    {
        public event Action<int> OnUnitDied;
        public event Action OnTurnStarted;
        public event Action OnBuffApplied;

        private const int MaxBuffs = 2;
        
        private BuffProvider _buffProvider;
        private bool _buffIsApplied;

        public UnitController LeftUnit { get; private set; }
        public UnitController RightUnit { get; private set; }
        public int TurningUnitId { get; private set; }

        public bool BuffCanBeApplied
        {
            get
            {
                var unit = TurningUnitId == 0 ? LeftUnit : RightUnit;
                return unit.BuffNumber < MaxBuffs && !_buffIsApplied;
            }
        }

        public void Init(UnitController leftUnit, UnitController rightUnit)
        {
            LeftUnit = leftUnit;
            RightUnit = rightUnit;
            _buffProvider = new BuffProvider();
        }

        public void Start()
        {
            TurningUnitId = 0;
            LeftUnit.OnDied += CompleteFight;
            RightUnit.OnDied += CompleteFight;
            OnTurnStarted?.Invoke();
        }

        public void MakeAttackCalculations()
        {
            var attackingUnit = TurningUnitId == 0 ? LeftUnit : RightUnit;
            var attackedUnit = TurningUnitId == 0 ? RightUnit : LeftUnit;
            var armorCoefficient = 1 - Mathf.Clamp01((attackedUnit.Stats.Armor - attackingUnit.Stats.ArmorPenetration) / 100f);
            var damage = attackingUnit.Stats.AttackDamage * armorCoefficient;
            var vampirismCoefficient = Mathf.Clamp01((attackingUnit.Stats.Vampirism - attackedUnit.Stats.VampirismResistance) / 100f);
            var healing = (int)(damage * vampirismCoefficient);

            if (damage > 0)
            {
                attackedUnit.ChangeHealthPoints(-(int)damage); 
            }
            
            if (healing > 0)
            {
                attackingUnit.ChangeHealthPoints(healing);
            }

            StartNextTurn();
        }

        public void ApplyRandomBuff()
        {
            var unit = TurningUnitId == 0 ? LeftUnit : RightUnit;
            
            if (!_buffIsApplied && unit.BuffNumber < MaxBuffs)
            {
                _buffProvider.ApplyRandomBuff(unit);
                _buffIsApplied = true;
                OnBuffApplied?.Invoke();
            }
        }

        private void CompleteFight(int diedUnitId)
        {
            TurningUnitId = 0;
            LeftUnit.OnDied -= CompleteFight;
            RightUnit.OnDied -= CompleteFight;
            OnUnitDied?.Invoke(diedUnitId);
        }

        private void StartNextTurn()
        {
            TurningUnitId += 1;
            
            if (TurningUnitId == 1)
            {
                LeftUnit.OnTurnCompleted();
            }
            else
            {
                RightUnit.OnTurnCompleted();
                TurningUnitId = 0;
            }

            _buffIsApplied = false;
            OnTurnStarted?.Invoke();
        }
    }
}