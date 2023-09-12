using System;

namespace _GameLogic.Gameplay.Units.Buffs
{
    public abstract class BaseBuff : ICloneable
    {
        public virtual int BaseDuration => 2;
        public int Duration { get; private set; }
        
        public BaseBuff()
        {
            Duration = BaseDuration;
        }

        protected abstract void ChangeStats(ref UnitStats stats);

        public void Apply(ref UnitStats stats)
        {
            if (Duration <= 0) return;
            
            ChangeStats(ref stats);
        }

        public void ReduceDuration() => Duration -= 1;
        
        public object Clone() => MemberwiseClone();
    }
}