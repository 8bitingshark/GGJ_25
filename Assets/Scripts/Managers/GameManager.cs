using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private int levelsIndex = 0;
        // for testing the loading screen
        private float _loadingOffset = 1f;
        
        //*********************************Initialization*********************************
        protected override void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning($"More than one instance of {this.GetType().Name}!");
                Destroy(gameObject);
            }

            base.Awake();
            DontDestroyOnLoad(gameObject);
            
        }
        
        //*********************************Loading Scene Management*********************************
        // Guide: Menu -> scene index = 0,
        //        Level1 -> scene index = 1
        //        LoadingScreen -> last index
        
        public void ReloadCurrentLevel()
        {
            LoadLevelAtIndex(SceneManager.GetActiveScene().buildIndex);
        }
        public void LoadNextLevel()
        {
            levelsIndex++;

            LoadLevelAtIndex(1);
        }

        public void ReturnToMainMenu()
        {
            // reset game manager info
            levelsIndex = 0;
            LoadLevelAtIndex(0);
        }
        
        public void LoadLevelAtIndex(int index)
        {
            // loading the loading screen
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);

            StartCoroutine(LoadingLevel(index));
        }

        private IEnumerator LoadingLevel(int index)
        {
            yield return new WaitForSeconds(_loadingOffset);
            SceneManager.LoadScene(index);
        }
        
        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

    }
}
