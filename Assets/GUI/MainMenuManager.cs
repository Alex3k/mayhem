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

        public Transform MainMenu;
        public Transform JoinFriendsGameMenu;

        void Start()
        {
            m_LoadingSceneOperation = SceneManager.LoadSceneAsync(MainGameScene, LoadSceneMode.Single);
            m_LoadingSceneOperation.allowSceneActivation = false;
            MainMenu.gameObject.SetActive(true);
            JoinFriendsGameMenu.gameObject.SetActive(false);
        }

        void Update()
        {
        }

        public void ChangeToJoinRandomGame()
        {
            Core.SettingsFromMainMenu.PlayerNickName = PlayerNickName.text;
            m_LoadingSceneOperation.allowSceneActivation = true;
            Core.SettingsFromMainMenu.SpecifiedGameMode = Core.GameMode.RandomGame;
        }

        public void ChangeToJoinFriendsGameMenu()
        {
            MainMenu.gameObject.SetActive(false);
            JoinFriendsGameMenu.gameObject.SetActive(true);
        }

        public void JoinFriendsGame()
        {
            string friend = FriendID.text;
            Debug.Log(friend);
            Debug.Log(PhotonNetwork.FindFriends(new string[1] { friend }));

        }

        public override void OnUpdatedFriendList()
        {
            string friend = FriendID.text;

            Debug.Log("Is In Room: " + PhotonNetwork.Friends[0].IsInRoom);
            Debug.Log("Is Online: " + PhotonNetwork.Friends[0].IsOnline);
            Debug.Log("Name: " + PhotonNetwork.Friends[0].Room);

        }

    }
}
