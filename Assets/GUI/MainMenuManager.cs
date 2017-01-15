using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Mayhem.GUI
{
    public class MainMenuManager : MonoBehaviour
    {
        public int MainGameScene = 1;
        private AsyncOperation m_LoadingSceneOperation;
        public Text PlayerNickName;

        void Start()
        {
            m_LoadingSceneOperation = SceneManager.LoadSceneAsync(MainGameScene, LoadSceneMode.Single);
            m_LoadingSceneOperation.allowSceneActivation = false;
        }

        void Update()
        {
        }

        public void ChangeToLoadingScene()
        {
            Core.SettingsFromMainMenu.PlayerNickName = PlayerNickName.text;
            m_LoadingSceneOperation.allowSceneActivation = true;
        }
    }
}
