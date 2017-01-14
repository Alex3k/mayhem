using UnityEngine;

namespace Mayhem.Weaponary
{
    public class Shotgun : BaseWeapon
    {
        private const int PELLET_COUNT = 10;

        public Shotgun()
            : base(50, 0.5f, 0.5f, 20f, "Shotgun")
        {
        }

        protected override void FireBullet(Vector3 carrierPosition, Vector3 carrierAngle)
        {
            for (int i = 0; i < PELLET_COUNT; i++)
            {
                base.FireBullet(carrierPosition, carrierAngle);
            }
        }
    }
}
