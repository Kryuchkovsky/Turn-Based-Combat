namespace _GameLogic.Gameplay.Units.Buffs
{
    public class ArmorReinforcementBuff : BaseBuff
    {
        protected override void ChangeStats(ref UnitStats stats)
        {
            stats.Armor += 50;
        }
    }
}