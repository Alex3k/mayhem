using Mayhem.World;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mayhem.Core
{
    public class ZombieSpawner : Photon.PunBehaviour
    {
        public GameObject ZombiePrefab;
        public Text WaveCounter;

        private List<GameObject> m_WaveEnemies;
        private bool m_HasTriggeredNextWave;

        public int CurrentWave { get; private set; }

        void Start()
        {
        }

        void Awake()
        {
            PhotonNetwork.InstantiateInRoomOnly = true;
            m_WaveEnemies = new List<GameObject>();
            CurrentWave = 0;
            m_HasTriggeredNextWave = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (PhotonNetwork.isMasterClient && PhotonNetwork.inRoom)
            {
                if(CurrentWave == 0)
                {
                    InitiateNextWave();
                }
                else
                {

                    for(int i = m_WaveEnemies.Count - 1; i >= 0; i--)
                    {
                        if (m_WaveEnemies[i].gameObject == null)
                        {
                            m_WaveEnemies.RemoveAt(i);
                        }
                    }

                    if (m_WaveEnemies.Count == 0 && m_HasTriggeredNextWave == false)
                    {
                        m_HasTriggeredNextWave = true;
                        photonView.RPC("InitiateNextWave", PhotonTargets.AllBufferedViaServer);
                    }
                }
            }
        }

        private void SpawnZombie()
        {
            float mapWidth = GameObject.Find("Map").GetComponent<Map>().Size.x;
            float mapHeight = GameObject.Find("Map").GetComponent<Map>().Size.y;

            m_WaveEnemies.Add(PhotonNetwork.InstantiateSceneObject("Zombie", new Vector3(Random.Range(0, mapWidth), Random.Range(0, mapHeight), 0), Quaternion.identity, 0, null));
        }

        [PunRPC]
        public void InitiateNextWave()
        {
            if (PhotonNetwork.isMasterClient)
            {
                CurrentWave++;
                WaveCounter.text = "Wave: " + CurrentWave;

                for (int i = 0; i < 20; i++)
                {
                    SpawnZombie();
                }

                m_HasTriggeredNextWave = false;
            }
        }
    }
}
