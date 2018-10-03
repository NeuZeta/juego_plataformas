using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level3Controller : MonoBehaviour {
    private PlayerController player;

    public Text recordText;
    public Button startButton, pauseButton;
    private int secondsToStart = 3;
    private Text mainText;
    private float tiempoInicial;

    private float gameTime, timeRecord;

    [SerializeField]
    private GameObject pausePanel, victoryPanel;

    [SerializeField]
    private Text scoreText, highscoreText;

    private void Start () {

        pausePanel.SetActive(false);
        victoryPanel.SetActive(false);

        player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.eliminado += RestartGame;
        player.endLevel += EndLevel;
        player.enabled = false;
        mainText = startButton.GetComponentInChildren<Text>();

        timeRecord = GameController.instance.GetHighscoreLevel3();

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
        SceneFader.instance.FadeIn(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneFader.instance.FadeIn("MainMenu");
    }

    private string NextLevelName()
    {
        string currentLevelName = SceneManager.GetActiveScene().name;
        if (currentLevelName == "Gameplay")
        {
            string nextLevelName = "Level2";
            return nextLevelName;
        } else if (currentLevelName == "Level2")
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
        player.enabled = false;

        StartCoroutine(WaitToShowVictoryPanel());

        gameTime = (Time.time - tiempoInicial);

        if (timeRecord == 0) {
            GameController.instance.SetHighscoreLevel3(gameTime);
        } else if (gameTime < timeRecord)
        {
            GameController.instance.SetHighscoreLevel3(gameTime);
        }

        scoreText.text = gameTime.ToString();
        highscoreText.text = GameController.instance.GetHighscoreLevel3().ToString("##.##");
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
        startButton.enabled = false;
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
    }

    private void Update()
    {
        if (player.enabled)
        {
            mainText.text = "Time: " + (Time.time - tiempoInicial).ToString("##.##");
        }
    }

    private IEnumerator WaitForReload()
    {
        yield return new WaitForSeconds(1.5f);
        SceneFader.instance.FadeIn(SceneManager.GetActiveScene().name);

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator WaitToShowVictoryPanel()
    {
        yield return new WaitForSeconds(1f);
        victoryPanel.SetActive(true);
    }




}//GameplayController
