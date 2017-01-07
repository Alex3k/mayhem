using UnityEngine;

public class NetworkStressTest :MonoBehaviour
{

    public GameObject ZombiePrefab;
    public GameObject BulletPrefab;

    public int ZombieCount;
    public int BulletCount;
    private bool done = false;
	void Start () {
	
	}

    void Awake()
    {
        PhotonNetwork.InstantiateInRoomOnly = true;
          
        
    }
	
	// Update is called once per frame
	void Update () {
        int x = 8;
        int y = 5;
        if (PhotonNetwork.inRoom && done == false)
        {
            for (int i = 0; i < ZombieCount; i++)
            {
                PhotonNetwork.InstantiateSceneObject("Zombie", new Vector3(Random.Range(-x, x), Random.Range(-y, y), 0), Quaternion.identity,0, null);
            }

            for (int i = 0; i < BulletCount; i++)
            {
                PhotonNetwork.InstantiateSceneObject("Bullet", new Vector3(Random.Range(-x, x), Random.Range(-y, y), 0), Quaternion.identity, 0, null);

            }

            done = true;
        }	
	}
}
