using System;
using System.Collections;
using Common.UI;
using DG.Tweening;
using Drland.MagicTileLite.UI;
using MagicTileLite.Scripts.Mics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Drland.MagicTileLite
{
    public enum GameState
    {
        WaitToStart,
        Playing,
        GameOver
    }

    public enum GameSpeed
    {
        Medium = 1,
        Fast = 2,
        UltraFast = 3,
        NotHuman = 4
    }
    
    [RequireComponent(typeof(EventTrigger))]

    public class GamePlayController : MonoBehaviour
    {
        private static GamePlayController _instance;
        public static GamePlayController Instance => _instance;
        
        [SerializeField] private Transform _scoreCheckPointTransform;
        [SerializeField] private Transform _gameOverCheckPointTransform;

        [FormerlySerializedAs("_gameplayUI")]
        [Header("_UI")]
        [SerializeField] GamePlayView _gamePlayView;
        
        [SerializeField] private TileManager _tileManager;
        [SerializeField] BackgroundManager _backgroundManager;
        [SerializeField] private SoundManager _soundManager;
       
        private ScoreManager _scoreManager;

        private GameState _gameState;
        private GameSpeed _gameSpeed;
        public GameSpeed GameSpeed => _gameSpeed;

        public SoundManager Sound => _soundManager;
        public GamePlayView UI => _gamePlayView;
        public BackgroundManager Background => _backgroundManager;
        
        public Action OnWin;
        public Action OnLose;
        
        public void BackToMainMenu()
        {
            OnLose?.Invoke();
        }

        private void Awake()
        {
            _instance = this;
        }

        private void OnEnable()
        {
            _gamePlayView.OnGameStart += WaitToStartComplete;
        }

        private void OnDisable()
        {
            _gamePlayView.OnGameStart -= WaitToStartComplete;
        }
        
        public void Init(int level)
        {
            _gameSpeed = (GameSpeed)level;
            StartCoroutine(InitCoroutine());
        }

        private void Update()
        {
            switch (_gameState)
            {
                case GameState.Playing:
                    _tileManager.UpdateData();
                    break;
            }
        }
        
        private IEnumerator InitCoroutine()
        {
            _gameState = GameState.WaitToStart;
            
            _soundManager.Init(_gameSpeed);
            _tileManager.Init(_gameSpeed, _soundManager.GetBeatMap());
            _scoreManager = new ScoreManager(_tileManager.GetTotalTile());
            
            _gamePlayView.CountDownToStart(GameConstants.COUNT_DOWN_TO_START_VALUE);
            yield return null;
        }
        
        private void WaitToStartComplete()
        {
            _gameState = GameState.Playing;
            
            _soundManager.Play();
            _tileManager.Generate();
        }

        public void GameOver()
        {
            _gameState = GameState.GameOver;
            _soundManager.StopMusic();
            var score = _scoreManager.GetScore();
            var highScore = _scoreManager.GetHighScore();
            var endGameUI = UIManager.Instance.ShowUIOnTop<EndGameUI>("EndGameUI");
            endGameUI.OnReplay = () =>
            {
                GameFlow.RequestStateChange(this, new GamePlayPayload());
                UIManager.Instance.ReleaseUI(endGameUI, true);
            };
            endGameUI.OnBackMenu = () =>
            {
                GameFlow.RequestStateChange(this, new MainMenuPayload());
                UIManager.Instance.ReleaseUI(endGameUI, true);
            };
            endGameUI.Show(score, highScore);
            UIManager.Instance.ReleaseUI(_gamePlayView, true);
        }

        public void AddScore(float accurateInteraction, InteractType interactType)
        {
            _scoreManager.UpdateScoreByAccurateInteraction(accurateInteraction, interactType);
        }
        
        public void AddBonusScore()
        {
            _scoreManager.AddBonusScore();
        }
        
        public Vector3 GetCheckPointPosition()
        {
            return _scoreCheckPointTransform.position;
        }
    }
}