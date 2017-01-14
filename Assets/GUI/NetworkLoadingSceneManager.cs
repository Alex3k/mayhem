using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Mayhem.GUI
{
    public class NetworkLoadingSceneManager : MonoBehaviour
    {
        public Text ConnectionText;
        private int m_GameScene = 2;
        private AsyncOperation m_LoadSceneOperation;

        void Awake()
        {
            m_LoadSceneOperation = SceneManager.LoadSceneAsync(m_GameScene, LoadSceneMode.Single);
            m_LoadSceneOperation.allowSceneActivation = false;
        }

        void Update()
        {
            ConnectionText.text = PhotonNetwork.connectionStateDetailed.ToString();

            if (PhotonNetwork.inRoom )
            {
                m_LoadSceneOperation.allowSceneActivation = true;
            }
        }
    }
}