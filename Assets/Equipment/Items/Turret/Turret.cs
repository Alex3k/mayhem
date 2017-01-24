using Mayhem.Equipment.Weaponary;
using UnityEngine;

namespace Mayhem.Equipment.Items.Turret
{
    public class Turret : MonoBehaviour
    {
        private Sights m_MySights;
        private MachineGun m_Gun;

        void Awake()
        {
            m_MySights = GetComponentInChildren<Sights>();
            m_Gun = new MachineGun();
        }

        void Update()
        {
            if (m_MySights.HasTarget)
            {
                if (m_MySights.Target != null)
                {
                    transform.right = m_MySights.Target.position - transform.position;
                    m_Gun.Use(transform.position, transform.eulerAngles);
                }
            }
        }
    }
}
