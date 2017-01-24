using Mayhem.World;
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
                var CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();

                CustomRoomProperties.Add("private", false);

                PhotonNetwork.JoinRandomRoom(CustomRoomProperties, 10);
            }
            else if(Core.SettingsFromMainMenu.SpecifiedGameMode == Core.GameMode.FriendsGame)
            {
                PhotonNetwork.JoinRoom(Core.SettingsFromMainMenu.RoomToJoin);
            }
            else if(Core.SettingsFromMainMenu.SpecifiedGameMode == Core.GameMode.PrivateGame)
            {
                RoomOptions options = new RoomOptions();
                options.MaxPlayers = 10;
                options.PublishUserId = true;

                options.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();

                options.CustomRoomProperties.Add("password", Core.SettingsFromMainMenu.RoomPassword);
                options.CustomRoomProperties.Add("private", true);
                options.CustomRoomPropertiesForLobby = new string[] { "private" };

                PhotonNetwork.JoinOrCreateRoom(Core.SettingsFromMainMenu.RoomToJoin, options, null); 
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
            RoomOptions options = new RoomOptions();

            options.MaxPlayers = 10;
            options.PublishUserId = true;

            options.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();

            options.CustomRoomProperties.Add("private", false);
            options.CustomRoomPropertiesForLobby = new string[] { "private" };

            PhotonNetwork.CreateRoom(null, options, null);
        }

        public override void OnJoinedRoom()
        {
            if ((bool)PhotonNetwork.room.CustomProperties["private"] == true && PhotonNetwork.room.CustomProperties["password"].Equals(Core.SettingsFromMainMenu.RoomPassword))
            {
                authorisedJoinRoom();
            }
            else if ((bool)PhotonNetwork.room.CustomProperties["private"] == false)
            {
                authorisedJoinRoom();
            }
            else
            {
                SetErrorMessage("Invalid Room Password");
                PhotonNetwork.LeaveRoom();
            }
        }

        private void authorisedJoinRoom()
        {
            PhotonNetwork.player.UserId = PhotonNetwork.AuthValues.UserId;

            GameObject.Find("LoadingUI").SetActive(false);
            GameObject.Find("GameUI").SetActive(true);
            
            GameObject player = PhotonNetwork.Instantiate("Sprite", new Vector3(GameObject.Find("Map").GetComponent<Map>().Size.x / 2, GameObject.Find("Map").GetComponent<Map>().Size.y / 2, 0), Quaternion.identity, 0);
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