using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

namespace Mayhem.Networking
{
    public class MainMenuNetworkInitiliser : Photon.PunBehaviour
    {
        public Text ConnectionState;

        void Start()
        {
            PhotonNetwork.ConnectUsingSettings("0.1");
        }

        void Update()
        {
            ConnectionState.text = PhotonNetwork.connectionStateDetailed.ToString();
        }

    }
}
