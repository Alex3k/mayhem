using System;
using UnityEngine;

namespace Mayhem.Equipment.Weaponary
{
    public class BaseWeapon : EquipmentItem
    {
        public float FireRate;
        public float ClipSize;
        public float ReloadTime;
        public float HalfScatterRadius;

        private float m_CurrentAmmoInClip;
        private bool m_IsReloading;

        private double m_LastFireTime;
        private double m_ReloadStartTime;

        private string m_IconPath;

        public BaseWeapon(float clipSize, float fireRate, float reloadTime, float halfScatterRadius, string iconPath)
        {
            m_IsReloading = false;
            FireRate = fireRate;
            ClipSize = clipSize;
            ReloadTime = reloadTime;
            m_CurrentAmmoInClip = clipSize;
            HalfScatterRadius = halfScatterRadius;
            m_IconPath = iconPath;
        }

        public override void Use(Vector3 position, Vector3 angle, PhotonView player)
        {
            if (isReloading())
            {
                return;
            }

            if (PhotonNetwork.time - m_LastFireTime > FireRate)
            {
                if (m_CurrentAmmoInClip == 0)
                {
                    Reload();
                }
                else
                {
                    FireBullet(position, angle, player);
                    m_CurrentAmmoInClip--;
                    m_LastFireTime = PhotonNetwork.time;
                }
            }
        }

        protected virtual void FireBullet(Vector3 position, Vector3 carrierAngle, PhotonView player)
        {
            carrierAngle.z = UnityEngine.Random.Range(carrierAngle.z - HalfScatterRadius, carrierAngle.z + HalfScatterRadius);
            PhotonNetwork.Instantiate("Bullet", position, Quaternion.Euler(carrierAngle), 0).GetComponent<Bullet>().Parent = player;
        }

        private bool isReloading()
        {
            if (m_IsReloading)
            {
                m_IsReloading = PhotonNetwork.time - m_ReloadStartTime < ReloadTime;

                if (m_IsReloading == false)
                {
                    m_CurrentAmmoInClip = ClipSize;
                }
            }

            return m_IsReloading;
        }

        public void Reload()
        {
            m_IsReloading = true;
            m_ReloadStartTime = PhotonNetwork.time;
        }

        public override string GetIconPath()
        {
            return m_IconPath;
        }

        public override EquipmentType GetType()
        {
            return EquipmentType.Weapon;
        }

        public override bool ShouldBeRemoved()
        {
            return false;
        }

        public override UsageType GetUsageType()
        {
            return UsageType.Continued;
        }
    }
}
