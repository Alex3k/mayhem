using Mayhem.Weaponary;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mayhem.Entities
{
    public class Player : MonoBehaviour
    {
        public float MovementSpeed = 3f;
        public float RotateSpeed = 5f;

        PhotonView m_PhotonView;

        public WeaponBag WeaponBag { get; private set; }

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

                WeaponBag.GetCurrentSelectedWeapon().FireHandler(transform.position, Quaternion.AngleAxis(angle, Vector3.forward).eulerAngles);
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