using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Mayhem.Core
{
    public class Scoreboard : MonoBehaviour
    {
        private Text m_ScoreBoard;

        public void Awake()
        {
            m_ScoreBoard = GetComponent<Text>();
        }

        public void FixedUpdate()
        {
            if (PhotonNetwork.isMasterClient)
            {
                m_ScoreBoard.text = "";

                PhotonNetwork.playerList.OrderBy(o => o.GetScore());

                for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
                {
                    int score = PhotonNetwork.playerList[i].GetScore();
                    m_ScoreBoard.text += PhotonNetwork.playerList[i].NickName + ":" + score + "\n";
                }
            }
        }
    }
}
