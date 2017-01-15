using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Mayhem.GUI
{
    public class MainMenuManager : Photon.PunBehaviour
    {
        public int MainGameScene = 1;
        private AsyncOperation m_LoadingSceneOperation;
        public Text PlayerNickName;
        public Text FriendID;

        public Text ErrorMessage;

        public Transform MainMenu;
        public Transform JoinFriendsGameMenu;

        void Start()
        {
            m_LoadingSceneOperation = SceneManager.LoadSceneAsync(MainGameScene, LoadSceneMode.Single);
            m_LoadingSceneOperation.allowSceneActivation = false;
            MainMenu.gameObject.SetActive(true);
            JoinFriendsGameMenu.gameObject.SetActive(false);
            ErrorMessage.gameObject.SetActive(false);
        }

        void Update()
        {
        }

        public void ChangeToJoinRandomGame()
        {
            Core.SettingsFromMainMenu.PlayerNickName = PlayerNickName.text;
            Core.SettingsFromMainMenu.SpecifiedGameMode = Core.GameMode.RandomGame;
            Core.SettingsFromMainMenu.RoomToJoin = null;
            m_LoadingSceneOperation.allowSceneActivation = true;

        }

        public void ChangeToJoinFriendsGameMenu()
        {
            MainMenu.gameObject.SetActive(false);
            JoinFriendsGameMenu.gameObject.SetActive(true);
        }

        public void JoinFriendsGame()
        {
            Debug.Log(PhotonNetwork.FindFriends(new string[1] { FriendID.text }));
        }

        public override void OnUpdatedFriendList()
        {
            if(PhotonNetwork.Friends.Count > 0)
            {
                if (PhotonNetwork.Friends[0].IsInRoom)
                {
                    ErrorMessage.gameObject.SetActive(false);
                    Core.SettingsFromMainMenu.PlayerNickName = PlayerNickName.text;
                    Core.SettingsFromMainMenu.SpecifiedGameMode = Core.GameMode.FriendsGame;
                    Core.SettingsFromMainMenu.RoomToJoin = PhotonNetwork.Friends[0].Room;
                    m_LoadingSceneOperation.allowSceneActivation = true;
                }
                else if (PhotonNetwork.Friends[0].IsOnline)
                {
                    ErrorMessage.text = "Your friend is not in a game.";
                    ErrorMessage.gameObject.SetActive(true);
                }
                else if (!PhotonNetwork.Friends[0].IsOnline)
                {
                    ErrorMessage.text = "Your friend is not online.";
                    ErrorMessage.gameObject.SetActive(true);
                }

            }
        }

        public void ShowMainMenu()
        {
            MainMenu.gameObject.SetActive(true);
            JoinFriendsGameMenu.gameObject.SetActive(false);
            ErrorMessage.gameObject.SetActive(false);
            FriendID.text = "";

        }

    }
}
