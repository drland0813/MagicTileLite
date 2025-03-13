// using UnityEngine;
// using UnityEngine.Serialization;
//
// namespace Drland.MagicTileLite
// {
//     public class UIManagerOld : MonoBehaviour
//     {
//         private static UIManagerOld _instance;
//         public static UIManagerOld Instance => _instance;
//
//         [FormerlySerializedAs("_mainMenuUIPrefab")] [SerializeField] private MainMenuView _mainMenuViewPrefab;
//         [SerializeField] private EndGameUI _endGameUIPrefab;
//         [SerializeField] private SelectLevelUI _selectedUIPrefab;
//
//         
//         [SerializeField] private Transform _UITrasform;
//
//
//         private MainMenuView _mainMenuView;
//         private EndGameUI _endGameUI;
//         private SelectLevelUI _selectLevelUI;
//
//
//         private void Awake()
//         {
//             _instance = this;
//         }
//
//         public MainMenuView GetMainMenuUI(bool enable)
//         {
//             if (_mainMenuView == null)
//             {
//                 _mainMenuView = Instantiate(_mainMenuViewPrefab, _UITrasform);
//             }
//             _mainMenuView.gameObject.SetActive(enable);
//             return _mainMenuView;
//         }
//
//         public EndGameUI GetEndGameUI(bool enable)
//         {
//             if (_endGameUI == null)
//             {
//                 _endGameUI = Instantiate(_endGameUIPrefab, _UITrasform);
//             }
//             _endGameUI.gameObject.SetActive(enable);
//             return _endGameUI;
//         }
//         
//         public SelectLevelUI GetSelectedLevelUI(bool enable)
//         {
//             if (_selectLevelUI == null)
//             {
//                 _selectLevelUI = Instantiate(_selectedUIPrefab, _UITrasform);
//             }
//             _selectLevelUI.gameObject.SetActive(enable);
//             return _selectLevelUI;
//         }
//     }
// }