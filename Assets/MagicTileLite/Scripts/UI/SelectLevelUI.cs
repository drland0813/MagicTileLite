using System;
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
            OnPlayGame?.Invoke(level);
        }
    }
}