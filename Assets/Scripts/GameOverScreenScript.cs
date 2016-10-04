using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameOverScreenScript : MonoBehaviour
    {
        public CanvasGroup CanvasGroup;

        void Awake()
        {
            var mainCanvas = GetComponentInParent<Canvas>();
            mainCanvas.enabled = false;
            mainCanvas.enabled = true;
            CanvasGroup.alpha = 0;
        }

        public void DisplayGameOver()
        {
            CanvasGroup.alpha = 1;
        }

        public void OnRestartButtonDown()
        {
            GameManager.Instance.RestartGame(true);
        }

        public void OnMainMenuButtonDown()
        {
            GameManager.Instance.ReturnToMainMenu();
        }
    }
}
