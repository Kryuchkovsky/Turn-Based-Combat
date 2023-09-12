using _GameLogic.Gameplay.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GameLogic.UI
{
    public class UnitInterface : MonoBehaviour
    {
        [SerializeField] private Image _hpBarFilling;
        [SerializeField] private TextMeshProUGUI _attackDamageText;
        [SerializeField] private TextMeshProUGUI _armorText;
        [SerializeField] private TextMeshProUGUI _armorPenetrationText;
        [SerializeField] private TextMeshProUGUI _vampirismText;
        [SerializeField] private TextMeshProUGUI _vampirismResistanceText;
        
        [field: SerializeField] public Button AttackButton { get; private set; }
        [field: SerializeField] public Button ApplyRandomBuffButton { get; private set; }

        public void SetHpBarFilling(float value) => _hpBarFilling.fillAmount = value;

        public void SetStats(UnitStats stats)
        {
            _attackDamageText.SetText("Attack damage: " + "{0:0}", stats.AttackDamage);
            _armorText.SetText("Armor: " + "{0:0}", stats.Armor);
            _armorPenetrationText.SetText("Armor penetration: " + "{0:0}", stats.ArmorPenetration);
            _vampirismText.SetText("Vampirism: " + "{0:0}", stats.Vampirism);
            _vampirismResistanceText.SetText("Vampirism resistrance: " + "{0:0}", stats.VampirismResistance);
        }
    }
}