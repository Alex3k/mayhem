using System;
using Mayhem.Equipment;
using UnityEngine;

namespace Mayhem.Equipment.Items.Turret
{
    public class TurretPlacer : EquipmentItem
    {
        public TurretPlacer()
        {

        }

        public override string GetIconPath()
        {
            return "TurretIcon";
        }

        public override EquipmentType GetType()
        {
            return EquipmentType.Item;
        }

        public override void Use(Vector3 carrierPosition, Vector3 carrierAngle)
        {
            GameObject.Instantiate(Resources.Load("Turret"), carrierPosition, Quaternion.Euler(carrierAngle));
        }
    }
}
