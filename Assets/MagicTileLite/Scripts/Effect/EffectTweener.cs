using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Drland.MagicTileLite
{
    public class EffectTweener : MonoBehaviour, ITweenable
    {
        private Tweener _tweener;
        public void Kill()
        {
            _tweener?.Kill();
        }

        public void PunchScale(Transform target, float targetScale, float duration)
        {
            if (target.localScale != Vector3.one)
            {
                target.localScale = Vector3.one;
            }
            _tweener = target.DOPunchScale(Vector3.one * targetScale, duration);

        }

        public void ChangeImageAlpha(Image target, float targetAlpha, float duration)
        {
            _tweener = target.DOFade(targetAlpha, duration).SetAutoKill(true);
        }

        public void ChangeImageColor(Image target, Color targetColor, float duration)
        {
            _tweener = target.DOColor(targetColor, duration);
        }
    }
}