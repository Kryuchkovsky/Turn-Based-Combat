using Random = System.Random;

namespace _GameLogic.Gameplay.Units.Buffs
{
    public class BuffProvider
    {
        private readonly BaseBuff[] _buffs;
        private readonly Random _random;

        public BuffProvider()
        {
            _buffs = new BaseBuff[]
            {
                new DoubleDamageBuff(),
                new ArmorReinforcementBuff(),
                new ArmorPenetrationBuff(),
                new VampirismIncreaseBuff(),
                new VampirismResistanceBuff()
            };
            _random = new Random();
        }

        public void ApplyRandomBuff(UnitController unitController)
        {
            ShuffleBuffs();
            
            for (int i = 0; i < _buffs.Length; i++)
            {
                if (unitController.HasBuff(_buffs[i])) continue;
                
                unitController.AddBuff(_buffs[i].Clone() as BaseBuff);
                return;
            }
        }

        private void ShuffleBuffs()
        {
            for (int i = _buffs.Length - 1; i >= 1; i--)
            {
                var j = _random.Next(i + 1);
                (_buffs[j], _buffs[i]) = (_buffs[i], _buffs[j]);
            }
        }
    }
}