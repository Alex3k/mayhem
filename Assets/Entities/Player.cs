using Mayhem.Equipment;
using Mayhem.Equipment.Items;
using Mayhem.Equipment.Items.Turret;
using Mayhem.Equipment.Weaponary;
using System;
using UnityEngine;

namespace Mayhem.Entities
{
    public class Player : MonoBehaviour
    {
        public float MovementSpeed = 3f;
        public float RotateSpeed = 5f;

        PhotonView m_PhotonView;

        public EquipmentBag<EquipmentItem> WeaponBag { get; private set; }
        public EquipmentBag<EquipmentItem> ItemBag { get; private set; }

        void Awake()
        {
            m_PhotonView = GetComponent<PhotonView>();
            WeaponBag = new EquipmentBag<EquipmentItem>();
            WeaponBag.AddObject(new Handgun());
            WeaponBag.AddObject(new MachineGun());
            WeaponBag.AddObject(new Shotgun());

            WeaponBag.ChangeObjectToIndex(1);

            ItemBag = new EquipmentBag<EquipmentItem>();
            ItemBag.AddObject(new TurretPlacer());
            ItemBag.AddObject(new Flashlight(GetComponentInChildren<Light>()));
        }

        void FixedUpdate()
        {
            if (m_PhotonView.isMine == false)
            {
                return;
            }

            UpdateMovement();
            handleWeaponary();
            handleItems();
        }

        public Equipment.EquipmentBag<EquipmentItem> GetEquipmentBag(EquipmentType equipmentBagType)
        {
            if(equipmentBagType == EquipmentType.Weapon)
            {
                return WeaponBag;
            }
            else if(equipmentBagType == EquipmentType.Item)
            {
                return ItemBag;
            }
            else
            {
                throw new Exception("Can not get equipment bag of type " + equipmentBagType.ToString());
            }
        }

        void handleWeaponary()
        {
            var move = new Vector3(CnControls.CnInputManager.GetAxis("ShootHorizontal"), CnControls.CnInputManager.GetAxis("ShootVertical"), 0);

            if (move != Vector3.zero)
            {
                float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;

                WeaponBag.GetCurrentSelectedObject().Use(transform.position, Quaternion.AngleAxis(angle, Vector3.forward).eulerAngles);
            }
        }

        void handleItems()
        {
            if(ItemBag.HasSelectedSomething())
            {
                ItemBag.GetCurrentSelectedObject().Use(transform.position, transform.eulerAngles);
                ItemBag.Deselect();
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

        public void SetNickname(string name)
        {
            GetComponentInChildren<NickName>().SetNickName(name);
        }
    }
}