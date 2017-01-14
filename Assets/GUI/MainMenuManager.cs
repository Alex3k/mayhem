using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mayhem.GUI
{
    public class MainMenuManager : MonoBehaviour
    {
        public int LoadingScene = 1;
        public int MainGameScene = 2;
        private AsyncOperation m_LoadingSceneOperation;

        void Start()
        {
            m_LoadingSceneOperation = SceneManager.LoadSceneAsync(LoadingScene, LoadSceneMode.Single);
            m_LoadingSceneOperation.allowSceneActivation = false;
        }

        void Update()
        {
        }

        public void ChangeToLoadingScene()
        {
            m_LoadingSceneOperation.allowSceneActivation = true;
        }
    }
}
