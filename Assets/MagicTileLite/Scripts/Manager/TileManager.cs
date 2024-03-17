using System;
using System.Collections.Generic;
using MagicTileLite.Scripts.Mics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Drland.MagicTileLite
{
    public class TileManager : MonoBehaviour
    {
        [SerializeField] private TouchTile _touchTileSample;
        [SerializeField] private HoldTile _holdTileSample;
        [SerializeField] private Transform _spawnTransform;

        [SerializeField] private Transform[] _linesTransform;

        private List<Tile> _tileList;
        private ObjectPool<Tile> _touchTileObjectPool;
        private ObjectPool<Tile> _holdTileObjectPool;
        private int _totalLines;
        private Vector3 _spawnPosition;
        private Vector3 _checkPointPosition;
        private bool _isEndGame;
        private float _tileFallSpeed;


        private BeatMap _beatMap;
        private int _currentBeatMapIndex;
        private GameSpeed _gameSpeed;

        private bool _isGoldenTime;

        public void Init(GameSpeed gameSpeed ,BeatMap beatMap)
        {
            _gameSpeed = gameSpeed;
            _tileFallSpeed = GetTileSpeed();
            _beatMap = beatMap;
            _tileList = new List<Tile>();
            _touchTileObjectPool = new ObjectPool<Tile>(_touchTileSample);
            _holdTileObjectPool = new ObjectPool<Tile>(_holdTileSample);
            _spawnPosition = _spawnTransform.position;
            _checkPointPosition = GameplayManager.Instance.GetCheckPointPosition();
            _totalLines = _linesTransform.Length;
        }

        public void Generate()
        {
            InitTiles();
        }
        
        public void UpdateData()
        {
            if (_isEndGame) return;
            if (GameplayManager.Instance.Sound.GetTimeSongRemain() < 0)
            {
                _isEndGame = true;
                GameplayManager.Instance.GameOver();
                return;
            }
            for (var i = 0; i < _tileList.Count; i++)
            {
                var tile = _tileList[i];
                var newPos = tile.transform.position;
                newPos.y -= _tileFallSpeed * Time.deltaTime;
                tile.UpdatePosition(newPos);
                if (!(newPos.y <= _checkPointPosition.y)) continue;
                
                if (tile.IsInteracted)
                {
                    _tileList.Remove(tile);
                    if (tile.GetInteractType() == InteractType.Hold)
                    {
                        _holdTileObjectPool.Store(tile);
                    }
                    else
                    {
                        _touchTileObjectPool.Store(tile);
                    }
                }
                else
                {
                    // if (tile.GetInteractType() == InteractType.Hold)
                    // {
                    //     _holdTileObjectPool.Store(tile);
                    // }
                    // else
                    // {
                    //     _touchTileObjectPool.Store(tile);
                    // }
                    
                    tile.Enable = false;
                    _isEndGame = true;
                    GameplayManager.Instance.GameOver();
                }
            }
        }

        public int GetTotalTile()
        {
            return _beatMap.GetTotalTile();
        }

        private void InitTiles()
        {
            Invoke(nameof(SpawnTilesFromBeatMapData), _beatMap.Notes[0].SpawnTime);
        }

        private void SpawnTilesFromBeatMapData()
        {
            if (_isEndGame) return;
            
            var type = (SpawnType)_beatMap.Notes[_currentBeatMapIndex].SpawnType;
            InitTile(type);
            _currentBeatMapIndex++;
            if (_currentBeatMapIndex >= _beatMap.Notes.Count) return;
            
            var delay = _beatMap.Notes[_currentBeatMapIndex].SpawnTime - _beatMap.Notes[_currentBeatMapIndex - 1].SpawnTime;
            Invoke(nameof(SpawnTilesFromBeatMapData), delay);
        }

        private void Spawn(int lineIndex, ObjectPool<Tile> pool)
        {
            var tile = pool.Get(_linesTransform[lineIndex]);
            var tilePos = tile.transform.position;
            tilePos.y = _spawnPosition.y;
            tile.transform.position = tilePos;

            if (!_tileList.Contains(tile))
            {
                _tileList.Add(tile);
            }

            if (tile is HoldTile)
            {
                HoldTile holdTile = (HoldTile)tile;
                holdTile.UpdateHoldSpeed(_tileFallSpeed);
            }
            
        }
        
        private void SpawnDoubleTile(ObjectPool<Tile> pool)
        {
            var linesIndex = GetDoubleLineIndex();
            for (var i = 0; i < linesIndex.Length; i++)
            {
                Spawn(linesIndex[i], pool);
            }
        }
        
        private void SpawnSingleTile(ObjectPool<Tile> pool)
        {
            var lineIndex = GetLineIndex();
            Spawn(lineIndex, pool);
        }

        private int GetRandomLineIndex()
        {
            return Random.Range(0, _totalLines);
        }

        private int GetLineIndex()
        {
            var index = GetRandomLineIndex();
            return index;
        }

        private int[] GetDoubleLineIndex()
        {
            var doubleIndex = new int[2];
            var firstIndex = GetRandomLineIndex();
            var secondIndex = GetRandomLineIndex();

            while (firstIndex == secondIndex || Mathf.Abs(firstIndex - secondIndex) == 1)
            {
                firstIndex = GetRandomLineIndex();
                secondIndex = GetRandomLineIndex();
            }

            doubleIndex[0] = firstIndex;
            doubleIndex[1] = secondIndex;

            return doubleIndex;
        }

        private void InitTile(SpawnType type)
        {
            switch (type)
            {
                case SpawnType.SingleTouch:
                    SpawnSingleTile(_touchTileObjectPool);                    
                    break;
                case SpawnType.SingleHold:
                    SpawnSingleTile(_holdTileObjectPool);                    
                    break;
                case SpawnType.DoubleTouch:
                    SpawnDoubleTile(_touchTileObjectPool);
                    break;
                case SpawnType.DoubleHold:
                    SpawnDoubleTile(_holdTileObjectPool);
                    break;
            }
        }
        
        private float GetTileSpeed()
        {
            var speed = _gameSpeed switch
            {
                GameSpeed.Medium => GameConstants.TILE_SPEED_LEVEL_MEDIUM,
                GameSpeed.Fast => GameConstants.TILE_SPEED_LEVEL_FAST,
                GameSpeed.UltraFast => GameConstants.TILE_SPEED_LEVEL_ULTRA_FAST,
                GameSpeed.NotHuman => GameConstants.TILE_SPEED_LEVEL_NOT_HUMAN,
                _ => throw new ArgumentOutOfRangeException()
            };
            return speed;
        }
    }
}