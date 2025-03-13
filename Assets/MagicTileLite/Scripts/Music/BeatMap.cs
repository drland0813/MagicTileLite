using System;
using System.Collections.Generic;


namespace Drland.MagicTileLite
{
    public enum SpawnType
    {
        SingleTouch,
        SingleHold,
        DoubleTouch,
        DoubleHold
    }
    [Serializable]
    public class Note
    {
        public int SpawnType;
        public float SpawnTime;

        public bool IsSpawnDoubleTile()
        {
            return SpawnType > 1;
        }
    }
    
    [Serializable]
    public class BeatMap
    {
        public List<Note> Notes;

        public int GetTotalTile()
        {
            var totalTile = 0;
            foreach (var note in Notes)
            {
                totalTile += note.IsSpawnDoubleTile() ? 2 : 1;
            }

            return totalTile;
        }
    }

    [Serializable]
    public class Song
    {
        public int ID;
        public string Name;
        public BeatMap BeatMap;
    }
}