using UnityEngine;
using UnityEngine.UI;

public class RandomMatchMaker : Photon.PunBehaviour
{

    public Text ConnectionState;

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    // Update is called once per frame
    void Update()
    {
        ConnectionState.text = PhotonNetwork.connectionStateDetailed.ToString();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Room Count: " + PhotonNetwork.GetRoomList().Length);
        for (int i = 0; i < PhotonNetwork.GetRoomList().Length; i++)
        {
            Debug.Log(PhotonNetwork.GetRoomList()[i].Name);
        }
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.Log("Can't join random room!");
        PhotonNetwork.CreateRoom(null);
    }

    public override void OnJoinedRoom()
    {
        GameObject player = PhotonNetwork.Instantiate("Sprite", Vector3.zero, Quaternion.identity, 0);
        GameObject.Find("Main Camera").AddComponent<Camera2DFollow>().target = player.transform;
    }
}
