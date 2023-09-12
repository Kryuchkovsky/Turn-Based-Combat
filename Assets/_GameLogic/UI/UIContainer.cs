using UnityEngine;
using UnityEngine.UI;

namespace _GameLogic.UI
{
    public class UIContainer : MonoBehaviour
    {
        [field: SerializeField] public UnitInterface LeftUnitInterface { get; private set; }
        [field: SerializeField] public UnitInterface RightUnitInterface { get; private set; }
        [field: SerializeField] public Button RestartButton { get; private set; }

        public void SetUnitInterfaceVisibility(bool isVisible)
        {
            LeftUnitInterface.gameObject.SetActive(isVisible);
            RightUnitInterface.gameObject.SetActive(isVisible);
        }
    }
}