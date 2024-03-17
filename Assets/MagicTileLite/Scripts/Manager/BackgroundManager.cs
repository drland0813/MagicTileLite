using System;
using DG.Tweening;
using UnityEngine;

namespace Drland.MagicTileLite
{
    public class BackgroundManager : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _light;
        public void TriggerLight()
        {
            var lightSequence = DOTween.Sequence();
            lightSequence.Append(_light.DOFade(1, 0.2f))
                .Append(_light.DOFade(0.6f, 0.2f));
        }
    }
}