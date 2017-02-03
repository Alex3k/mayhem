using Mayhem.World;
using UnityEngine;

namespace Mayhem.Core
{
    public class ZombieSpawner : Photon.PunBehaviour
    {
        public GameObject ZombiePrefab;

        public int InitZombieCount;

        private bool done = false;

        private float m_ZombieSpawnTime;
        private double m_LastSpawnTime;

        void Start()
        {
        }

        void Awake()
        {
            PhotonNetwork.InstantiateInRoomOnly = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (PhotonNetwork.isMasterClient && PhotonNetwork.inRoom)
            {
                if (done == false)
                {
                    for (int i = 0; i < InitZombieCount; i++)
                    {
                        SpawnZombie();
                    }

                    done = true;
                }

                if (PhotonNetwork.time - m_LastSpawnTime > m_ZombieSpawnTime)
                {
                    SpawnZombie();
                }
            }
        }

        void SpawnZombie()
        {
            float mapWidth = GameObject.Find("Map").GetComponent<Map>().Size.x;
            float mapHeight = GameObject.Find("Map").GetComponent<Map>().Size.y;

            m_ZombieSpawnTime = Random.Range(1, 3);
            PhotonNetwork.InstantiateSceneObject("Zombie", new Vector3(Random.Range(0, mapWidth), Random.Range(0, mapHeight), 0), Quaternion.identity, 0, null);
            m_LastSpawnTime = PhotonNetwork.time;
        }
    }
}
