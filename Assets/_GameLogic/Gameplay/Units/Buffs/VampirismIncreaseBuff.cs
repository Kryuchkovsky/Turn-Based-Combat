namespace _GameLogic.Gameplay.Units.Buffs
{
    public class VampirismIncreaseBuff : BaseBuff
    {
        protected override void ChangeStats(ref UnitStats stats)
        {
            stats.Armor -= 25;
            stats.Vampirism += 50;
        }
    }
}