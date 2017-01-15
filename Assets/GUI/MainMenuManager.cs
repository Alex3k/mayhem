using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mayhem.GUI
{
    public class MainMenuManager : MonoBehaviour
    {
        public int MainGameScene = 1;
        private AsyncOperation m_LoadingSceneOperation;

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
            m_LoadingSceneOperation.allowSceneActivation = true;
        }
    }
}
