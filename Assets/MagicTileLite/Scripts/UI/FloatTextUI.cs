using TMPro;
using UnityEngine;

namespace Drland.MagicTileLite
{
    public class FloatTextUI : UIElement
    {
        private TextMeshProUGUI _text;

        protected override void Awake()
        {
            base.Awake();
            _text = GetComponent<TextMeshProUGUI>();
        }

        public override void Show()
        {
            _effectTweener.Append(_effectTweener.PunchScale(_text.transform, 0.2f, 0.2f));
        }

        public void Enable(int score, Vector3 position)
        {
            transform.position = position;
            _text.text = $"+{score}";
            Show();
        }
    }
}