using Mayhem.Equipment;
using UnityEngine;
using UnityEngine.UI;

namespace Mayhem.GUI
{
    public class WeaponSelector : MonoBehaviour
    {
        public GameObject EMPTY_TILE;

        private EquipmentSelector m_WeaponSelector;

        void Start()
        {
            m_WeaponSelector = new EquipmentSelector(transform.FindChild("IconCollection").GetComponent<RectTransform>(), GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform, EMPTY_TILE, EquipmentType.Weapon);
        }

        void Update()
        {
            m_WeaponSelector.Update();
        }
    }
}
