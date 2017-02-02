using UnityEngine;

namespace Mayhem.Equipment
{
    public enum EquipmentType
    {
        Weapon,
        Item
    }

    public enum UsageType
    {
        Passive,    // Items that can be used continually (torch)
        OneTime,    // Items you drop (turret)
        Continued   // Weapons
    }

    public abstract class EquipmentItem
    {
        public abstract string GetIconPath();
        public abstract void Use(Transform owner, Vector3 carrierAngle);
        public abstract EquipmentType GetType();
        public abstract bool ShouldBeRemoved();
        public abstract UsageType GetUsageType();
    }
}
