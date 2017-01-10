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

        private float m_SwipeZoneStartY = Screen.height / 5;
        private float m_SwipeZoneStartX = Screen.width / 2;
        private RectTransform m_SwipeZone;

        void Start()
        {
            m_SwipeZone = transform.FindChild("SwipeZone").GetComponent<RectTransform>();
            m_ShownWeaponTiles = new List<GameObject>();
        }

        void Update()
        {
            if (m_PlayerAvailableWeapons == null && PhotonNetwork.inRoom)
            {
                m_PlayerAvailableWeapons = Entities.Player.FindLocalPlayer().WeaponBag;
                m_PlayerAvailableWeapons.Changed += M_PlayerAvailableWeapons_Changed;
                updateWeaponSelection();
            }
        }

        private void M_PlayerAvailableWeapons_Changed(object sender, System.EventArgs e)
        {
            updateWeaponSelection();
        }

        private void updateWeaponSelection()
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

            // Add empty tiles 
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
