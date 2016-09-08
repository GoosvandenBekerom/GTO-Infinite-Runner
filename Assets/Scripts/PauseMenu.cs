using Assets.Scripts;
using UnityEngine;

namespace Assets.scripts
{
    public class PauseMenu : MonoBehaviour {

        public int MainMenuScene;
        public Font PauseMenuFont;
        private bool _pauseEnabled;

        public GameObject Player;
        public GameObject World;

        private PlayerMovement _playerScript;
        private PathGeneration _worldScript;

        void Start()
        {
            _pauseEnabled = false;
            AudioListener.volume = 1;
            _playerScript = Player.GetComponent<PlayerMovement>();
            _worldScript = World.GetComponent<PathGeneration>();
        }

        public void OnPauseButtonDown()
        {
            //check if game is already paused		
            if (_pauseEnabled) Unpause();
            else if (!_pauseEnabled) Pause();
        }

        void OnGUI()
        {
            GUI.skin.box.font = PauseMenuFont;
            GUI.skin.button.font = PauseMenuFont;

            if (!_pauseEnabled) return;

            var xStartPos = (Screen.width / 2) - 125;

            //Make a background box
            GUI.Box(new Rect(xStartPos, Screen.height/2 - 100, 250, 200), "Game Paused");

            //Make Resume Game button
            if (GUI.Button(new Rect(xStartPos, Screen.height / 2 - 50, 250, 50), "Resume Game"))
            {
                Unpause();
            }

            //Make Main Menu button
            if (GUI.Button(new Rect(xStartPos, Screen.height/2 - 0, 250, 50), "Main Menu"))
            {
                //SceneManager.LoadScene(MainMenuScene);
                Debug.Log("No main menu scene implemented yet.");
            }

            //Make quit game button
            if (GUI.Button(new Rect(xStartPos, Screen.height/2 + 50, 250, 50), "Quit Game"))
            {
                Application.Quit();
            }
        }

        private void Pause()
        {
            // pause the game
            _pauseEnabled = true;
            Time.timeScale = 0;
            if (_playerScript != null) _playerScript.enabled = false;
            if (_worldScript != null) _worldScript.enabled = false;
        }

        private void Unpause()
        {
            // unpause the game
            _pauseEnabled = false;
            Time.timeScale = 1;
            if (_playerScript != null) _playerScript.enabled = true;
            if (_worldScript != null) _worldScript.enabled = true;
        }
    }
}
