using System.Collections;
using MPUIKIT;
using UnityEngine;
using TMPro;

public class CanvasComponent : MonoBehaviour
{
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
    
    private const string _spawnedBoxesText = "x";

    private bool _isVisibleStartGameIcon;
    private bool _isVisibleContinueGameIcon;
    private bool _isVisibleGameOverIcon;

    private float _normalTimeScale;
    private float _currentPowerActiveTanks = 0;
    private float _delayUpdateTextCurrentPowerActiveTanks = 0.02f;
    private int _currentScore;
    private int _currentLevel;
    private int _recordScore = 0;
    private int _recordLevel = 0;
    private int _firstVisibleTutorial = 0;

    private const string _recordScorePrefs = "RecordScorePrefs";
    private const string _recortLevelPrefs = "RecordLevelPrefs";
    private const string _firstVisibleTutorialPrefs = "FirstVisibleTutorialPrefs";

    private void Start()
    {
        InitPrefs();
        _normalTimeScale = Time.timeScale;
        _currentPowerActiveTanks = 0;
        UpdateTextCurrentPowerActiveTanks(0);
        UpdateScoreText();
        UpdateLevelTexts();

        if (_firstVisibleTutorial == 0)
        {
            SetVisibleTutorialIcon(true);
            PlayerPrefs.SetInt(_firstVisibleTutorialPrefs, ++_firstVisibleTutorial);
        }
    }

    private void OnDisable()
    {
        StopCoroutine(UpdatedTextCurrentPowerActiveTanks());
    }

    public void SetVisibleStartLevelLabel(bool isVisible)
    {
        _startLevelLabel.SetActive(isVisible);
    }
    
    public void SetVisibleStartGameIcon(bool isVisible)
    {
        _startGameIcon.SetActive(isVisible);
    }
    
    public void SetVisibleContinueGameIcon(bool isVisible)
    {
        _continueGameIcon.SetActive(isVisible);
    }

    public void UpdateTextLevel(int currentLevel)
    {
        _currentLevel = currentLevel;
        UpdateLevelTexts();
    }

    public void UpdateTextCountBoxes(int currentBoxes, float fillValue)
    {
        _countBoxes.text = _spawnedBoxesText + currentBoxes;
        _timerFill.fillAmount = fillValue;
    }

    public void UpdateTextCurrentPowerActiveTanks(float powerTank)
    {
        _currentPowerActiveTanks += powerTank;
        StartCoroutine(UpdatedTextCurrentPowerActiveTanks());
    }

    public void ResetCurrentPowerActiveTanksValue()
    {
        _currentPowerActiveTanks = 0;
    }

    public void SetVisibleGameOverIcon(bool isVisible)
    {
        _gameOverIcon.SetActive(isVisible);
    }

    public void SetVisibleNewTankIcon(bool isVisible)
    {
        _newTankIcon.SetActive(isVisible);
    }

    public void SetTextFeaturesNewTank(int levelNewTank, float powerNewTank)
    {
        _newTankLevel.text = levelNewTank.ToString();
        _newTankPower.text = powerNewTank.ToString();
    }

    public void SetRenderNewTank(Sprite render)
    {
        _tankRender.sprite = render;
    }

    public void ShowSettings()
    {
        if (_settingsIcon.active == false)
        {
            _isVisibleStartGameIcon = _startGameIcon.active;
            _isVisibleContinueGameIcon = _continueGameIcon.active;
            _isVisibleGameOverIcon = _gameOverIcon.active;

            SetVisibleStartGameIcon(false);
            SetVisibleContinueGameIcon(false);
            SetVisibleGameOverIcon(false);

            _settingsIcon.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void CloseSettings()
    {
        SetVisibleStartGameIcon(_isVisibleStartGameIcon);
        SetVisibleContinueGameIcon(_isVisibleContinueGameIcon);
        SetVisibleGameOverIcon(_isVisibleGameOverIcon);

        _settingsIcon.SetActive(false);
        Time.timeScale = _normalTimeScale;
    }

    public void GetScore(int score)
    {
        _currentScore += score;

        if(_recordScore < _currentScore)
        {
            _recordScore = _currentScore;
            PlayerPrefs.SetInt(_recordScorePrefs, _recordScore);
        }

        UpdateScoreText();
    }

    public void ResetScore()
    {
        _currentScore = 0;
        UpdateScoreText();
    }

    public void SetVisibleTutorialIcon(bool isVisible)
    {
        _settingsTutorial.SetActive(isVisible);
        _mainCanvas.SetActive(!isVisible);
    }

    private void UpdateLevelTexts()
    {
        if (_recordLevel < _currentLevel)
        {
            _recordLevel = _currentLevel;
            PlayerPrefs.SetInt(_recortLevelPrefs, _recordLevel);
        }

        _recordScoreTextStart.text = _recordScore.ToString();
        _recordScoreTextGameOver.text = _recordScore.ToString();
        _recordLevelTextStart.text = _recordLevel.ToString();
        _recordLevelTextGameOver.text = _recordLevel.ToString();
        _currentLevelTextNumber.text = _currentLevel.ToString();
        _startLevel.text = _currentLevel.ToString();
    }

    private void UpdateScoreText()
    {
        _currentScoreText.text = _currentScore.ToString();
        _recordScoreTextGameOver.text = _recordScore.ToString();
        _recordScoreTextStart.text = _recordScore.ToString();
    }

    private void InitPrefs()
    {
        _recordScore = PlayerPrefs.GetInt(_recordScorePrefs);
        _recordLevel = PlayerPrefs.GetInt(_recortLevelPrefs);
        _firstVisibleTutorial = PlayerPrefs.GetInt(_firstVisibleTutorialPrefs);
    }

    private IEnumerator UpdatedTextCurrentPowerActiveTanks()
    {
        var waitForDelaySeconds = new WaitForSeconds(_delayUpdateTextCurrentPowerActiveTanks);
        yield return waitForDelaySeconds;

        _currentPowerActiveTanksText.text = _currentPowerActiveTanks.ToString();
    }
}
