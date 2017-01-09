using UnityEngine;

namespace Mayhem.Weaponary
{
    public class Bullet : MonoBehaviour
    {
        public float MovementSpeed = 100;
        public float TTL = 2;
        private double m_CreationTime;

        void Start()
        {
            m_CreationTime = PhotonNetwork.time;
        }

        void FixedUpdate()
        {
            if(PhotonNetwork.time - m_CreationTime > TTL)
            {
                Destroy(transform.gameObject);
            }
            else
            {
                HandleMovement();
            }
        }

        void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.transform.tag == "BulletCollider")
            {
                Destroy(collision.transform.parent.gameObject);
            }
        }

        private void HandleMovement()
        {
            transform.position += ((transform.right * MovementSpeed) * Time.deltaTime);
        }
    }
}
