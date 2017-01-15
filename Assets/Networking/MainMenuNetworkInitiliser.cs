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
            PhotonNetwork.AuthValues = new AuthenticationValues(((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) + new System.Random(DateTime.Now.Millisecond).Next(3912)).ToString());
           
            PhotonNetwork.ConnectUsingSettings("0.1");
        }

        void Update()
        {
            ConnectionState.text = PhotonNetwork.connectionStateDetailed.ToString();
        }

    }
}
