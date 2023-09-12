using _GameLogic.Gameplay.Battlefield;
using _GameLogic.Gameplay.Units;
using UnityEngine;

namespace _GameLogic.Configs
{
    [CreateAssetMenu(menuName = "Configs/PrefabCatalog", fileName = "PrefabCatalog")]
    public class PrefabCatalog : BaseConfig
    {
        [field: SerializeField] public ArenaView ArenaView { get; private set; }
        [field: SerializeField] public UnitRepresentation Unit { get; private set; }
    }
}