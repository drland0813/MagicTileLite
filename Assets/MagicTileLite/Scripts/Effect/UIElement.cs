using System;
using DG.Tweening;
using UnityEngine;

namespace Drland.MagicTileLite
{
    [RequireComponent(typeof(EffectTweener))]
    public abstract class UIElement : MonoBehaviour
    {
        protected EffectTweener _effectTweener;

        protected virtual void Awake()
        {
            _effectTweener = GetComponent<EffectTweener>();
        }

        protected virtual void OnDisable()
        {
            _effectTweener.Kill();
        }


        public abstract void Show();

    }
}