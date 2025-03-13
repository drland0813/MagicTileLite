using System;
using Common.UI;
using MagicTileLite.Scripts.Mics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Drland.MagicTileLite
{
    public class MainMenuView : UIController
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private TextMeshProUGUI _highScoreText;

        public Action OnPlay;
        public Action OnExit;

        public void Init()
        {
            _playButton.onClick.AddListener(Play);
            _exitButton.onClick.AddListener(Exit);

            _highScoreText.text = PlayerPrefs.GetInt(GameConstants.KEY_PREF_HIGH_SCORE).ToString();
        }

        private void Play()
        {
            OnPlay?.Invoke();
        }

        private void Exit()
        {
            OnExit?.Invoke();
        }
    }
}

