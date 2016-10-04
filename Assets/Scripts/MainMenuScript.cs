using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class MainMenuScript : MonoBehaviour {

        public void OnStartGameButtonDown()
        {
            GameManager.Instance.RestartGame(false);
            SceneManager.LoadSceneAsync("Main");
        }
        
        public void OnOpenShopButtonDown()
        {
            Debug.Log("Shop not yet implemented.");
        }
        
        public void OnHighscoresButtonDown()
        {
            Debug.Log("Highscores not yet implemented.");
        }
    }
}
