using UnityEngine;

namespace Mayhem.Entities
{
    public class NickName : MonoBehaviour
    {
        Quaternion rotation;
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
            GetComponent<TextMesh>().text = name;
        }
    }
}
