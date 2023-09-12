namespace _GameLogic.Gameplay.Units.Buffs
{
    public class ArmorPenetrationBuff : BaseBuff
    {
        protected override void ChangeStats(ref UnitStats stats)
        {
            stats.ArmorPenetration += 10;
        }
    }
}