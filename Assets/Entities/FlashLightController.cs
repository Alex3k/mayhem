using UnityEngine;

namespace Mayhem.Entities
{
    public class FlashLightController : MonoBehaviour
    {
        private Light m_LightSource;

        void Awake()
        {
            m_LightSource = GetComponent<Light>();
        }

        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                stream.SendNext(m_LightSource.enabled);
            }
            else
            {
                m_LightSource.enabled = ((bool)stream.ReceiveNext());
            }
        }
    }
}
