using UnityEngine;

namespace Mayhem.Equipment.Weaponary
{
    public class Bullet : MonoBehaviour
    {
        public float MovementSpeed = 100;
        public float TTL = 2;
        private double m_CreationTime;
        public Entities.Player Parent;

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

        // Bullets handle their own logic to determine if they have collided with zombies
        void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.transform.tag == "BulletCollider")
            {
                Parent.AddScore(collision.transform.parent.gameObject.GetComponent<Entities.Zombie>().ScoreReward());

                Destroy(collision.transform.parent.gameObject);
                Destroy(transform.gameObject);
            }
        }

        private void HandleMovement()
        {
            transform.position += ((transform.right * MovementSpeed) * Time.deltaTime);
        }
    }
}
