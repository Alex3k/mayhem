using Mayhem.Equipment;
using UnityEngine;

namespace Mayhem.GUI
{
    public class EquipmentSelectionManager :MonoBehaviour
    {
        private EquipmentType m_CurrentContainer;
        public GameObject WeaponContainer;
        public GameObject ItemContainer;

        public void ChangeContainer()
        {
            if (m_CurrentContainer == EquipmentType.Item)
            {
                ItemContainer.SetActive(false);
                WeaponContainer.SetActive(true);
                m_CurrentContainer = EquipmentType.Weapon;
            }
            else
            {
                ItemContainer.SetActive(true);
                WeaponContainer.SetActive(false);
                m_CurrentContainer = EquipmentType.Item;
            }
        }

        void Update()
        {
            if(Application.isMobilePlatform == false)
            {
                if (UnityEngine.Input.GetKeyDown(KeyCode.Tab))
                {
                    ChangeContainer();
                }
            }
        }
    }
}
