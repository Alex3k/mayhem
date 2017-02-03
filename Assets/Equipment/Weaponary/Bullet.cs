using UnityEngine;

namespace Mayhem.Equipment.Weaponary
{
    public class Bullet : Photon.MonoBehaviour
    {
        public float MovementSpeed = 100;
        public float TTL = 2;
        private double m_CreationTime;
        public PhotonView Parent;

        void Start()
        {
            m_CreationTime = PhotonNetwork.time;
        }

        void FixedUpdate()
        {
            if (PhotonNetwork.time - m_CreationTime > TTL)
            {
                if (photonView.isMine)
                {
                    PhotonNetwork.Destroy(transform.gameObject);
                }
            }
            else
            {
                HandleMovement();
            }
        }

        // Bullets handle their own logic to determine if they have collided with zombies
        void OnTriggerStay2D(Collider2D collision)
        {
            if(Parent == null)
            {
                return;
            }

            if (collision.transform.tag == "BulletCollider")
            {
                Entities.Zombie enemy = collision.transform.parent.gameObject.GetComponent<Entities.Zombie>();

                Parent.RPC("AddScore", PhotonTargets.AllBufferedViaServer, enemy.ScoreReward());

                if (photonView.isMine)
                {
                    PhotonNetwork.Destroy(transform.gameObject);
                }

                enemy.photonView.RPC("Die", PhotonTargets.All);
            }
        }

        private void HandleMovement()
        {
            transform.position += ((transform.right * MovementSpeed) * Time.deltaTime);
        }
    }
}
