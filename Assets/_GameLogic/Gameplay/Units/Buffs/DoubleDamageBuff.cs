namespace _GameLogic.Gameplay.Units.Buffs
{
    public class DoubleDamageBuff : BaseBuff
    {
        protected override void ChangeStats(ref UnitStats stats)
        {
            stats.AttackDamage *= 2;
        }
    }
}