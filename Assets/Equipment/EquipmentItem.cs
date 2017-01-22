using UnityEngine;

namespace Mayhem.Equipment
{
    public enum EquipmentType
    {
        Weapon,
        Item
    }

    public abstract class EquipmentItem
    {
        public abstract string GetIconPath();
        public abstract void Use(Vector3 carrierPosition, Vector3 carrierAngle);
        public abstract EquipmentType GetType();
        public abstract bool ShouldBeRemoved();
    }
}
