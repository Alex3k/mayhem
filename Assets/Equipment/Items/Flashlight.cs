using System;
using Mayhem.Equipment;
using UnityEngine;

namespace Mayhem.Equipment.Items
{
    public class Flashlight : EquipmentItem
    {
        private Light m_LightSource;

        public Flashlight(Light light)
        {
            m_LightSource = light;
            m_LightSource.enabled = false;
        }

        public override string GetIconPath()
        {
            return "FlashlightIcon";
        }

        public override EquipmentType GetType()
        {
            return EquipmentType.Item;
        }

        public override bool ShouldBeRemoved()
        {
            return false;
        }

        public override UsageType GetUsageType()
        {
            return UsageType.Passive;
        }

        public override void Use(Transform owner, Vector3 carrierAngle)
        {
            m_LightSource.enabled = !m_LightSource.enabled;
        }
    }
}
