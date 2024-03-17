using System;
using System.Collections.Generic;
using MagicTileLite.Scripts.Mics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Drland.MagicTileLite
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private List<Song> _listSong;
        private Song _songSelected;
        private GameSpeed _gameSpeed;

        public void Init(GameSpeed gameSpeed)
        {
            _gameSpeed = gameSpeed;

            SetUpSong();
            ConfigAudioSource();
        }

        public void Play()
        {
            _audioSource.Play();
        }

        private void ConfigAudioSource()
        {
            var pitchBendGroup = Resources.Load<UnityEngine.Audio.AudioMixerGroup>(GameConstants.MIXER_FILE_PATH);
            _audioSource.outputAudioMixerGroup = pitchBendGroup;

            var pitch = GetAudioPitch();
            _audioSource.pitch = pitch;
            pitchBendGroup.audioMixer.SetFloat(GameConstants.PITCH_BEND_PARAM, GameConstants.AUDIO_PITCH_LEVEL_MEDIUM / pitch);
            _audioSource.outputAudioMixerGroup = pitchBendGroup;

        }

        private float GetAudioPitch()
        {
            var factor = _gameSpeed switch
            {
                GameSpeed.Medium => GameConstants.AUDIO_PITCH_LEVEL_MEDIUM,
                GameSpeed.Fast => GameConstants.AUDIO_PITCH_LEVEL_FAST,
                GameSpeed.UltraFast => GameConstants.AUDIO_PITCH_LEVEL_ULTRA_FAST,
                GameSpeed.NotHuman => GameConstants.AUDIO_PITCH_LEVEL_NOT_HUMAN,
                _ => throw new ArgumentOutOfRangeException()
            };

            return factor;
        }

        private void SetUpSong()
        {
            _songSelected = new Song();
            _songSelected.ID = 01;
            _songSelected.Name = "GoldenHour";
            _songSelected.BeatMap = new BeatMap();

            GenerateRandomNotesData();
        }

        private void GenerateRandomNotesData()
        {
            _songSelected.BeatMap.Notes = new List<Note>();
            var timeEstimate = (int)_audioSource.clip.length / 1;
            var timeSpawnGap = GetSpawnTimeGap();
            for (float i = 1; i < timeEstimate; i += timeSpawnGap)
            {
                var note = new Note
                {
                    SpawnType = Random.Range(0, 5),
                    SpawnTime = i + 1
                };
                _songSelected.BeatMap.Notes.Add(note);
            }
        }

        private float GetSpawnTimeGap()
        {
            var gameSpeed = GameplayManager.Instance.GameSpeed;
            var timeGap = gameSpeed switch
            {
                GameSpeed.Medium => GameConstants.SPAWN_TIME_GAP_LEVEL_MEDIUM,
                GameSpeed.Fast => GameConstants.SPAWN_TIME_GAP_LEVEL_FAST,
                GameSpeed.UltraFast => GameConstants.SPAWN_TIME_GAP_LEVEL_ULTRA_FAST,
                GameSpeed.NotHuman => GameConstants.SPAWN_TIME_GAP_LEVEL_NOT_HUMAN,
                _ => 1f
            };
            return timeGap;
        }

        public BeatMap GetBeatMap()
        {
            return _songSelected.BeatMap;
        }

        public void StopMusic()
        {
            _audioSource.Stop();
        }

        public float GetTimeSongRemain()
        {
            var timeSongRemain = _audioSource.clip.length - _audioSource.time;
            return timeSongRemain;
        }
    }
}