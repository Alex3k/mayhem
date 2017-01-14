using Mayhem.World;
using UnityEngine;
using UnityEngine.UI;

namespace Mayhem.Networking
{
    public class RandomGameMatchMaker : Photon.PunBehaviour
    {
        void Start()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            Debug.Log("Can't join random room! Creating New Room!");
            PhotonNetwork.CreateRoom(null);
        }
    }
}