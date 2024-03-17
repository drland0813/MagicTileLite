using TMPro;
using UnityEngine;

namespace Drland.MagicTileLite
{
    public class EndGameUI : UIController
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;

        public void Show(int score, int highScore)
        {
            _scoreText.text = score.ToString();
            _highScoreText.text = highScore.ToString();
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}