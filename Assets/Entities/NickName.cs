using UnityEngine;

namespace Mayhem.Entities
{
    public class NickName : MonoBehaviour
    {
        Quaternion rotation;
        public string Name { get; private set; }

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
            Name = name;
            GetComponent<TextMesh>().text = name;
        }

        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                stream.SendNext(Name);
            }
            else
            {
                SetNickName((string)stream.ReceiveNext());
            }
        }
    }
}
