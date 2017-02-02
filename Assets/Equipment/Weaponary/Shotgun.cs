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

        protected override void FireBullet(Transform owner, Vector3 carrierAngle)
        {
            for (int i = 0; i < PELLET_COUNT; i++)
            {
                base.FireBullet(owner, carrierAngle);
            }
        }
    }
}
