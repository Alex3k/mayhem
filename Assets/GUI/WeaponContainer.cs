using Mayhem.Weaponary;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mayhem.GUI
{
    public class WeaponContainer : MonoBehaviour
    {
        public GameObject EMPTY_WEAPON_TILE;

        private List<GameObject> m_ShownWeaponTiles;

        private WeaponBag m_PlayerAvailableWeapons;

        private RectTransform m_SwipeZone;

        private Vector2 m_SwipeStartPosition;
        private float m_SwipeMinimumRegistrationDistance = 30f;
        private bool m_IsSwiping = false;

        void Start()
        {
            m_SwipeZone = transform.FindChild("SwipeZone").GetComponent<RectTransform>();
            m_ShownWeaponTiles = new List<GameObject>();
        }

        void Update()
        {
            if (PhotonNetwork.inRoom)
            {
                if (m_PlayerAvailableWeapons == null)
                {
                    m_PlayerAvailableWeapons = Entities.Player.FindLocalPlayer().WeaponBag;
                    m_PlayerAvailableWeapons.Changed += m_PlayerAvailableWeapons_Changed;
                    updateWeaponIcons();
                    setSelectedWeaponIcon();
                }
                if(Application.isMobilePlatform)
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
                if (RectTransformUtility.RectangleContainsScreenPoint(m_SwipeZone, touch.position) == false)
                {
                    continue;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    for(int i = 0; i < m_ShownWeaponTiles.Count; i++)
                    {
                        if (RectTransformUtility.RectangleContainsScreenPoint(m_ShownWeaponTiles[i].GetComponent<RectTransform>(), touch.position))
                        {
                            bool validWeaponChange = m_PlayerAvailableWeapons.ChangeWeaponToIndex(i); 
                            if (validWeaponChange)
                            {
                                setSelectedWeaponIcon();
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void handleDesktopInput()
        {
            for (int i = (int)KeyCode.Alpha1; i < (int)KeyCode.Alpha1 + m_PlayerAvailableWeapons.Weapons.Length; i++)
            {
                if (UnityEngine.Input.GetKeyDown((KeyCode)i))
                {
                    bool validWeaponChange = m_PlayerAvailableWeapons.ChangeWeaponToIndex(i - (int)KeyCode.Alpha1); // (int)KeyCode.Alpha1 is subtracted as the keycodes start at 49. Subtracting gives a number starting at 0
                    if (validWeaponChange)
                    {
                        setSelectedWeaponIcon();
                    }
                }
            }
        }

        private void setSelectedWeaponIcon()
        {
            string selectedIcon = m_PlayerAvailableWeapons.GetCurrentSelectedWeapon().IconPath;

            GetComponent<Text>().text = selectedIcon;

            for (int i = 0; i < m_ShownWeaponTiles.Count; i++)
            {
                if (m_ShownWeaponTiles[i].GetComponent<Image>().sprite.name.Equals(selectedIcon))
                {
                    m_ShownWeaponTiles[i].GetComponent<Image>().color = Color.red;
                }
                else
                {
                    m_ShownWeaponTiles[i].GetComponent<Image>().color = Color.white;
                }
            }
        }

        private void m_PlayerAvailableWeapons_Changed(object sender, System.EventArgs e)
        {
            updateWeaponIcons();
        }

        private void updateWeaponIcons()
        {
            m_ShownWeaponTiles.ForEach(m => Destroy(m));
            m_ShownWeaponTiles.Clear();

            // Add all required weapons 
            for (int i = 0; i < m_PlayerAvailableWeapons.Weapons.Length; i++)
            {
                GameObject weaponTile = Instantiate(EMPTY_WEAPON_TILE, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

                weaponTile.GetComponent<Image>().sprite = Resources.Load<Sprite>(m_PlayerAvailableWeapons.Weapons[i].IconPath);

                weaponTile.SetActive(true);
                weaponTile.transform.SetParent(GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform);
                m_ShownWeaponTiles.Add(weaponTile);
            }

            // Add empty tiles as padding
            for (int i = m_ShownWeaponTiles.Count; i < Weaponary.WeaponBag.MAX_WEAPON_COUNT; i++)
            {
                GameObject weaponTile = Instantiate(EMPTY_WEAPON_TILE, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

                weaponTile.SetActive(true);
                weaponTile.transform.SetParent(GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform);
                m_ShownWeaponTiles.Add(weaponTile);
            }
        }
    }
}
