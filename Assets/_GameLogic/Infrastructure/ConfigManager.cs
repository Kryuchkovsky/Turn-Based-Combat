using System;
using System.Collections.Generic;
using _GameLogic.Configs;
using _GameLogic.Extension;
using UnityEngine;

namespace _GameLogic.Infrastructure
{
    public class ConfigManager : Singleton<ConfigManager>
    {
        [SerializeField] private BaseConfig[] _configs;
        
        private Dictionary<Type, BaseConfig> _configsDictionary;

        public bool TryGetConfig<T>(out T config) where T : BaseConfig
        {
            var type = typeof(T);
            
            if (_configsDictionary.ContainsKey(type))
            {
                config = _configsDictionary[type] as T;
                return true;
            }

            config = default;
            return false;
        }

        protected override void Init()
        {
            base.Init();
            
            _configsDictionary = new Dictionary<Type, BaseConfig>();
            
            foreach (var config in _configs)
            {
                config.Init();
                var type = config.GetType();

                if (!_configsDictionary.ContainsKey(type))
                {
                    _configsDictionary.Add(type, config);
                }
            }
        }
    }
}