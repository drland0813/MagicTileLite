using System;
using Common.UI;
using TMPro;
using UnityEngine;

namespace Drland.MagicTileLite
{
    public class EndGameUI : UIController
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;

        public Action OnReplay;
        public Action OnBackMenu;

        public void Show(int score, int highScore)
        {
            _scoreText.text = score.ToString();
            _highScoreText.text = highScore.ToString();
        }

        public void BackMenu()
        {
            OnBackMenu?.Invoke();
        }

        public void Replay()
        {
            OnReplay?.Invoke();
        }
        
        public void Quit()
        {
            Application.Quit();
        }
    }
}