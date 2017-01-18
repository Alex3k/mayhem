using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mayhem.Weaponary.Turret
{
    public class Sights : MonoBehaviour
    {
        public bool HasTarget { get; private set; }
        public Transform Target { get; private set; }

        void Awake()
        {
            HasTarget = false;
            Target = null;
        }

        void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Zombie")
            {
                HasTarget = true;
                Target = collision.gameObject.transform;
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Zombie")
            {
                HasTarget = false;
                Target = null;
            }
        }
    }
}
