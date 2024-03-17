using System;
using DG.Tweening;
using MagicTileLite.Scripts.Mics;
using UnityEngine;
using UnityEngine.UI;

namespace Drland.MagicTileLite
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private Text _countDownText;
        public ScoreUI Score;
        public EffectManager Effect;

        public Action OnGameStart;
        public void CountDownToStart(int countDownTime)
        {
            var startSequence = DOTween.Sequence();
            startSequence.Append(_countDownText.DOCounter(countDownTime, 1, countDownTime))
                .Append(_countDownText.DOText(GameConstants.START_TEXT, 0.5f))
                .Append(_countDownText.rectTransform.DOPunchScale(Vector3.one * 0.2f, 0.5f))
                .Append(_countDownText.DOFade(0, 0.2f)).OnComplete(StartGame);
        }

        private void StartGame()
        {
            _countDownText.gameObject.SetActive(false);
            OnGameStart?.Invoke();
        }
    }
}