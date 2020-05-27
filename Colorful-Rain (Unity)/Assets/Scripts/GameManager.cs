using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Load Game")]
    public LoadGame loadGame;
    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject helpMenu;
    public GameObject howToPlayMenu;
    public GameObject creditsMenu;
    public GameObject levelSelectionMenu;
    public GameObject colorSelectionMenu;
    [Header("Gameplay Objects")]
    public GameObject player;
    public GameObject dropSpawner;
    public GameObject b1Slider;
    public GameObject b2Slider;
    public GameObject circleArtGenerator;
    [Header("Gameplay Menus")]
    public GameObject pauseMenu;
    public GameObject levelCompleteMenu;
    [Header("Level Complete Objects")]
    public TMPro.TextMeshProUGUI dataText;
    public TMPro.TextMeshProUGUI gradeText;
    [Header("Audio Settings")]
    public GameObject soundOffBtn;
    public GameObject soundOnBtn;
    public GameObject musicOffBtn;
    public GameObject musicOnBtn;

    private int wastedDrops;
    private int totalDrops;
    private string grade;
    private float perc;
    private bool isGamePaused = false;
    private bool playMusic = true;

    private void Awake()
    {
        LoadMainMenuSceneComponents();
        // DontDestroyOnLoad(gameObject);
        PlayerPrefs.SetInt("Music", 1);

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (PlayerPrefs.GetInt("Music", -1) == 1)
            {
                playMusic = true;
                FindObjectOfType<AudioManager>().soundEffectAudio.loop = true;
                FindObjectOfType<AudioManager>().soundEffectAudio.clip = FindObjectOfType<AudioManager>().menuJingle;
                FindObjectOfType<AudioManager>().soundEffectAudio.Play();
            }
            else if (PlayerPrefs.GetInt("Music", -1) == 0)
                playMusic = false;
            else
                PlayerPrefs.SetInt("Music", 1);
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            if (PlayerPrefs.GetInt("Music", -1) == 1)
            {
                playMusic = true;
                FindObjectOfType<AudioManager>().soundEffectAudio.loop = true;
                FindObjectOfType<AudioManager>().soundEffectAudio.clip = FindObjectOfType<AudioManager>().gameJingle;
                FindObjectOfType<AudioManager>().soundEffectAudio.Play();
            }
            else if (PlayerPrefs.GetInt("Music", -1) == 0)
                playMusic = false;
            else
                PlayerPrefs.SetInt("Music", 1);
        }
        else
        {
            FindObjectOfType<AudioManager>().soundEffectAudio.loop = false;
            FindObjectOfType<AudioManager>().soundEffectAudio.clip = null;
        }

        if (PlayerPrefs.GetInt("Mute") == 1)
        {
            FindObjectOfType<AudioManager>().soundEffectAudio.mute = true;
        }
        else
        {
            FindObjectOfType<AudioManager>().soundEffectAudio.mute = false;
        }

        if (PlayerPrefs.GetInt("Music") == 1)
        {
            playMusic = true;
        }
        else
        {
            playMusic = false;
        }
    }

    public void LoadMainMenuSceneComponents() 
    { 
        // Load MainMenu Scene components
        if (mainMenu == null || howToPlayMenu == null || levelSelectionMenu == null || colorSelectionMenu == null)
        {
            GameObject[] gos = FindObjectsOfType<GameObject>();
            foreach(GameObject g in gos)
            {
                if (g.CompareTag("MainMenu"))
                {
                    mainMenu = g;
                }
                else if (g.CompareTag("HowToPlay"))
                {
                    howToPlayMenu = g;
                }
                else if (g.CompareTag("LevelSelection"))
                {
                    levelSelectionMenu = g;
                }
                else if (g.CompareTag("ColourSelection"))
                {
                    colorSelectionMenu = g;
                }
            }
        }
    }

    public void LoadGameSceneComponents()
    {
        // Load Game Scene components
        if (player == null || dropSpawner == null || b1Slider == null || b2Slider == null || circleArtGenerator == null
            || pauseMenu == null || levelCompleteMenu == null)
        {
            GameObject[] gos = FindObjectsOfType<GameObject>();
            foreach (GameObject g in gos)
            {
                if (g.CompareTag("Player"))
                {
                    player = g;
                }
                else if (g.CompareTag("DropSpawner"))
                {
                    dropSpawner = g;
                }
                else if (g.CompareTag("B1Slider"))
                {
                    b1Slider = g;
                }
                else if (g.CompareTag("B2Slider"))
                {
                    b2Slider = g;
                }
                else if (g.CompareTag("CircleArt"))
                {
                    circleArtGenerator = g;
                    circleArtGenerator.SetActive(false);
                }
                else if (g.CompareTag("PauseMenu"))
                {
                    pauseMenu = g;
                    pauseMenu.SetActive(false);
                }
                else if (g.CompareTag("LevelComplete"))
                {
                    levelCompleteMenu = g;
                    levelCompleteMenu.SetActive(false);
                }
            }
        }
    }

    // Menu Management
    public void ViewMainMenu()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.buttonSound);

        mainMenu.SetActive(true);
        helpMenu.SetActive(false);
        howToPlayMenu.SetActive(false);
        creditsMenu.SetActive(false);
        levelSelectionMenu.SetActive(false);
        colorSelectionMenu.SetActive(false);

        CheckSoundSettings();
    }

    public void ViewHelpMenu()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.buttonSound);

        mainMenu.SetActive(false);
        helpMenu.SetActive(true);
        howToPlayMenu.SetActive(false);
        creditsMenu.SetActive(false);
        levelSelectionMenu.SetActive(false);
        colorSelectionMenu.SetActive(false);
    }
    public void ViewHowToPlayMenu()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.buttonSound);

        mainMenu.SetActive(false);
        helpMenu.SetActive(false);
        howToPlayMenu.SetActive(true);
        creditsMenu.SetActive(false);
        levelSelectionMenu.SetActive(false);
        colorSelectionMenu.SetActive(false);
    }

    public void ViewCreditsMenu()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.buttonSound);

        mainMenu.SetActive(false);
        helpMenu.SetActive(false);
        howToPlayMenu.SetActive(false);
        creditsMenu.SetActive(true);
        levelSelectionMenu.SetActive(false);
        colorSelectionMenu.SetActive(false);
    }

    public void ViewLevelSelectionMenu()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.buttonSound);

        mainMenu.SetActive(false);
        helpMenu.SetActive(false);
        howToPlayMenu.SetActive(false);
        creditsMenu.SetActive(false);
        levelSelectionMenu.SetActive(true);
        colorSelectionMenu.SetActive(false);
    }
    public void ViewColorSelectionMenu()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.buttonSound);

        ColorPicker[] cp = colorSelectionMenu.GetComponentsInChildren<ColorPicker>();
        FlexibleColorPicker[] fcps = colorSelectionMenu.GetComponentsInChildren<FlexibleColorPicker>();
        foreach(FlexibleColorPicker fcp in fcps)
        {
            if (fcp.gameObject.name.Equals("Bucket1_ColorPicker"))
                fcp.startingColor = cp[0].bucket1.color;
            else if (fcp.gameObject.name.Equals("Bucket2_ColorPicker"))
                fcp.startingColor = cp[0].bucket2.color;
        }

        mainMenu.SetActive(false);
        helpMenu.SetActive(false);
        howToPlayMenu.SetActive(false);
        creditsMenu.SetActive(false);
        levelSelectionMenu.SetActive(false);
        colorSelectionMenu.SetActive(true);
    }

    public void StartGame()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.buttonSound);

        //mainMenu.SetActive(true);
        helpMenu.SetActive(false);
        howToPlayMenu.SetActive(false);
        creditsMenu.SetActive(false);
        levelSelectionMenu.SetActive(false);
        colorSelectionMenu.SetActive(false);

        SceneManager.LoadScene("Game");
        LoadGameSceneComponents();
    }

    public void PauseCurrentGame()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.buttonSound);

        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0;
            // Disable scripts that still work while timescale is set to 0
            player.SetActive(false);
            dropSpawner.SetActive(false);
            circleArtGenerator.SetActive(false);
            b1Slider.SetActive(false);
            b2Slider.SetActive(false);

            //pauseBtn.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "RESUME";
            pauseMenu.SetActive(true);
            CheckSoundSettings();
        }
        else
        {
            Time.timeScale = 1;
            // Disable scripts that still work while timescale is set to 0
            player.SetActive(true);
            dropSpawner.SetActive(true);
            circleArtGenerator.SetActive(false);
            b1Slider.SetActive(true);
            b2Slider.SetActive(true);

            //pauseBtn.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "PAUSE";
            pauseMenu.SetActive(false);
            soundOffBtn.SetActive(false);
            soundOnBtn.SetActive(false);
            //CheckSoundSettings();
        }
    }

    public void GoToPauseMenu()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.buttonSound);

        player.SetActive(false);
        dropSpawner.SetActive(false);
        circleArtGenerator.SetActive(false);
        b1Slider.SetActive(false);
        b2Slider.SetActive(false);

        //pauseBtn.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "RESUME";
        pauseMenu.SetActive(true);
        helpMenu.SetActive(false);
        CheckSoundSettings();
    }

    public void RestartGame()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.buttonSound);

        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
        LoadGameSceneComponents();
    }

    public void GoToLevelSelection()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.buttonSound);
        // LoadMainMenuSceneComponents();
        levelCompleteMenu.SetActive(false);
        levelSelectionMenu.SetActive(true);
        colorSelectionMenu.SetActive(false);
    }

    public void GoToColorSelection()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.buttonSound);

        ColorPicker[] cp = colorSelectionMenu.GetComponentsInChildren<ColorPicker>();
        FlexibleColorPicker[] fcps = colorSelectionMenu.GetComponentsInChildren<FlexibleColorPicker>();
        foreach (FlexibleColorPicker fcp in fcps)
        {
            if (fcp.gameObject.name.Equals("Bucket1_ColorPicker"))
                fcp.startingColor = cp[0].bucket1.color;
            else if (fcp.gameObject.name.Equals("Bucket2_ColorPicker"))
                fcp.startingColor = cp[0].bucket2.color;
        }

        levelCompleteMenu.SetActive(false);
        levelSelectionMenu.SetActive(false);
        colorSelectionMenu.SetActive(true);
    }

    public void GoToMainMenu()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.buttonSound);

        SceneManager.LoadScene("MainMenu");
    }

    public void GoToHelpMenu()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.buttonSound);

        pauseMenu.SetActive(false);
        helpMenu.SetActive(true);
        howToPlayMenu.SetActive(false);
        creditsMenu.SetActive(false);
        levelSelectionMenu.SetActive(false);
        colorSelectionMenu.SetActive(false);
    }
    public void GoToHowToPlayMenu()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.buttonSound);

        pauseMenu.SetActive(false);
        helpMenu.SetActive(false);
        howToPlayMenu.SetActive(true);
        creditsMenu.SetActive(false);
        levelSelectionMenu.SetActive(false);
        colorSelectionMenu.SetActive(false);
    }

    public void GoToCreditsMenu()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.buttonSound);

        pauseMenu.SetActive(false);
        helpMenu.SetActive(false);
        howToPlayMenu.SetActive(false);
        creditsMenu.SetActive(true);
        levelSelectionMenu.SetActive(false);
        colorSelectionMenu.SetActive(false);
    }

    public void ViewLevelComplete()
    {
        CalculateAndDisplayPoints();

        player.SetActive(false);
        dropSpawner.GetComponent<DropSpawner>().DestroyRemainingDrops();
        dropSpawner.SetActive(false);
        b1Slider.SetActive(false);
        b2Slider.SetActive(false);
        levelCompleteMenu.SetActive(true);
        levelSelectionMenu.SetActive(false);
        colorSelectionMenu.SetActive(false);
        pauseMenu.SetActive(false);
        circleArtGenerator.GetComponent<CircleArt>().maxNrOfCircles = totalDrops;
        circleArtGenerator.SetActive(true);

        Debug.Log("Current Level: " + PlayerPrefs.GetInt("CurrentLevel"));
        if(perc >= 75)
        {
            int nextLevel = PlayerPrefs.GetInt("CurrentLevel") + 1;
            string levelKey = "Level" + nextLevel + "Enabled";
            loadGame.UnlockLevel(levelKey);
        }
    }

    public void CalculateAndDisplayPoints()
    {
        totalDrops = dropSpawner.GetComponent<DropSpawner>().GetTotalNumberOfDrops();
        wastedDrops = dropSpawner.GetComponent<DropSpawner>().GetNumberOfWastedDrops();
        int dropsCollected = totalDrops - wastedDrops;
        perc = (dropsCollected * 100) / totalDrops;
        if (perc == 100)
        {
            grade = "A+";
        }
        else if (perc >= 85 && perc < 100)
        {
            grade = "A";
        }
        else if (perc >= 75 && perc < 85)
        {
            grade = "A-";
        }
        else if (perc >= 70 && perc < 75)
        {
            grade = "B+";
        }
        else if (perc >= 65 && perc < 70)
        {
            grade = "B";
        }
        else if (perc >= 60 && perc < 65)
        {
            grade = "B-";
        }
        else if (perc >= 55 && perc < 60)
        {
            grade = "C+";
        }
        else if (perc >= 50 && perc < 55)
        {
            grade = "C";
        }
        else if (perc >= 45 && perc < 50)
        {
            grade = "C-";
        }
        else if (perc >= 40 && perc < 45)
        {
            grade = "D";
        }
        else if(perc < 40)
        {
            grade = "F";
        }
        else
        {
            grade = "";
        }

        // Display Scores
        dataText.text = "";
        dataText.text += "Collected Drops: " + dropsCollected + "\n";
        dataText.text += "Wasted Drops:    " + wastedDrops + "\n";
        dataText.text += "Total Drops:     " + totalDrops + "\n";
        gradeText.text = "";
        gradeText.text += "Grade: " + grade;
    }

    // Level Selection
    public void LoadLevel_1()
    {
        float dropSpeed = 2f;
        int levelTotal = 8;

        PlayerPrefs.SetFloat("DropSpeed", dropSpeed);
        PlayerPrefs.SetInt("ScoreIncrement", 1);
        PlayerPrefs.SetInt("LevelTotal", levelTotal);
        PlayerPrefs.SetInt("CurrentLevel", 1);

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
            GoToColorSelection();
        else if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
            ViewColorSelectionMenu();
    }

    public void LoadLevel_2()
    {
        float dropSpeed = 2.5f;
        int levelTotal = 10;

        PlayerPrefs.SetFloat("DropSpeed", dropSpeed);
        PlayerPrefs.SetInt("ScoreIncrement", 1);
        PlayerPrefs.SetInt("LevelTotal", levelTotal);
        PlayerPrefs.SetInt("CurrentLevel", 2);

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
            GoToColorSelection();
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
            ViewColorSelectionMenu();
    }

    public void LoadLevel_3()
    {
        float dropSpeed = 3f;
        int levelTotal = 16;

        PlayerPrefs.SetFloat("DropSpeed", dropSpeed);
        PlayerPrefs.SetInt("ScoreIncrement", 1);
        PlayerPrefs.SetInt("LevelTotal", levelTotal);
        PlayerPrefs.SetInt("CurrentLevel", 3);

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
            GoToColorSelection();
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
            ViewColorSelectionMenu();
    }

    public void LoadLevel_4()
    {
        float dropSpeed = 3.5f;
        int levelTotal = 16;

        PlayerPrefs.SetFloat("DropSpeed", dropSpeed);
        PlayerPrefs.SetInt("ScoreIncrement", 1);
        PlayerPrefs.SetInt("LevelTotal", levelTotal);
        PlayerPrefs.SetInt("CurrentLevel", 4);

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
            GoToColorSelection();
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
            ViewColorSelectionMenu();
    }

    public void LoadLevel_5()
    {
        float dropSpeed = 4f;
        int levelTotal = 10;

        PlayerPrefs.SetFloat("DropSpeed", dropSpeed);
        PlayerPrefs.SetInt("ScoreIncrement", 1);
        PlayerPrefs.SetInt("LevelTotal", levelTotal);
        PlayerPrefs.SetInt("CurrentLevel", 5);

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
            GoToColorSelection();
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
            ViewColorSelectionMenu();
    }

    public void LoadLevel_6()
    {
        float dropSpeed = 4.5f;
        int levelTotal = 10;

        PlayerPrefs.SetFloat("DropSpeed", dropSpeed);
        PlayerPrefs.SetInt("ScoreIncrement", 1);
        PlayerPrefs.SetInt("LevelTotal", levelTotal);
        PlayerPrefs.SetInt("CurrentLevel", 6);

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
            GoToColorSelection();
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
            ViewColorSelectionMenu();
    }

    public void LoadLevel_7()
    {
        float dropSpeed = 5f;
        int levelTotal = 8;

        PlayerPrefs.SetFloat("DropSpeed", dropSpeed);
        PlayerPrefs.SetInt("ScoreIncrement", 1);
        PlayerPrefs.SetInt("LevelTotal", levelTotal);
        PlayerPrefs.SetInt("CurrentLevel", 7);

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
            GoToColorSelection();
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
            ViewColorSelectionMenu();
    }

    public void LoadLevel_8()
    {
        float dropSpeed = 5.5f;
        int levelTotal = 8;

        PlayerPrefs.SetFloat("DropSpeed", dropSpeed);
        PlayerPrefs.SetInt("ScoreIncrement", 1);
        PlayerPrefs.SetInt("LevelTotal", levelTotal);
        PlayerPrefs.SetInt("CurrentLevel", 8);

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
            GoToColorSelection();
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
            ViewColorSelectionMenu();
    }

    public void LoadLevel_9()
    {
        float dropSpeed = 6f;
        int levelTotal = 5;

        PlayerPrefs.SetFloat("DropSpeed", dropSpeed);
        PlayerPrefs.SetInt("ScoreIncrement", 1);
        PlayerPrefs.SetInt("LevelTotal", levelTotal);
        PlayerPrefs.SetInt("CurrentLevel", 9);

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
            GoToColorSelection();
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
            ViewColorSelectionMenu();
    }

    public void LoadLevel_10()
    {
        float dropSpeed = 6.5f;
        int levelTotal = 5;

        PlayerPrefs.SetFloat("DropSpeed", dropSpeed);
        PlayerPrefs.SetInt("ScoreIncrement", 1);
        PlayerPrefs.SetInt("LevelTotal", levelTotal);
        PlayerPrefs.SetInt("CurrentLevel", 10);

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
            GoToColorSelection();
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
            ViewColorSelectionMenu();
    }

    // Sound Settings
    public void SoundToggleButton()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.buttonSound);

        AudioManager.Instance.soundEffectAudio.mute = !AudioManager.Instance.soundEffectAudio.mute;
        if (AudioManager.Instance.soundEffectAudio.mute)
        {
            PlayerPrefs.SetInt("Mute", 1);
            soundOffBtn.SetActive(false);
            soundOnBtn.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("Mute", 0);
            soundOffBtn.SetActive(true);
            soundOnBtn.SetActive(false);
        }
    }

    public void MusicToggleButton()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.buttonSound);

        playMusic = !playMusic;
        if (playMusic)
        {
            PlayerPrefs.SetInt("Music", 1);
            musicOffBtn.SetActive(true);
            musicOnBtn.SetActive(false);
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                FindObjectOfType<AudioManager>().soundEffectAudio.loop = true;
                FindObjectOfType<AudioManager>().soundEffectAudio.clip = FindObjectOfType<AudioManager>().menuJingle;
                FindObjectOfType<AudioManager>().soundEffectAudio.Play();
            }
            else if (SceneManager.GetActiveScene().name == "Game")
            {
                FindObjectOfType<AudioManager>().soundEffectAudio.loop = true;
                FindObjectOfType<AudioManager>().soundEffectAudio.clip = FindObjectOfType<AudioManager>().gameJingle;
                FindObjectOfType<AudioManager>().soundEffectAudio.Play();
            }
            else
            {
                FindObjectOfType<AudioManager>().soundEffectAudio.loop = false;
                FindObjectOfType<AudioManager>().soundEffectAudio.clip = null;
            }

        }
        else
        {
            PlayerPrefs.SetInt("Music", 0);
            musicOffBtn.SetActive(false);
            musicOnBtn.SetActive(true);
            FindObjectOfType<AudioManager>().soundEffectAudio.loop = false;
            FindObjectOfType<AudioManager>().soundEffectAudio.clip = null;
        }
    }

    public void CheckSoundSettings()
    {
        if (AudioManager.Instance.soundEffectAudio.mute)
        {
            PlayerPrefs.SetInt("Mute", 1);
            soundOffBtn.SetActive(false);
            soundOnBtn.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("Mute", 0);
            soundOffBtn.SetActive(true);
            soundOnBtn.SetActive(false);
        }
    }
}
