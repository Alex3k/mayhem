using UnityEngine;

namespace Mayhem.Entities
{
    public class NickName : MonoBehaviour
    {
        Quaternion rotation;
        private string m_NickName;

        void Awake()
        {
            rotation = transform.rotation;
            GetComponent<MeshRenderer>().sortingLayerName = "Game";
        }
        void LateUpdate()
        {
            transform.rotation = rotation;
        }

        public void SetNickName(string name)
        {
            m_NickName = name;
            GetComponent<TextMesh>().text = name;
        }

        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                stream.SendNext(m_NickName);

            }
            else
            {
                SetNickName((string)stream.ReceiveNext());
            }
        }
    }
}
