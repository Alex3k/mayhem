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

        public EquipmentBag WeaponBag { get; private set; }
        public EquipmentBag ItemBag { get; private set; }
        private PhotonPlayer m_PlayerReference;

        void Awake()
        {
            m_PhotonView = GetComponent<PhotonView>();
            WeaponBag = new EquipmentBag(EquipmentBag.BagType.Single);
            WeaponBag.AddObject(new Handgun());
            WeaponBag.AddObject(new MachineGun());
            WeaponBag.AddObject(new Shotgun());

            WeaponBag.ChangeObjectToIndex(1);
            var tp = new TurretPlacer();
            tp.AddTurret();
            tp.AddTurret();
            ItemBag = new EquipmentBag(EquipmentBag.BagType.Toggleable);
            ItemBag.AddObject(tp);

            ItemBag.AddObject(new Flashlight(GetComponentInChildren<Light>()));

            ItemBag.EquipmentDeselected += ItemBag_EquipmentDeselected;
            ItemBag.EquipmentSelected += ItemBag_EquipmentSelected;
            ItemBag.EquipmentUsed += ItemBag_EquipmentUsed;
        }

        private void ItemBag_EquipmentUsed(object sender, EquipmentItem usedEquipment)
        {
            usedEquipment.Use(transform, transform.eulerAngles);

            if (usedEquipment.ShouldBeRemoved())
            {
                ItemBag.RemoveObject(usedEquipment);
            }
        }

        private void ItemBag_EquipmentSelected(object sender, EquipmentItem newSelection)
        {
            newSelection.Use(transform, transform.eulerAngles);

            if (newSelection.ShouldBeRemoved())
            {
                ItemBag.RemoveCurrentObject();
            }
        }

        private void ItemBag_EquipmentDeselected(object sender, EquipmentItem previousSelection)
        {
            if (previousSelection.GetUsageType() == UsageType.Passive)
            {
                previousSelection.Use(transform, transform.eulerAngles); // Toggle it to disable
            }
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

        public Equipment.EquipmentBag GetEquipmentBag(EquipmentType equipmentBagType)
        {
            if (equipmentBagType == EquipmentType.Weapon)
            {
                return WeaponBag;
            }
            else if (equipmentBagType == EquipmentType.Item)
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
            Vector3 move = Vector3.zero;

            if (Application.isMobilePlatform)
            {
                move = new Vector3(CnControls.CnInputManager.GetAxis("ShootHorizontal"), CnControls.CnInputManager.GetAxis("ShootVertical"), 0);
            }
            else
            {
                if (UnityEngine.Input.GetMouseButton(0))
                {
                    move = UnityEngine.Input.mousePosition;

                    Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
                    move.x = move.x - objectPos.x;
                    move.y = move.y - objectPos.y;
                }
            }

            if (move != Vector3.zero)
            {
                float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;

                if (WeaponBag.GetCurrentSelectedObject() != null)
                {
                    WeaponBag.GetCurrentSelectedObject().Use(transform, Quaternion.AngleAxis(angle, Vector3.forward).eulerAngles);

                    if (WeaponBag.GetCurrentSelectedObject().ShouldBeRemoved() == true)
                    {
                        WeaponBag.RemoveCurrentObject();
                    }
                }

            }
        }

        public void SetPlayerReference(PhotonPlayer player)
        {
            m_PlayerReference = player;
        }

        public void AddScore(int value)
        {
            m_PlayerReference.AddScore(value);
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