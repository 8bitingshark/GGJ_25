using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

namespace Scoring
{

    public class ScoringScript : MonoBehaviour
    {
        [SerializeField] int score = 0;
        [SerializeField] int scoreWinning = 20;
        public string giocatore = "";
        [SerializeField] public Text uiText;
        [SerializeField] public Text winningText;

        private void Start()
        {
            uiText.text = score.ToString();
        }

        public void UpdatePoints(int scoreNow)
        {
            uiText.text = scoreNow.ToString();
            if (scoreNow >= scoreWinning)
            {
                winningText.GetComponent<WinningScreen>().EndGame(giocatore);
            }
        }


    }
}