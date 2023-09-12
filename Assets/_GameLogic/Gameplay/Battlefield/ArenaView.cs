using UnityEngine;

namespace _GameLogic.Gameplay.Battlefield
{
    public class ArenaView : MonoBehaviour
    {
        [field: SerializeField] public Transform LeftSpawnPoint { get; private set; } 
        [field: SerializeField] public Transform RightSpawnPoint { get; private set; }
    }
}