using Mayhem.World;
using UnityEngine;
using UnityEngine.UI;

namespace Mayhem.Networking
{
    public class RandomGameMatchMaker : Photon.PunBehaviour
    {
        public Text ConnectionStatus;

        void Awake()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        void Update()
        {
            ConnectionStatus.text = PhotonNetwork.connectionStateDetailed.ToString();
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            Debug.Log("Can't join random room! Creating New Room!");
            PhotonNetwork.CreateRoom(null);
        }

        public override void OnJoinedRoom()
        {
            GameObject.Find("LoadingUI").SetActive(false);

            GameObject player = PhotonNetwork.Instantiate("Sprite", Vector3.zero, Quaternion.identity, 0);
            GameObject.Find("Main Camera").AddComponent<Camera2DFollow>().target = player.transform;
        }
    }
}