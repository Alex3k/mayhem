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

        void Awake()
        {
            PhotonNetwork.AuthValues = new AuthenticationValues(Core.RandomIDGenerator.GenerateID());
           
            PhotonNetwork.ConnectUsingSettings("0.1");
        }

        void Update()
        {
            ConnectionState.text = PhotonNetwork.connectionStateDetailed.ToString();
        }

    }
}
