using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RandomMatchMaker : Photon.PunBehaviour {

    public Text ConnectionState;

	// Use this for initialization
	void Start () {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    // Update is called once per frame
    void Update () {
        Debug.Log(PhotonNetwork.connectionStateDetailed.ToString());
        ConnectionState.text = PhotonNetwork.connectionStateDetailed.ToString();
	}

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.Log("Can't join random room!");
        PhotonNetwork.CreateRoom(null);
    }

    public override void OnJoinedRoom()
    {
        GameObject player = PhotonNetwork.Instantiate("Sprite", Vector3.zero, Quaternion.identity, 0);
        CharacterController controller = player.GetComponent<CharacterController>();
        controller.enabled = true;
    }
}
