using UnityEngine;

namespace Mayhem.Equipment.Weaponary
{
    public class Shotgun : BaseWeapon
    {
        private const int PELLET_COUNT = 10;

        public Shotgun()
            : base(50, 0.5f, 0.5f, 20f, "Shotgun")
        {
        }

        [PunRPC]
        protected override void FireBullet(Vector3 position, Vector3 carrierAngle, PhotonView player)
        {
            for (int i = 0; i < PELLET_COUNT; i++)
            {
                base.FireBullet(position, carrierAngle, player);
            }
        }
    }
}
