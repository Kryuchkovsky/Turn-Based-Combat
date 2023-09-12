using System;
using System.Collections.Generic;
using System.Linq;
using _GameLogic.Gameplay.Units.Buffs;

namespace _GameLogic.Gameplay.Units
{
    public class UnitData
    {
        private const int DefaultAttackDamage = 15;
        private const int DefaultHealthPoints = 100;
        private readonly Dictionary<Type, BaseBuff> _buffs;
        private readonly UnitStats _baseUnitStats;
        private int _currentHealthPoints;

        public UnitStats CurrentUnitStats { get; private set; }
        public int BuffNumber => _buffs.Count; 

        public UnitData()
        {
            _buffs = new Dictionary<Type, BaseBuff>();
            _baseUnitStats = new UnitStats
            {
                AttackDamage = DefaultAttackDamage,
                HealthPoints = DefaultHealthPoints
            };
            Reset();
        }

        public void ChangeHealthPoints(int value)
        {
            _currentHealthPoints += value;
            CalculateCurrentStats();
        }

        public void AddBuff(BaseBuff baseBuff)
        {
            var type = baseBuff.GetType();

            if (!_buffs.ContainsKey(type))
            {
                _buffs.Add(type, baseBuff);
            }
            
            CalculateCurrentStats();
        }

        public float GetHealthPointPercent() => (float)_currentHealthPoints / _baseUnitStats.HealthPoints;
        public bool HasBuff<T>(T buffType) where T : BaseBuff => _buffs.ContainsKey(buffType.GetType());

        public void CalculateCurrentStats()
        {
            var stats = _baseUnitStats;
            stats.HealthPoints = _currentHealthPoints;

            foreach (var buff in _buffs.Values)
            {
                buff.Apply(ref stats);
            }

            CurrentUnitStats = stats;
        }

        public void OnTurnCompleted()
        {
            for (int i = 0; i < _buffs.Count; )
            {
                var buff = _buffs.ElementAt(i);
                buff.Value.ReduceDuration();

                if (buff.Value.Duration <= 0)
                {
                    _buffs.Remove(buff.Key);
                }
                else
                {
                    i++;
                }
            }
            
            CalculateCurrentStats();
        }

        public void Reset()
        {
            _buffs.Clear();
            _currentHealthPoints = DefaultHealthPoints;
            CalculateCurrentStats();
        }
    }
}