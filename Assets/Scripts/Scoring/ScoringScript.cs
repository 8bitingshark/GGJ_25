using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scoring
{

    public class ScoringScript : MonoBehaviour
    {
        [SerializeField] int score = 0;
        public Text uiText;

        private void Start()
        {
            uiText.text =  score.ToString();
        }

        public void UpdatePoints(int scoreNow)
        {
            uiText.text = scoreNow.ToString();
        }
    }
}