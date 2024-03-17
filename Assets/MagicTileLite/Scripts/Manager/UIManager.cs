using UnityEngine;

namespace Drland.MagicTileLite
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;
        public static UIManager Instance => _instance;

        [SerializeField] private MainMenuUI _mainMenuUIPrefab;
        [SerializeField] private EndGameUI _endGameUIPrefab;
        [SerializeField] private SelectLevelUI _selectedUIPrefab;

        
        [SerializeField] private Transform _UITrasform;


        private MainMenuUI _mainMenuUI;
        private EndGameUI _endGameUI;
        private SelectLevelUI _selectLevelUI;


        private void Awake()
        {
            _instance = this;
        }

        public MainMenuUI GetMainMenuUI(bool enable)
        {
            if (_mainMenuUI == null)
            {
                _mainMenuUI = Instantiate(_mainMenuUIPrefab, _UITrasform);
            }
            _mainMenuUI.gameObject.SetActive(enable);
            return _mainMenuUI;
        }

        public EndGameUI GetEndGameUI(bool enable)
        {
            if (_endGameUI == null)
            {
                _endGameUI = Instantiate(_endGameUIPrefab, _UITrasform);
            }
            _endGameUI.gameObject.SetActive(enable);
            return _endGameUI;
        }
        
        public SelectLevelUI GetSelectedLevelUI(bool enable)
        {
            if (_selectLevelUI == null)
            {
                _selectLevelUI = Instantiate(_selectedUIPrefab, _UITrasform);
            }
            _selectLevelUI.gameObject.SetActive(enable);
            return _selectLevelUI;
        }
    }
}