using UnityEngine;

namespace Assets
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

        private void HandleMovement()
        {
            transform.position += ((transform.right * MovementSpeed) * Time.deltaTime);
        }
    }
}
