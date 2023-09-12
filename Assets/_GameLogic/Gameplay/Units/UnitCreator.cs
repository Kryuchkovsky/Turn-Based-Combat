using System;
using _GameLogic.Configs;
using _GameLogic.Gameplay.Battlefield;
using _GameLogic.Infrastructure;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _GameLogic.Gameplay.Units
{
    public class UnitCreator
    {
        private readonly Transform _unitContainer;

        public UnitCreator(Transform unitContainer)
        {
            _unitContainer = unitContainer;
        }
        
        public UnitController Create(ArenaView arenaView, int id)
        {
            if (ConfigManager.Instance.TryGetConfig<PrefabCatalog>(out var catalog))
            {
                var position = id == 0 ? arenaView.LeftSpawnPoint.position : arenaView.RightSpawnPoint.position;
                var rotation = Quaternion.LookRotation(arenaView.transform.position - position);
                var createdRepresentation = Object.Instantiate(catalog.Unit, position, rotation, _unitContainer);
                return new UnitController(createdRepresentation, new UnitData(), id);
            }
            
            throw new Exception("The unit creation was failed!");
        }
    }
}