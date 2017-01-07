using UnityEngine;
using UnityEngine.UI;

public static class Stats
{
    public static int ZombieCount = 0;
}

public class NetworkStressTest : MonoBehaviour
{
    public GameObject ZombiePrefab;
    public Text ZombieCounter;

    public int InitZombieCount;

    private bool done = false;

    private float m_ZombieSpawnTime;
    private double m_LastSpawnTime;

    void Start () {
	
	}

    void Awake()
    {
        PhotonNetwork.InstantiateInRoomOnly = true;
    }

    // Update is called once per frame
    void Update()
    {
        ZombieCounter.text = "Remaining Zombies: " + Stats.ZombieCount.ToString();

        if (PhotonNetwork.inRoom && done == false)
        {
            for (int i = 0; i < InitZombieCount; i++)
            {
                SpawnZombie();
            }

            done = true;
        }
        if (PhotonNetwork.inRoom)
        {
            if (PhotonNetwork.time - m_LastSpawnTime > m_ZombieSpawnTime)
            {
                SpawnZombie();
            }
        }
    }

    void SpawnZombie()
    {
        int x = 8;
        int y = 5;

        m_ZombieSpawnTime = Random.Range(1, 3);
        PhotonNetwork.InstantiateSceneObject("Zombie", new Vector3(Random.Range(-x, x), Random.Range(-y, y), 0), Quaternion.identity, 0, null);
        m_LastSpawnTime = PhotonNetwork.time;
        Stats.ZombieCount++;
    }
}
