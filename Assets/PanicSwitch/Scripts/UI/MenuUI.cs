using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public static bool IsGame;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject GameUIWrapper;
    [SerializeField] private GameObject MenuUIWrapper;
    [SerializeField] private GameObject[] PlayerInfoLevel;
    [SerializeField] private TextMeshProUGUI[] RecordLevelDisplayText;
    private PlayerData _playerData;
    [SerializeField] TMP_InputField _usernameText;
    private AudioManager _audioManager;
    private Wallet _wallet;
    private AppMetEvents _appMetEvents;
    private RandomChest _randomChest;
    //private AppMetEvents _appMetEvents;

    private void Awake()
    {
        IsGame = false;
        PlayerInfoLevel[1].SetActive(false);
        PlayerInfoLevel[0].SetActive(true);
        _audioManager = GetComponent<AudioManager>();
        _playerData = FindObjectOfType<PlayerData>();
        _appMetEvents = FindObjectOfType<AppMetEvents>();
        _randomChest = FindObjectOfType<RandomChest>();
    }

    private void Start()
    {
        // _wallet = GetComponent<GameUI>()._wallet; 
        // _appMetEvents = FindObjectOfType<AppMetEvents>();

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        RecordLevelDisplayText[0].text = (_playerData.GetRecordLevel() + 1).ToString();
        RecordLevelDisplayText[1].text = (_playerData.GetRecordLevel() + 1).ToString();

        if (PlayerPrefs.HasKey("NextLevel"))
        {
            StartGame();
            PlayerPrefs.DeleteKey("NextLevel");
        }

        

        if (PlayerPrefs.HasKey("username"))
            _usernameText.text = PlayerPrefs.GetString("username");
    }

    public void SaveUserName()
    {
        PlayerPrefs.SetString("username", _usernameText.text);
    }

    public void StartGame()
    {
        if (PlayerPrefs.HasKey("TutorialUI"))
        {
            IsGame = true;
        }

        if (!PlayerPrefs.HasKey("_waveLevelContinue"))
        {
            QuestManager.ContinueCounter = 0;
            _appMetEvents.LevelStartEvent("normal", (_playerData.GetLevel() + 1), GameUI.gameCouner);
        }


        _randomChest.StartChestTimer();
        _pauseButton.SetActive(true);
        _audioManager.SFXPlay(7);
        GameUIWrapper.SetActive(true);
        MenuUIWrapper.SetActive(false);
        PlayerInfoLevel[0].SetActive(false);
        PlayerInfoLevel[1].SetActive(true);
        // _musicDelay.StartMusic();
        Time.timeScale = AdsButton.TimeScaleValue;
    }
}
