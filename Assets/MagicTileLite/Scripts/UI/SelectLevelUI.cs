using System;
using Common.UI;
using MagicTileLite.Scripts.Mics;
using UnityEngine;
using UnityEngine.UI;

namespace Drland.MagicTileLite
{
    public class SelectLevelUI : UIController
    {
        private int _level;

        public Action<int> OnPlayGame;
        
        public void SelectLevel(int level)
        {
            _level = level;
            PlayerPrefs.SetInt(GameConstants.KEY_PREF_GAME_SPEED, _level);
            PlayerPrefs.Save();
            OnPlayGame?.Invoke(level);
        }
    }
}