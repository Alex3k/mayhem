﻿using Mayhem.World;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Mayhem.Networking
{
    public class RoomManager : Photon.PunBehaviour
    {
        public Text ConnectionStatus;
        public Text RoomNameLabel;
        public Button ReturnToMainMenuButton;
        public Text LoadingTitle;

        private bool m_ErrorOccured;

        void Awake()
        {
            m_ErrorOccured = false;
            ReturnToMainMenuButton.gameObject.SetActive(false);

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
            if (!m_ErrorOccured)
            {
                ConnectionStatus.text = PhotonNetwork.connectionStateDetailed.ToString();
            }
        }

        private void SetErrorMessage(string message)
        {
            m_ErrorOccured = true;
            ConnectionStatus.text = message;
            ReturnToMainMenuButton.gameObject.SetActive(true);
            LoadingTitle.gameObject.SetActive(false);
        }
        public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
        {
            if (codeAndMsg[0].ToString().Equals("32765"))
            {
                SetErrorMessage("This game is full. Please try again later.");
            }
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            Debug.Log("Can't join random room! Creating New Room!");
            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 1, PublishUserId = true }, null);
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

        public void ReturnToMainMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneAt(0));
        }
    }
}