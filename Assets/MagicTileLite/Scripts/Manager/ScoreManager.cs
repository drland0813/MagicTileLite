using System;
using System.Runtime.InteropServices;
using Drland.MagicTileLite.UI;
using MagicTileLite.Scripts.Mics;
using UnityEngine;

namespace Drland.MagicTileLite
{
    public enum ScoreLevel
    {
        Cool = 1,
        Good = 2,
        Great = 3,
        Perfect = 5
    }

    public class ScoreData
    {
        public int Score;
        public int Streak;
        public int TotalTileCollected;
        public int TotalTile;
        public int[] StarMilestone;

        public ScoreData(int totalTile, int starNumber)
        {
            Streak = GameConstants.STREAK_RESET_VALUE;;
            StarMilestone = GetStarMilestone(totalTile, starNumber);
        }
        private int[] GetStarMilestone(int totalTile, int starNumber)
        {
            TotalTile = totalTile;
            var totalTilesPerStar = totalTile / starNumber;
            var milestones = new int[starNumber];
            for (var i = 0; i < milestones.Length; i++)
            {
                milestones[i] = totalTilesPerStar * (i + 1);
            }
            milestones[starNumber - 1] =  totalTile;
            return milestones;
        }
    }
    public class ScoreManager
    {
        private ScoreData _data;

        public ScoreManager(int totalTile)
        {
            _data = new ScoreData(totalTile, GameConstants.TOTAL_STAR);
        }
        
        public void UpdateScoreByAccurateInteraction(float accurate, InteractType interactType)
        {
            var addScoreValue = GetScoreByAccurateInteraction(accurate, interactType);
            if ((addScoreValue >= GameConstants.GREAT_SCORE))
            {
                _data.Streak++;
            }
            else
            {
                _data.Streak = GameConstants.STREAK_RESET_VALUE;;
            }
            _data.Score += addScoreValue;
            _data.TotalTileCollected++;
            GamePlayController.Instance.UI.Score.UpdateScore(_data.Score);
            GamePlayController.Instance.UI.Effect.ShowTextEffectUI(addScoreValue, _data.Streak);
            UpdateTileProcess();
            UpdateStarProcess();
        }

        public void AddBonusScore()
        {
            var addScoreValue = GameConstants.BONUS_HOLD_SCORE;
            _data.Score += addScoreValue;
            GamePlayController.Instance.UI.Score.UpdateScore(_data.Score);
        }

        private void UpdateStarProcess()
        {
            for (var i = 0; i < _data.StarMilestone.Length; i++)
            {
                var milestone = _data.StarMilestone[i];
                if (_data.TotalTileCollected < milestone) return;
                if ((_data.TotalTileCollected != milestone)) continue;
                
                GamePlayController.Instance.UI.Score.UpdateStarProcess(i);
                return;
            }
        }

        private void UpdateTileProcess()
        {
            var tileProcess = GetTileProcessNormalized();
            GamePlayController.Instance.UI.Score.UpdateTileProcess(tileProcess);
        }
        
        private int GetScoreByAccurateInteraction(float accurate, InteractType interactType)
        {
            return interactType switch
            {
                InteractType.Touch => CalcScoreByTouchInteraction(accurate),
                InteractType.Hold => CalcScoreByHoldInteraction(accurate),
                _ => throw new ArgumentOutOfRangeException(nameof(interactType), interactType, null)
            };
        }

        private int CalcScoreByTouchInteraction(float accurate)
        {
            return accurate switch
            {
                >= GameConstants.TOUCH_TILE_PERFECT_ACCURATE_MILESTONE => GameConstants.PERFECT_SCORE,
                >= GameConstants.TOUCH_TILE_GREAT_ACCURATE_MILESTONE => GameConstants.GREAT_SCORE,
                >= GameConstants.TOUCH_TILE_GOOD_ACCURATE_MILESTONE => GameConstants.GOOD_SCORE,
                _ => GameConstants.COOL_SCORE
            };
        }
        
        private int CalcScoreByHoldInteraction(float accurate)
        {
            return accurate switch
            {
                >= GameConstants.HOLD_TILE_PERFECT_ACCURATE_MILESTONE => GameConstants.PERFECT_SCORE,
                _ => GameConstants.GOOD_SCORE
            };
        }
        
        private float GetTileProcessNormalized()
        {
            var processNormalized = (float) _data.TotalTileCollected / _data.TotalTile;
            return processNormalized;
        }

        public int GetScore()
        {
            var highScore = PlayerPrefs.GetInt(GameConstants.KEY_PREF_HIGH_SCORE);
            if (_data.Score > highScore )
            {
                PlayerPrefs.SetInt(GameConstants.KEY_PREF_HIGH_SCORE, _data.Score);
                PlayerPrefs.Save();
            }
            return _data.Score;
        }
        
        public int GetHighScore()
        {
            var highScore = PlayerPrefs.GetInt(GameConstants.KEY_PREF_HIGH_SCORE);
            return highScore;
        }
    }
}