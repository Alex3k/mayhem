using UnityEngine;

namespace Assets
{

    public class Weapon
    {
        public float FireRate;
        public float ClipSize;
        public float ReloadTime;

        private float m_CurrentAmmoInClip;
        private bool m_IsReloading;

        private double m_LastFireTime;
        private double m_ReloadStartTime;

        public Weapon(float clipSize, float fireRate, float reloadTime)
        {
            m_IsReloading = false;
            FireRate = fireRate;
            ClipSize = clipSize;
            ReloadTime = reloadTime;
            m_CurrentAmmoInClip = clipSize;
        }

        public void Shoot(Vector3 position, Vector3 angle)
        {
            if (isReloading())
            {
                return;
            }

            if (PhotonNetwork.time - m_LastFireTime > FireRate)
            {
                if (ClipSize - 1 == 0)
                {
                    Reload();
                }
                else
                {
                    GameObject player = PhotonNetwork.Instantiate("Bullet", position, Quaternion.Euler(angle), 0);
                    m_CurrentAmmoInClip--;
                    m_LastFireTime = PhotonNetwork.time;
                }
            }
        }

        private bool isReloading()
        {
            if (m_IsReloading)
            {
                m_IsReloading = PhotonNetwork.time - m_ReloadStartTime < ReloadTime;
            }

            return m_IsReloading;
        }

        public void Reload()
        {
            m_IsReloading = true;
            m_ReloadStartTime = PhotonNetwork.time;
        }
    }
}
