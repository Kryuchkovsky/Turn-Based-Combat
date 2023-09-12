using UnityEngine;

namespace _GameLogic.Configs
{
    public abstract class BaseConfig : ScriptableObject
    {
        public bool IsInitiated { get; private set; }

        public virtual void Init()
        {
            IsInitiated = true;
        }
    }
}