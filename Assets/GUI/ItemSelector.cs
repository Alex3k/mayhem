using Mayhem.Equipment;
using Mayhem.GUI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GUI
{
    public class ItemSelector : MonoBehaviour
    {
        public GameObject EMPTY_TILE;

        private EquipmentSelector m_ItemSelector;

        void Start()
        {
            m_ItemSelector = new EquipmentSelector(transform.FindChild("IconCollection").GetComponent<RectTransform>(), GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform, EMPTY_TILE, EquipmentType.Item);
        }

        void Update()
        {
            m_ItemSelector.Update();
        }
    }
}
