using System;
using System.Collections;
using UnityEngine;

namespace Drland.MagicTileLite
{
    public class EffectManager : MonoBehaviour
    {
        [SerializeField] private TextEffectUI _textEffectUISample;
        [SerializeField] private FloatTextUI _floatTextUISample;

        private ObjectPool<FloatTextUI> _floatTextPool;
        
        private Coroutine _textEffectCoroutine;
        private Coroutine _floatTextCoroutine;

        private void Awake()
        {
            _floatTextPool = new ObjectPool<FloatTextUI>(_floatTextUISample);
        }

        public void ShowTextEffectUI(int scoreLevel, int streak)
        {
            if (_textEffectCoroutine != null)
            {
                _textEffectUISample.gameObject.SetActive(false);
                StopCoroutine(_textEffectCoroutine);
            }
            _textEffectCoroutine = StartCoroutine(ShowTextEffectCoroutine(scoreLevel, streak));
        }
        
        public void ShowFloatTextUI(int score, Vector3 position)
        {
            _floatTextCoroutine = StartCoroutine(ShowFloatTextCoroutine(score, position));
        }

        private IEnumerator ShowTextEffectCoroutine(int scoreLevel, int streak)
        {
            _textEffectUISample.gameObject.SetActive(true);
            _textEffectUISample.Enable((ScoreLevel)scoreLevel, streak);
            yield return new WaitForSeconds(0.5f);
            _textEffectUISample.gameObject.SetActive(false);
        }
        
        private IEnumerator ShowFloatTextCoroutine(int score, Vector3 position)
        {
            var floatText = _floatTextPool.Get(transform);
            floatText.Enable(score, position);
            yield return new WaitForSeconds(0.5f);
            _floatTextPool.Store(floatText);
        }
    }
}