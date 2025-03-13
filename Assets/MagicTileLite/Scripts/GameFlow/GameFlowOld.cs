// using System.Collections;
// using Common.UI;
// using MagicTileLite.Scripts.Mics;
// using UnityEngine;
// using UnityEngine.Serialization;
//
//
// namespace Drland.MagicTileLite
// {
//     public class GameFlowOld : MonoBehaviour
//     {
//         private static GameFlowOld _instance;
//         public static GameFlowOld Instance => _instance;
//
//         [SerializeField] private Transform _gamePlayTransform;
//         [SerializeField] private GamePlayController _gamePlayPrefab;
//         [FormerlySerializedAs("_uiManagerOld")] [SerializeField] private UIManager _uiManager;
//
//         private GamePlayController _gamePlay;
//         private void Awake()
//         {
//
//             _instance = this;
//             DontDestroyOnLoad(this);
//         }
//
//         private void Start()
//         {
//             // return;
//             Init();
//         }
//
//         private void Init()
//         {
//             StartCoroutine(InitCoroutine());
//         }
//     
//     
//         private IEnumerator InitCoroutine()
//         {
//             InitUI();
//             yield return null;
//         }
//
//         private void InitUI()
//         {
//             var mainMenuUI = _uiManager.GetMainMenuUI(true);
//             mainMenuUI.OnPlay += GoToSelectLevel;
//             mainMenuUI.OnExit += ExitGame;
//             
//             var selectLevel = _uiManager.GetSelectedLevelUI(false);
//             selectLevel.OnPlayGame += PlayGame;
//         }
//         private void ExitGame()
//         {
//             Application.Quit();
//         }
//
//         private void PlayGame(int level)
//         {
//             var selectLevel = _uiManager.GetSelectedLevelUI(false);
//             _gamePlay = Instantiate(_gamePlayPrefab, _gamePlayTransform);
//             _gamePlay.Init(level);
//         }
//         
//         private void GoToSelectLevel()
//         {
//             var mainMenuUI = _uiManager.GetMainMenuUI(false);
//             var selectLevel = _uiManager.GetSelectedLevelUI(true);
//         }
//     }
// }
//
//
