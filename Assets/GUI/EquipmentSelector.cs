using Mayhem.Equipment;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mayhem.GUI
{
    public class EquipmentSelector
    {
        private readonly GameObject EMPTY_TILE;

        private List<GameObject> m_ShownTiles;

        private EquipmentBag m_AvailableEquipment;

        private RectTransform m_SelectionZone;
        private Transform m_LayoutGroupTransform;

        private EquipmentType m_EquipmentType;

        public EquipmentSelector(RectTransform selectionZone, Transform layoutGroupTransform, GameObject emptyTile, EquipmentType type)
        {
            m_SelectionZone = selectionZone;
            m_ShownTiles = new List<GameObject>();
            m_LayoutGroupTransform = layoutGroupTransform;
            EMPTY_TILE = emptyTile;
            m_EquipmentType = type;
        }

        public void Update()
        {
            if (PhotonNetwork.inRoom)
            {
                if (m_AvailableEquipment == null)
                {
                    m_AvailableEquipment = Entities.Player.FindLocalPlayer().GetEquipmentBag(m_EquipmentType);
                    m_AvailableEquipment.EquipmentAddedRemoved += m_PlayerAvailableEquipmentChanged;
                    updateEquipmentIcons();
                    setSelectedEquipmentIcon();
                }

                if (Application.isMobilePlatform)
                {
                    handleMobileInput();
                }
                else
                {
                    handleDesktopInput();
                }
            }
        }

        void handleMobileInput()
        {
            foreach (var touch in UnityEngine.Input.touches)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(m_SelectionZone, touch.position) == false)
                {
                    continue;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    for (int i = 0; i < m_ShownTiles.Count; i++)
                    {
                        if (RectTransformUtility.RectangleContainsScreenPoint(m_ShownTiles[i].GetComponent<RectTransform>(), touch.position))
                        {
                            bool validWeaponChange = m_AvailableEquipment.ChangeObjectToIndex(i);
                            if (validWeaponChange && m_AvailableEquipment.GetCurrentSelectedObject().GetUsageType() != UsageType.OneTime)
                            {
                                setSelectedEquipmentIcon();
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void handleDesktopInput()
        {
            for (int i = (int)KeyCode.Alpha1; i < (int)KeyCode.Alpha1 + m_AvailableEquipment.Contents.Length; i++)
            {
                if (UnityEngine.Input.GetKeyDown((KeyCode)i))
                {
                    bool validWeaponChange = m_AvailableEquipment.ChangeObjectToIndex(i - (int)KeyCode.Alpha1); // (int)KeyCode.Alpha1 is subtracted as the keycodes start at 49. Subtracting gives a number starting at 0
                    if (validWeaponChange && m_AvailableEquipment.GetCurrentSelectedObject().GetUsageType() != UsageType.OneTime)
                    {
                        setSelectedEquipmentIcon();
                    }
                }
            }
        }

        private void setSelectedEquipmentIcon()
        {
            if (m_AvailableEquipment.HasSelectedSomething())
            {
                string Iconpath = m_AvailableEquipment.GetCurrentSelectedObject().GetIconPath();

                for (int i = 0; i < m_ShownTiles.Count; i++)
                {

                    if (m_ShownTiles[i].GetComponent<Image>().sprite.name.Equals(Iconpath))
                    {
                        m_ShownTiles[i].GetComponent<Image>().color = Color.red;
                    }
                    else
                    {
                        m_ShownTiles[i].GetComponent<Image>().color = Color.white;
                    }

                }
            }
        }

        private void m_PlayerAvailableEquipmentChanged(object sender)
        {
            updateEquipmentIcons();
        }

        private void updateEquipmentIcons()
        {
            m_ShownTiles.ForEach(m => GameObject.Destroy(m));
            m_ShownTiles.Clear();

            // Add all required weapons 
            for (int i = 0; i < m_AvailableEquipment.Contents.Length; i++)
            {
                GameObject weaponTile = GameObject.Instantiate(EMPTY_TILE, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

                weaponTile.GetComponent<Image>().sprite = Resources.Load<Sprite>(m_AvailableEquipment.Contents[i].GetIconPath());

                weaponTile.SetActive(true);
                weaponTile.transform.SetParent(m_LayoutGroupTransform);
                m_ShownTiles.Add(weaponTile);
            }

            // Add empty tiles as padding
            for (int i = m_ShownTiles.Count; i < EquipmentBag.MAX_OBJECT_COUNT; i++)
            {
                GameObject weaponTile = GameObject.Instantiate(EMPTY_TILE, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

                weaponTile.SetActive(true);
                weaponTile.transform.SetParent(m_LayoutGroupTransform);
                m_ShownTiles.Add(weaponTile);
            }
        }
    }
}
