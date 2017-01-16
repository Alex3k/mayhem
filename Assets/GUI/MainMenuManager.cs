using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Mayhem.GUI
{
    public class MainMenuManager : Photon.PunBehaviour
    {
        private enum Menu
        {
            MainMenu,
            JoinFriendsMenu,
            CreatePrivateGame,
            JoinPrivateGame
        }

        public int MainGameScene = 1;
        private AsyncOperation m_LoadingSceneOperation;
        public InputField PlayerNickName;
        public Text FriendID;

        public InputField CreatePrivateRoomName;
        public InputField CreatePrivateRoomPassword;

        public InputField JoinPrivateRoomName;
        public InputField JoinPrivateRoomPassword;

        public Text JoinFriendsGameErrorMessage;
        public Text CreatePrivateRoomErrorMessage;
        public Text JoinPrivateRoomErrorMessage;

        public Transform MainMenu;
        public Transform JoinFriendsGameMenu;
        public Transform CreatePrivateGameMenu;
        public Transform JoinPrivateGameMenu;

        private Menu m_CurrentMenu;

        void Start()
        {
            m_LoadingSceneOperation = SceneManager.LoadSceneAsync(MainGameScene, LoadSceneMode.Single);
            m_LoadingSceneOperation.allowSceneActivation = false;
            MainMenu.gameObject.SetActive(true);
            JoinFriendsGameMenu.gameObject.SetActive(false);
            CreatePrivateRoomErrorMessage.gameObject.SetActive(false);
            JoinFriendsGameErrorMessage.gameObject.SetActive(false);
            JoinPrivateRoomErrorMessage.gameObject.SetActive(false);
            PlayerNickName.text = Core.RandomNameGenerator.GetName();
        }

        private void changeMenu(Menu newMenu)
        {
            MainMenu.gameObject.SetActive(false);
            JoinFriendsGameMenu.gameObject.SetActive(false);
            CreatePrivateGameMenu.gameObject.SetActive(false);
            JoinPrivateGameMenu.gameObject.SetActive(false);

            CreatePrivateRoomErrorMessage.gameObject.SetActive(false);
            JoinFriendsGameErrorMessage.gameObject.SetActive(false);
            JoinPrivateRoomErrorMessage.gameObject.SetActive(false);
            FriendID.text = "";

            m_CurrentMenu = newMenu;

            if (newMenu == Menu.CreatePrivateGame)
            {
                CreatePrivateGameMenu.gameObject.SetActive(true);
            }
            else if(newMenu == Menu.JoinFriendsMenu)
            {
                JoinFriendsGameMenu.gameObject.SetActive(true);
            }
            else if(newMenu == Menu.MainMenu)
            {
                MainMenu.gameObject.SetActive(true);
            }
            else if(newMenu == Menu.JoinPrivateGame)
            {
                JoinPrivateGameMenu.gameObject.SetActive(true);
            }
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
            changeMenu(Menu.JoinFriendsMenu);
        }

        public void ChangeToJoinPrivateGameMenu()
        {
            changeMenu(Menu.JoinPrivateGame);
        }

        public void ChangeToCreatePrivateGameMenu()
        {
            changeMenu(Menu.CreatePrivateGame);
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
                    JoinFriendsGameErrorMessage.gameObject.SetActive(false);
                    Core.SettingsFromMainMenu.PlayerNickName = PlayerNickName.text;
                    Core.SettingsFromMainMenu.SpecifiedGameMode = Core.GameMode.FriendsGame;
                    Core.SettingsFromMainMenu.RoomToJoin = PhotonNetwork.Friends[0].Room;
                    m_LoadingSceneOperation.allowSceneActivation = true;
                }
                else if (PhotonNetwork.Friends[0].IsOnline)
                {
                    JoinFriendsGameErrorMessage.text = "Your friend is not in a game.";
                    JoinFriendsGameErrorMessage.gameObject.SetActive(true);
                }
                else if (!PhotonNetwork.Friends[0].IsOnline)
                {
                    JoinFriendsGameErrorMessage.text = "Your friend is not online.";
                    JoinFriendsGameErrorMessage.gameObject.SetActive(true);
                }
            }
        }

        public void ShowMainMenu()
        {
            changeMenu(Menu.MainMenu);
        }

        private bool doesRoomExist(string roomNameToCheck)
        {
            var rooms = PhotonNetwork.GetRoomList();

            for (int i = 0; i < rooms.Length; i++)
            {
                if (rooms[i].Name.Equals(roomNameToCheck))
                {
                    return true;
                }
            }

            return false;
        }

        public void CreateJoinPrivateRoom()
        {
            bool errorOccured = false;

            if (m_CurrentMenu == Menu.JoinPrivateGame)
            {
                if (doesRoomExist(JoinPrivateRoomName.text))
                {
                    Core.SettingsFromMainMenu.RoomToJoin = JoinPrivateRoomName.text;
                    Core.SettingsFromMainMenu.RoomPassword = JoinPrivateRoomPassword.text;
                }
                else
                {
                    errorOccured = true;
                    JoinPrivateRoomErrorMessage.text = "That room name doesn't exist.";
                    JoinPrivateRoomErrorMessage.gameObject.SetActive(true);
                }
            }
            else if (m_CurrentMenu == Menu.CreatePrivateGame)
            {
                if (doesRoomExist(CreatePrivateRoomName.text))
                {
                    errorOccured = true;
                    CreatePrivateRoomErrorMessage.text = "That room name already exists.";
                    CreatePrivateRoomErrorMessage.gameObject.SetActive(true);
                }
                else
                {
                    Core.SettingsFromMainMenu.RoomToJoin = CreatePrivateRoomName.text;
                    Core.SettingsFromMainMenu.RoomPassword = CreatePrivateRoomPassword.text;
                }
            }

            if (errorOccured == false)
            {
                Core.SettingsFromMainMenu.SpecifiedGameMode = Core.GameMode.PrivateGame;
                m_LoadingSceneOperation.allowSceneActivation = true;
            }
        }
    }
}
