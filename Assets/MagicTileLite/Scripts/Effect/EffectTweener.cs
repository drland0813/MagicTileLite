using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Drland.MagicTileLite
{
    public class EffectTweener : MonoBehaviour, ITweenable
    {
        private Sequence _sequence;

        public Sequence GetSequence()
        {
            _sequence = DOTween.Sequence();
            return _sequence;
        }

        public void Kill()
        {
            _sequence.Kill();
        }

        public void InsertTween(Tween tween)
        {
            if (_sequence == null)
            {
                _sequence = GetSequence();
            }
            _sequence.Insert(0, tween);
        }
        
        public void Append(Tween tween)
        {
            _sequence?.Kill();
            _sequence = GetSequence();
            _sequence.Append(tween);
        }
        

        public Tween PunchScale(Transform target, float targetScale, float duration)
        {
            if (target.localScale != Vector3.one)
            {
                target.localScale = Vector3.one;
            }
            return target.DOPunchScale(Vector3.one * targetScale, duration);

        }

        public Tween ChangeImageAlpha(Image target, float targetAlpha, float duration)
        {
            return target.DOFade(targetAlpha, duration);
        }

        public Tween ChangeImageColor(Image target, Color targetColor, float duration)
        {
            return target.DOColor(targetColor, duration);
        }
    }
}