using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Slider volumeSlider;
    public GameObject HomeScreen;
    public GameObject SettingsScreen;
    public GameObject GamePlayScreen;
    public GameObject GameOverScreen;
    public GameObject WinGameScreen;
    public GameObject TutorialScreen;
    public bool hasTimer;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        PauseGame();
        volumeSlider.onValueChanged.AddListener(AdjustVolume);
        HomeScreen.SetActive(true);
        SettingsScreen.SetActive(false);
        GamePlayScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        WinGameScreen.SetActive(false);
        TutorialScreen.SetActive(false);
        float sound = PlayerPrefs.GetFloat("Sound", 1);
        AudioListener.volume = sound;
        volumeSlider.value = sound;

        SoundManager.instance.BackGroundVolume(1);
        SoundManager.instance.BackGroundSound(true);
    }
    public void PauseGame()
    {
        GameManager.instance.IsGameStarted = false;
    }
    #region HomeScreenMethods
    public void HomeScreenPlayButton(bool hasTimer)
    {
        HomeScreen.SetActive(false);
        GamePlayScreen.SetActive(true);
        GameManager.instance.IsGameStarted = true;
        GameManager.instance.NewGame();
        SoundManager.instance.BackGroundVolume(0);
        //GameManager.instance.GameItems.SetActive(true);
        this.hasTimer = hasTimer;
    }

    public void HomeScreenTutorialButton()
    {
        HomeScreen.SetActive(false);
        TutorialScreen.SetActive(true);
    }

    public void HomeScreenSettingsButton()
    {
        HomeScreen.SetActive(false);
        SettingsScreen.SetActive(true);
    }
    public void HomeScreenQuitButton()
    {
        Application.Quit();
    }
    #endregion

    #region SettingsScreenMethods
    public void SettingsBackButton()
    {
        float sound = PlayerPrefs.GetFloat("Sound", 1);
        AudioListener.volume = sound;
        volumeSlider.value = sound;
        SettingsScreen.SetActive(false);
        HomeScreen.SetActive(true);
        //revertSoundLastState();
    }
    public void SettingsSaveButton()
    {

        PlayerPrefs.SetFloat("Sound", AudioListener.volume);

        SettingsScreen.SetActive(false);
        HomeScreen.SetActive(true);
        //SaveSoundState();
    }

    public void AdjustVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    #endregion

    
    #region GamePlayScreenBackButton
    public void GamePlayScreenBackButton()
    {
        PauseGame();
        GamePlayScreen.SetActive(false);
        HomeScreen.SetActive(true);
        SoundManager.instance.BackGroundVolume(1);
        SoundManager.instance.BackGroundSound(true);
        GameManager.instance.IsGameStarted = false;
        //revertSoundLastState();
    }
    #endregion

    #region TutorialScrren
        public void TutorialScreenBackButton()
        {
            TutorialScreen.SetActive(false);
            HomeScreen.SetActive(true);
        }
    #endregion

    #region GameOverScreenMethods
    public void GameOverScreenBackButton()
    {
        PauseGame();
        GameOverScreen.SetActive(false);
        HomeScreen.SetActive(true);
        //revertSoundLastState();
    }
    public void GameOverScreenTryAgainButton()
    {
        GameOverScreen.SetActive(false);
        GamePlayScreen.SetActive(true);
        GameManager.instance.IsGameStarted = true;
        GameManager.instance.NewGame();
        SoundManager.instance.BackGroundVolume(0);
    }

    public void EnableGameOverScreen()
    {

        PauseGame();
        GamePlayScreen.SetActive(false);
        GameOverScreen.SetActive(true);
    }
    #endregion
    #region WinScreenMethods
    public void WinScreenBackButton()
    {
        PauseGame();
        WinGameScreen.SetActive(false);
        HomeScreen.SetActive(true);
        //revertSoundLastState();
    }
    public void WinScreenNextLevelButton()
    {
        WinGameScreen.SetActive(false);
        GamePlayScreen.SetActive(true);
        GameManager.instance.IsGameStarted = true;
        GameManager.instance.NewGame();
        SoundManager.instance.BackGroundVolume(0);
    }
    public void EnableWinScreen()
    {
        PauseGame();
        GamePlayScreen.SetActive(false);
        WinGameScreen.SetActive(true);
    }
    #endregion
}
