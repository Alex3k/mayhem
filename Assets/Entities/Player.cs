using Mayhem.Weaponary;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mayhem.Entities
{
    public class WeaponBag
    {
        private const int MAX_WEAPON_COUNT = 5;

        public BaseWeapon[] Weapons
        {
            get
            {
                return m_Weapons.ToArray();
            }
        }

        public delegate void WeaponsChangedEventHandler(object sender, EventArgs e);
        public event WeaponsChangedEventHandler Changed;
        
        private List<BaseWeapon> m_Weapons;

        private int m_SelectedWeaponIndex;

        public WeaponBag()
        {
            m_Weapons = new List<BaseWeapon>();
            m_Weapons.Add(new Handgun());
            m_SelectedWeaponIndex = 0;
        }

        public BaseWeapon GetCurrentSelectedWeapon()
        {
            return m_Weapons[m_SelectedWeaponIndex];
        }

        public void AddWeapon(BaseWeapon weapon)
        {
            if(m_Weapons.Count == MAX_WEAPON_COUNT)
            {
                return;
            }

            m_Weapons.Add(weapon);
            Changed(null, new EventArgs());
        }

        public void ChangeWeapon(int index)
        {
            m_SelectedWeaponIndex = index;
        }
    }

    public class Player : MonoBehaviour
    {
        public float MovementSpeed = 3f;
        public float RotateSpeed = 5f;

        PhotonView m_PhotonView;

        public WeaponBag WeaponBag;

        void Awake()
        {
            m_PhotonView = GetComponent<PhotonView>();
            WeaponBag = new WeaponBag();
        }

        void FixedUpdate()
        {
            if (m_PhotonView.isMine == false)
            {
                return;
            }

            UpdateMovement();
            handleWeaponary();
        }

        void handleWeaponary()
        {
            var move = new Vector3(CnControls.CnInputManager.GetAxis("ShootHorizontal"), CnControls.CnInputManager.GetAxis("ShootVertical"), 0);

            if (move != Vector3.zero)
            {
                float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;

                WeaponBag.GetCurrentSelectedWeapon().Shoot(transform.position, Quaternion.AngleAxis(angle, Vector3.forward).eulerAngles);
            }
        }

        void UpdateMovement()
        {
            var move = new Vector3(CnControls.CnInputManager.GetAxis("Horizontal"), CnControls.CnInputManager.GetAxis("Vertical"), 0);
            transform.position += move * MovementSpeed * Time.deltaTime;

            float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        public static Player FindLocalPlayer()
        {
            var players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                string playerLoopId = player.GetComponent<PhotonView>().owner.UserId;

                if (playerLoopId.Equals(PhotonNetwork.player.UserId))
                {
                    return player.GetComponent<Player>();
                }
            }

            throw new Exception("No player found somehow...");
        }
    }
}