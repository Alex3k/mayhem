using System;
using Mayhem.Equipment;
using UnityEngine;

namespace Mayhem.Equipment.Items.Turret
{
    public class TurretPlacer : EquipmentItem
    {
        private bool m_Destroy;
        private int m_AvailableTurrets;

        public TurretPlacer()
        {
            m_Destroy = false;
        }

        public override bool ShouldBeRemoved()
        {
            return m_Destroy;
        }

        public override string GetIconPath()
        {
            return "TurretIcon";
        }

        public override EquipmentType GetType()
        {
            return EquipmentType.Item;
        }

        public override void Use(Vector3 position, Vector3 angle, PhotonView player)
        {
            if (m_AvailableTurrets > 0)
            {
                PhotonNetwork.Instantiate("Turret", position, Quaternion.Euler(angle), 0).GetComponent<Turret>().SetOwner(player);
                m_AvailableTurrets--;
            }

            if(m_AvailableTurrets == 0)
            {
                m_Destroy = true;
            }
        }

        public void AddTurret()
        {
            m_AvailableTurrets++;
        }

        public override UsageType GetUsageType()
        {
            return UsageType.OneTime;
        }
    }
}
