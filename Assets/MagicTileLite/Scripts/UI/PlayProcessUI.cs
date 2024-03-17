using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Drland.MagicTileLite
{
    public class PlayProcessUI: MonoBehaviour
    {
        [SerializeField] private Image _playProcessImg;
        [SerializeField] private Image[] _starImg;

        private void OnEnable()
        {
            _playProcessImg.fillAmount = 0;
        }
        
        private void PlayStarEffect(int index)
        {
            var star = _starImg[index];

            var starSequence = DOTween.Sequence();
            starSequence.Append(star.rectTransform.DORotate(new Vector3(0, 0, -360), 0.5f, RotateMode.FastBeyond360))
                .Append(star.rectTransform.DOPunchScale(Vector3.one * 0.4f, 0.2f))
                .Append(star.rectTransform.DOScale(Vector3.one, 0.2f))
                .Append(star.DOColor(Color.yellow, 0.2f))
                .Insert(0, star.rectTransform.DOScale(Vector3.one * 1.4f, 0.5f));
        }

        public void UpdateTileProcess(float newValue)
        {
            _playProcessImg.DOFillAmount(newValue, 0.2f).SetEase(Ease.InSine);
        }

        public void UpdateStarProcess(int starIndex)
        {
            var star = _starImg[starIndex];
            star.rectTransform.DOShakeRotation(0.2f, Vector3.one);
            PlayStarEffect(starIndex);
        }
    }
}