using Mayhem.World;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Mayhem.Networking
{
    public class RoomManager : Photon.PunBehaviour
    {
        public Text ConnectionStatus;
        public Text RoomNameLabel;

        void Awake()
        {
            if (Core.SettingsFromMainMenu.SpecifiedGameMode == Core.GameMode.RandomGame)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else if(Core.SettingsFromMainMenu.SpecifiedGameMode == Core.GameMode.FriendsGame)
            {
                PhotonNetwork.JoinRoom(Core.SettingsFromMainMenu.RoomToJoin);
            }
        }

        void Update()
        {
            ConnectionStatus.text = PhotonNetwork.connectionStateDetailed.ToString();
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            Debug.Log("Can't join random room! Creating New Room!");
            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 10, PublishUserId = true }, null);
        }

        public override void OnJoinedRoom()
        {
            PhotonNetwork.player.UserId = PhotonNetwork.AuthValues.UserId;

            GameObject.Find("LoadingUI").SetActive(false);
            GameObject.Find("GameUI").SetActive(true);

            GameObject player = PhotonNetwork.Instantiate("Sprite", Vector3.zero, Quaternion.identity, 0);
            GameObject.Find("Main Camera").AddComponent<Camera2DFollow>().target = player.transform;
            PhotonNetwork.player.NickName = Core.SettingsFromMainMenu.PlayerNickName;
            player.GetComponent<Mayhem.Entities.Player>().SetNickname(PhotonNetwork.player.NickName);

            RoomNameLabel.text = "Your ID: " + PhotonNetwork.player.UserId;
        }
    }
}