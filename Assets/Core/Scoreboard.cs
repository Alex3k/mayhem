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
            

            m_ScoreBoard.text = "";

            GameObject[] players = PhotonNetwork.FindGameObjectsWithComponent(typeof(Entities.Player)).ToArray();

            players.OrderByDescending(o => o.GetComponent<Entities.Player>().Score);

            for (int i = 0; i < players.Length; i++)
            {
                int score = players[i].GetComponent<Entities.Player>().Score;
                m_ScoreBoard.text += players[i].GetComponent<Entities.Player>().NickName + ":" + score + "\n";
            }

        }
    }
}
