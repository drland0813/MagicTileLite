using TMPro;
using UnityEngine;

namespace Drland.MagicTileLite
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private PlayProcessUI _playProcessUI;
        private EffectTweener _effectTweener;

        private void Awake()
        {
            _effectTweener = GetComponent<EffectTweener>();
        }
        
        public void UpdateScore(int scoreValue)
        {
            if (_scoreText.rectTransform.localScale != Vector3.one)
            {
                _effectTweener.Kill();
            }
            _effectTweener.PunchScale(_scoreText.rectTransform, 0.5f, 0.2f);
            _scoreText.text = scoreValue.ToString();
        }

        public void UpdateTileProcess(float newProcessValue)
        {
            _playProcessUI.UpdateTileProcess(newProcessValue);
        }

        public void UpdateStarProcess(int starNumber)
        {
            _playProcessUI.UpdateStarProcess(starNumber);
        }
    }
}