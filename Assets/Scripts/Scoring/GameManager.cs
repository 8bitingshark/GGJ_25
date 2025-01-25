using System;
using UnityEngine;
using UnityEngine.UI;

// Per lavorare con gli elementi UI

namespace Scoring
{
    public class GameManager : MonoBehaviour
    {
        public Text victoryText;  // Riferimento all'elemento di testo per la vittoria
        private bool isGameOver = false; // Indica se il gioco è terminato

        private void Start()
        {
            victoryText.text = "";
        }

        public void EndGame(string winner)
        {
            if (isGameOver) return; // Evita di chiamare più volte

            isGameOver = true;
            Debug.Log("Game Over!");
        
            // Mostra la UI di Game Over
            if (victoryText != null)
            {
                victoryText.text = "vince il "+ winner;
            }

            // Blocca il gameplay (opzionale)
            Time.timeScale = 0f; // Ferma il tempo
        }
/**
        public void RestartGame()
        {
            Time.timeScale = 1f; // Ripristina il tempo
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Ricarica la scena
        }

        public void QuitToMainMenu()
        {
            Time.timeScale = 1f; // Ripristina il tempo
            SceneManager.LoadScene("MainMenu"); // Cambia scena (assicurati che esista una scena chiamata "MainMenu")
        }**/
    }
}

