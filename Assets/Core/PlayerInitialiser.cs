using Mayhem.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mayhem.Core
{
    public class PlayerInitialiser : MonoBehaviour
    {
        void Start()
        {
            GameObject player = PhotonNetwork.Instantiate("Sprite", Vector3.zero, Quaternion.identity, 0);
            GameObject.Find("Main Camera").AddComponent<Camera2DFollow>().target = player.transform;
        }
    }
}
