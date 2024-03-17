using System;
using DG.Tweening;
using MagicTileLite.Scripts.Mics;
using TMPro;
using UnityEngine;

namespace Drland.MagicTileLite
{
    public struct TextEffectData
    {
        public string Text;
        public float Scale;
        public Color Color;
    }
    public class TextEffectUI : UIElement
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private TextMeshProUGUI _streakText;

        private int _streak;
        private ScoreLevel _scoreLevel;
        
        protected override void OnDisable()
        {
            base.OnDisable();
            _text.rectTransform.localScale = Vector3.one;
            _streakText.rectTransform.localScale = Vector3.one;
        }

        
        public override void Show()
        {
            var data = GetTextEffectData();
            _text.text = data.Text;
            if (_text.rectTransform.localScale != Vector3.one)
            {
            }
            _effectTweener.PunchScale(_text.rectTransform, data.Scale, 0.2f);
            _text.DOColor(data.Color, 0.2f);
            
            if (_streak > 0)
            {
                _streakText.text = $"x{_streak}";
                _effectTweener.PunchScale(_streakText.rectTransform, 0.3f, 0.2f);
                return;
            }
            _streakText.text = "";

        }
        
        public void Enable(ScoreLevel scoreLevel, int streak)
        {
            _scoreLevel = scoreLevel;
            _streak = streak;
            Show();
        }

        private TextEffectData GetTextEffectData()
        {
            var data = new TextEffectData();
            switch (_scoreLevel)
            {
                case ScoreLevel.Cool:
                    data.Scale = 0.3f;
                    data.Text = GameConstants.COOL_TEXT;
                    data.Color = Color.white;
                    break;
                case ScoreLevel.Good:
                    data.Scale = 0.5f;
                    data.Text = GameConstants.GOOD_TEXT;
                    data.Color = Color.blue;
                    break;
                case ScoreLevel.Great:
                    data.Scale = 0.8f;
                    data.Text = GameConstants.GREAT_TEXT;
                    data.Color = Color.green;
                    break;
                case ScoreLevel.Perfect:
                    data.Scale = 1f;
                    data.Text = GameConstants.PERFECT_TEXT;
                    data.Color = Color.yellow;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return data;
        }
    }
}