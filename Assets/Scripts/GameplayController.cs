using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour {
    private PlayerController player;
    private GameObject[] birds;
    private BirdScript birdScript;

    public Text recordText;
    public Button startBtn, countdownBtn, pauseBtn, resumeBtn, quitBtn, backBtn, replayBtn, nextBtn;
    private int secondsToStart = 3;
    private Text mainText;
    private float tiempoInicial;

    private float gameTime, timeRecord;

    [SerializeField]
    private GameObject pausePanel, victoryPanel;

    [SerializeField]
    private Text scoreText, highscoreText;

    private string currentLevel;

    private void Awake()
    {
        startBtn.onClick.AddListener(()=> PlayClickSound());
        pauseBtn.onClick.AddListener(() => PlayClickSound());
        resumeBtn.onClick.AddListener(() => PlayClickSound());
        quitBtn.onClick.AddListener(() => PlayClickSound());
        backBtn.onClick.AddListener(() => PlayClickSound());
        replayBtn.onClick.AddListener(() => PlayClickSound());
        nextBtn.onClick.AddListener(() => PlayClickSound());
    }

    private void Start () {

        pausePanel.SetActive(false);
        victoryPanel.SetActive(false);
        countdownBtn.gameObject.SetActive(false);
        startBtn.gameObject.SetActive(true);

        currentLevel = SceneManager.GetActiveScene().name;

        player = GameObject.Find("Player").GetComponent<PlayerController>();
        if (birds == null)
        {
            birds = GameObject.FindGameObjectsWithTag("Bird");
        }

        player.eliminado += RestartGame;
        player.endLevel += EndLevel;
        player.enabled = false;
        foreach (GameObject bird in birds)
        {
            birdScript = bird.GetComponent<BirdScript>();
            birdScript.enabled = false;
        }
        mainText = countdownBtn.GetComponentInChildren<Text>();

        if (currentLevel == "Level1")
        {
            timeRecord = GameController.instance.GetHighscoreLevel1();
        } else if (currentLevel == "Level2")
        {
            timeRecord = GameController.instance.GetHighscoreLevel2();
        } else
        {
            timeRecord = GameController.instance.GetHighscoreLevel3();
        }

        if (timeRecord > 0)
        {
            recordText.text = "Record: " + timeRecord.ToString("##.##");
        } else
        {
            recordText.enabled = false;
        }
    }
	
	
	public void RestartGame()
    {
        SceneFader.instance.FadeIn(currentLevel);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneFader.instance.FadeIn("MainMenu");
    }

    private string NextLevelName()
    {
        if (currentLevel == "Level1")
        {
            string nextLevelName = "Level2";
            return nextLevelName;
        } else if (currentLevel == "Level2")
        {
            string nextLevelName = "Level3";
            return nextLevelName;
        } else
        {
            string mainMenuName = "MainMenu";
            return mainMenuName;
        }
    }

    public void GoToNextLevel()
    {
        SceneFader.instance.FadeIn(NextLevelName());
    }

    private void EndLevel()
    {
        AudioController.instance.audioSource.PlayOneShot(AudioController.instance.cheer);
        player.enabled = false;

        if (currentLevel == "Level1")
        {
            if (timeRecord == 0)
            {
                GameController.instance.SetHighscoreLevel1(gameTime);
            }
            else if (gameTime < timeRecord)
            {
                GameController.instance.SetHighscoreLevel1(gameTime);
            }

            highscoreText.text = GameController.instance.GetHighscoreLevel1().ToString("##.##");
            GameController.instance.UnlockedLevel2();

        } else if (currentLevel == "Level2")
        {
            if (timeRecord == 0)
            {
                GameController.instance.SetHighscoreLevel2(gameTime);
            }
            else if (gameTime < timeRecord)
            {
                GameController.instance.SetHighscoreLevel2(gameTime);
            }

            highscoreText.text = GameController.instance.GetHighscoreLevel2().ToString("##.##");
            GameController.instance.UnlockedLevel3();

        } else
        {
            if (timeRecord == 0)
            {
                GameController.instance.SetHighscoreLevel3(gameTime);
            }
            else if (gameTime < timeRecord)
            {
                GameController.instance.SetHighscoreLevel3(gameTime);
            }

            highscoreText.text = GameController.instance.GetHighscoreLevel3().ToString("##.##");
        }
            
        scoreText.text = gameTime.ToString("##.##");
        StartCoroutine(WaitToShowVictoryPanel());  
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        startBtn.gameObject.SetActive(false);
        countdownBtn.gameObject.SetActive(true);
        mainText.text = secondsToStart.ToString();
        InvokeRepeating("CountDown", 1, 1);
    }

    private void CountDown()
    {
        secondsToStart--;
        if (secondsToStart <= 0)
        {
            CancelInvoke();
            GameStarted();

        } else
        {
            mainText.text = secondsToStart.ToString();
        }
    }

    private void GameStarted()
    {
        player.enabled = true;
        tiempoInicial = Time.time;
        mainText.alignment = TextAnchor.MiddleLeft;

        foreach (GameObject bird in birds)
        {
            birdScript = bird.GetComponent<BirdScript>();
            birdScript.enabled = true;
        }
    }

    private void Update()
    {
        if (player.enabled)
        {
            gameTime = (Time.time - tiempoInicial);
            mainText.text = "Time: " + (gameTime).ToString("##.##");
        }
    }

    private IEnumerator WaitForReload()
    {
        yield return new WaitForSeconds(1.5f);
        SceneFader.instance.FadeIn(currentLevel);
    }

    private IEnumerator WaitToShowVictoryPanel()
    {
        yield return new WaitForSeconds(1f);
        victoryPanel.SetActive(true);
    }

    void PlayClickSound()
    {
        AudioController.instance.audioSource.PlayOneShot(AudioController.instance.click);
    }

    void PlayCrashSound()
    {
        AudioController.instance.audioSource.PlayOneShot(AudioController.instance.carCrash);
    }


}//GameplayController
