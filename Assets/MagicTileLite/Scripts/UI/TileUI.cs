using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Drland.MagicTileLite.UI
{
    public class TileUI : MonoBehaviour
    {
        private Image _image;
        private EffectTweener _effectTweener;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _effectTweener = GetComponent<EffectTweener>();
        }

        protected void OnEnable()
        {
            var color = Color.black;
            color.a = 1;
            _image.color = color;
        }
        
        protected void OnDisable()
        {
            _effectTweener.Kill();
            var color = Color.black;
            color.a = 1;
            _image.color = color;
        }


        public void DoInteract()
        {
            _effectTweener.InsertTween(_effectTweener.PunchScale(_image.transform, 0.2f, 0.2f));
            _effectTweener.InsertTween(_effectTweener.ChangeImageColor(_image, Color.white, 0.1f));
            _effectTweener.InsertTween( _effectTweener.ChangeImageAlpha(_image, 0.5f, 0.3f));
        }
        public void PlayTouchEffect()
        {
            _effectTweener.Append(_effectTweener.PunchScale(_image.transform, 0.2f, 0.2f));
        }

        public void PlayChangeColorEffect()
        {
            _effectTweener.Append(_effectTweener.ChangeImageColor(_image, Color.white, 0.2f));
        }
        
        public void PlayFadeEffect()
        {
            _effectTweener.Append( _effectTweener.ChangeImageAlpha(_image, 0.5f, 0.5f));
        }
    }
}