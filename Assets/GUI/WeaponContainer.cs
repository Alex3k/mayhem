using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mayhem.GUI
{
    public class WeaponContainer : MonoBehaviour
    {
        public GameObject EMPTY_WEAPON_TILE;
        private string[] m_WeaponIcons;

        private List<GameObject> m_ShownWeaponTiles;

        private Entities.WeaponBag m_PlayerAvailableWeapons;

        private float m_SwipeZoneStartY = Screen.height / 5;
        private float m_SwipeZoneStartX = Screen.width / 2;
        private RectTransform m_SwipeZone;

        void Start()
        {
            m_SwipeZone = transform.FindChild("SwipeZone").GetComponent<RectTransform>();
            m_ShownWeaponTiles = new List<GameObject>();

            m_WeaponIcons = new string[]
            {
                new Weaponary.Handgun().IconPath,
                new Weaponary.MachineGun().IconPath,
                new Weaponary.MiniGun().IconPath
            };
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
            // This works but isn't pretty.
            // The next step is instead of replacing shown weapon tiles every time, update it to check if it contains ones that arent in the players bag any more
            // If they are notin the bag, delete them from shown weapon tiles
            // if there are new ones, addthem toshown weapon tiles
            m_ShownWeaponTiles.ForEach(m => Destroy(m));
            m_ShownWeaponTiles.Clear();

            for (int j = 0; j < m_WeaponIcons.Length; j++)
            {
                for (int i = 0; i < m_PlayerAvailableWeapons.Weapons.Length; i++)
                {
                    if (m_PlayerAvailableWeapons.Weapons[i].IconPath == m_WeaponIcons[j])
                    {
                        GameObject weaponTile = Instantiate(EMPTY_WEAPON_TILE, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

                        weaponTile.GetComponent<Image>().sprite = Resources.Load<Sprite>(m_WeaponIcons[j]);

                        weaponTile.SetActive(true);
                        weaponTile.transform.SetParent(GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform);
                        m_ShownWeaponTiles.Add(weaponTile);
                    }
                }
            }

            for(int i = m_ShownWeaponTiles.Count; i < 5; i++)
            {
                GameObject weaponTile = Instantiate(EMPTY_WEAPON_TILE, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

                weaponTile.SetActive(true);
                weaponTile.transform.SetParent(GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform);
                m_ShownWeaponTiles.Add(weaponTile);
            }
        }
    }
}
