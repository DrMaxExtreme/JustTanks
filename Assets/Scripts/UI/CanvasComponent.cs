using JustTanks.GameLogic;
using MPUIKIT;
using System.Collections;
using TMPro;
using UnityEngine;

namespace JustTanks.UI
{
    public class CanvasComponent : MonoBehaviour
    {
        private const string _spawnedBoxesText = "x";
        private const string _recordScorePrefs = "RecordScorePrefs";
        private const string _recordLevelPrefs = "RecordLevelPrefs";
        private const string _firstVisibleTutorialPrefs = "FirstVisibleTutorialPrefs";

        [SerializeField] private GameObject _mainCanvas;
        [SerializeField] private GameObject _startLevelLabel;
        [SerializeField] private GameObject _startGameIcon;
        [SerializeField] private GameObject _continueGameIcon;
        [SerializeField] private GameObject _gameOverIcon;
        [SerializeField] private GameObject _newTankIcon;
        [SerializeField] private GameObject _settingsIcon;
        [SerializeField] private GameObject _settingsTutorial;
        [SerializeField] private TMP_Text _currentLevelTextNumber;
        [SerializeField] private TMP_Text _startLevel;
        [SerializeField] private MPImage _timerFill;
        [SerializeField] private TMP_Text _countBoxes;
        [SerializeField] private TMP_Text _newTankLevel;
        [SerializeField] private TMP_Text _newTankPower;
        [SerializeField] private MPImage _tankRender;
        [SerializeField] private TMP_Text _currentPowerActiveTanksText;
        [SerializeField] private TMP_Text _currentScoreText;
        [SerializeField] private TMP_Text _recordScoreTextGameOver;
        [SerializeField] private TMP_Text _recordLevelTextGameOver;
        [SerializeField] private TMP_Text _recordScoreTextStart;
        [SerializeField] private TMP_Text _recordLevelTextStart;

        private UIVisibilityManager _uiVisibilityManager;
        private UITextManager _uiTextManager;
        private GameStateManager _gameStateManager;
        private PrefsManager _prefsManager;
        private LevelController _levelController;

        private bool _isVisibleStartGameIcon;
        private bool _isVisibleContinueGameIcon;
        private bool _isVisibleGameOverIcon;
        private float _normalTimeScale;
        private float _delayUpdateTextCurrentPowerActiveTanks = 0.02f;

        private void Awake()
        {
            _uiVisibilityManager = new UIVisibilityManager();
            _uiTextManager = new UITextManager();
            _gameStateManager = new GameStateManager();
            _prefsManager = new PrefsManager();
            _levelController = new LevelController();
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _gameStateManager.ResetCurrentPowerActiveTanks();
            _normalTimeScale = Time.timeScale;

            UpdateLevelTexts(1);
            UpdateScoreText();

            if (_prefsManager.LoadInt(_firstVisibleTutorialPrefs) == 0)
            {
                SetVisibleTutorialIcon(true);
                _prefsManager.SaveInt(_firstVisibleTutorialPrefs, 1);
            }
        }

        private void OnDisable()
        {
            StopCoroutine(UpdatedTextCurrentPowerActiveTanks());
        }

        public void SetVisibleStartLevelLabel(bool isVisible)
        {
            _uiVisibilityManager.SetVisible(_startLevelLabel, isVisible);
        }

        public void SetVisibleStartGameIcon(bool isVisible)
        {
            _uiVisibilityManager.SetVisible(_startGameIcon, isVisible);
        }

        public void SetVisibleContinueGameIcon(bool isVisible)
        {
            _uiVisibilityManager.SetVisible(_continueGameIcon, isVisible);
        }

        public void SetVisibleGameOverIcon(bool isVisible)
        {
            _uiVisibilityManager.SetVisible(_gameOverIcon, isVisible);
        }

        public void SetVisibleNewTankIcon(bool isVisible)
        {
            _uiVisibilityManager.SetVisible(_newTankIcon, isVisible);
        }

        public void SetVisibleTutorialIcon(bool isVisible)
        {
            _uiVisibilityManager.SetVisible(_settingsTutorial, isVisible);
            _uiVisibilityManager.SetVisible(_mainCanvas, !isVisible);
        }

        public void UpdateTextLevel(int currentLevel)
        {
            if(currentLevel > _prefsManager.LoadInt("RecordLevelPrefs"))
                _gameStateManager.SaveRecordLevel(currentLevel);

            UpdateLevelTexts(currentLevel);
        }

        public void UpdateTextCountBoxes(int currentBoxes)
        {
            _uiTextManager.UpdateText(_countBoxes, _spawnedBoxesText + currentBoxes);
        }

        public void UpdateFillDelaySpawnBoxes(float fillValue)
        {
            _uiTextManager.UpdateFillAmount(_timerFill, fillValue);
        }

        public void UpdateTextCurrentPowerActiveTanks(float powerTank)
        {
            _gameStateManager.AddPowerToTanks(powerTank);
            StartCoroutine(UpdatedTextCurrentPowerActiveTanks());
        }

        public void ResetCurrentPowerActiveTanksValue()
        {
            _gameStateManager.ResetCurrentPowerActiveTanks();
        }

        public void SetTextFeaturesNewTank(int levelNewTank, float powerNewTank)
        {
            _uiTextManager.UpdateText(_newTankLevel, levelNewTank.ToString());
            _uiTextManager.UpdateText(_newTankPower, powerNewTank.ToString());
        }

        public void SetRenderNewTank(Sprite render)
        {
            _uiTextManager.SetRender(_tankRender, render);
        }

        public void ShowSettings()
        {
            if (!_settingsIcon.activeSelf)
            {
                _isVisibleStartGameIcon = _startGameIcon.activeSelf;
                _isVisibleContinueGameIcon = _continueGameIcon.activeSelf;
                _isVisibleGameOverIcon = _gameOverIcon.activeSelf;

                SetVisibleStartGameIcon(false);
                SetVisibleContinueGameIcon(false);
                SetVisibleGameOverIcon(false);

                _uiVisibilityManager.SetVisible(_settingsIcon, true);
                Time.timeScale = 0;
            }
        }

        public void CloseSettings()
        {
            SetVisibleStartGameIcon(_isVisibleStartGameIcon);
            SetVisibleContinueGameIcon(_isVisibleContinueGameIcon);
            SetVisibleGameOverIcon(_isVisibleGameOverIcon);

            _uiVisibilityManager.SetVisible(_settingsIcon, false);
            Time.timeScale = _normalTimeScale;
        }

        public void GetScore(int score)
        {
            _gameStateManager.AddScore(score);
            UpdateScoreText();
        }

        public void ResetScore()
        {
            _gameStateManager.ResetScore();
            UpdateScoreText();
        }

        private void UpdateLevelTexts(int currentLevel)
        {
            int recordLevel = _gameStateManager.LoadRecordLevel();

            _uiTextManager.UpdateText(_recordScoreTextStart, _gameStateManager.GetRecordScore().ToString());
            _uiTextManager.UpdateText(_recordScoreTextGameOver, _gameStateManager.GetRecordScore().ToString());
            _uiTextManager.UpdateText(_recordLevelTextStart, recordLevel.ToString());
            _uiTextManager.UpdateText(_recordLevelTextGameOver, recordLevel.ToString());
            _uiTextManager.UpdateText(_currentLevelTextNumber, currentLevel.ToString());
            _uiTextManager.UpdateText(_startLevel, recordLevel.ToString());
        }

        private void UpdateScoreText()
        {
            _uiTextManager.UpdateText(_currentScoreText, _gameStateManager.GetCurrentScore().ToString());
            _uiTextManager.UpdateText(_recordScoreTextGameOver, _gameStateManager.GetRecordScore().ToString());
            _uiTextManager.UpdateText(_recordScoreTextStart, _gameStateManager.GetRecordScore().ToString());
        }

        private IEnumerator UpdatedTextCurrentPowerActiveTanks()
        {
            var waitForDelaySeconds = new WaitForSeconds(_delayUpdateTextCurrentPowerActiveTanks);
            yield return waitForDelaySeconds;

            _uiTextManager.UpdateText(_currentPowerActiveTanksText, _gameStateManager.GetCurrentPowerActiveTanks().ToString());
        }
    }
}
