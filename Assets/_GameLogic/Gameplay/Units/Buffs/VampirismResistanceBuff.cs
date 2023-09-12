namespace _GameLogic.Gameplay.Units.Buffs
{
    public class VampirismResistanceBuff : BaseBuff
    {
        protected override void ChangeStats(ref UnitStats stats)
        {
            stats.VampirismResistance += 25;
        }
    }
}