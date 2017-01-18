using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mayhem.Weaponary.Turret
{
    public class Turret : MonoBehaviour
    {
        private Sights m_MySights;

        void Awake()
        {
            m_MySights = GetComponentInChildren<Sights>();
        }

        void Update()
        {
            if (m_MySights.HasTarget)
            {
                transform.right = m_MySights.Target - transform.position;
                Debug.Log("Shooting at " + m_MySights.Target);
            }
        }
    }
}
