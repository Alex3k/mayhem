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
        public Vector3 Target { get; private set; }

        void Awake()
        {
            HasTarget = false;
            Target = Vector2.zero;
        }

        void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Zombie")
            {
                HasTarget = true;
                Target = collision.gameObject.transform.position;
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Zombie")
            {
                HasTarget = false;
                Target = Vector3.zero;
            }
        }
    }
}
