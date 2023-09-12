using System;
using _GameLogic.Configs;
using _GameLogic.Infrastructure;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _GameLogic.Gameplay.Battlefield
{
    public class ArenaGenerator
    {
        private readonly Transform _parentTransform;
        private ArenaView _arenaView;

        public ArenaGenerator(Transform parentTransform)
        {
            _parentTransform = parentTransform;
        }

        public ArenaView GetArena()
        {
            if (_arenaView == null)
            {
                Generate();
            }

            return _arenaView;
        }

        private void Generate()
        {
            if (ConfigManager.Instance.TryGetConfig<PrefabCatalog>(out var prefabCatalog))
            {
                var prefab = prefabCatalog.ArenaView;
                _arenaView = Object.Instantiate(prefab, _parentTransform);
                _arenaView.transform.localPosition = Vector3.zero;
            }
            else throw new Exception("The arena generation was failed!");
        }
    }
}