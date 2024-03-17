namespace MagicTileLite.Scripts.Mics
{
    public static class GameConstants
    {
        public const int TARGET_FRAMERATE = 60;

        public const string KEY_PREF_HIGH_SCORE = "HighScore";

        public const float SPAWN_TIME_GAP_LEVEL_MEDIUM = 1;
        public const float SPAWN_TIME_GAP_LEVEL_FAST = 0.85f;
        public const float SPAWN_TIME_GAP_LEVEL_ULTRA_FAST = 0.7f;
        public const float SPAWN_TIME_GAP_LEVEL_NOT_HUMAN = 0.5f;

        
        public const string PITCH_BEND_PARAM = "PitchBend";
        public const string MIXER_FILE_PATH = "Mixer/Pitch Bend Mixer";

        public const string START_TEXT = "START!";

        public const int STREAK_RESET_VALUE = -1;
        public const int COUNT_DOWN_TO_START_VALUE = 3;

        public const int COOL_SCORE = 1;
        public const int GOOD_SCORE = 2;
        public const int GREAT_SCORE = 3;
        public const int PERFECT_SCORE = 5;
        public const int BONUS_HOLD_SCORE = 3;

        public const string COOL_TEXT = "Cool!";
        public const string GOOD_TEXT = "Good!!";
        public const string GREAT_TEXT = "Greatt!";
        public const string PERFECT_TEXT = "Perfect!!!";


        public const float TOUCH_TILE_GOOD_ACCURATE_MILESTONE = 0.15f;
        public const float TOUCH_TILE_GREAT_ACCURATE_MILESTONE = 0.25f;
        public const float TOUCH_TILE_PERFECT_ACCURATE_MILESTONE = 0.5f;
        public const float HOLD_TILE_PERFECT_ACCURATE_MILESTONE = 0.15f;

        public const int TOTAL_STAR = 3;

        public const float AUDIO_PITCH_LEVEL_MEDIUM = 1;
        public const float AUDIO_PITCH_LEVEL_FAST = 1.15f;
        public const float AUDIO_PITCH_LEVEL_ULTRA_FAST = 1.3f;
        public const float AUDIO_PITCH_LEVEL_NOT_HUMAN = 1.5f;

        public const float TILE_SPEED_LEVEL_MEDIUM = 18;
        public const float TILE_SPEED_LEVEL_FAST = 21;
        public const float TILE_SPEED_LEVEL_ULTRA_FAST = 24;
        public const float TILE_SPEED_LEVEL_NOT_HUMAN = 28;
    }
}