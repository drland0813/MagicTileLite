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
            if (_image.color != Color.black)
            {
                _image.color = Color.black;
            }
        }
        
        protected void OnDisable()
        {
            _effectTweener.Kill();
            if (_image.color != Color.black)
            {
                _image.color = Color.black;
            }
        }
        
        
        public void PlayTouchEffect()
        {
            _effectTweener.PunchScale(_image.transform, 0.2f, 0.2f);
        }

        public void PlayChangeColorEffect()
        {
            _effectTweener.ChangeImageColor(_image, Color.white, 0.2f);
        }
        
        public void PlayFadeEffect()
        {
            _effectTweener.ChangeImageAlpha(_image, 0.5f, 0.5f);
        }
    }
}