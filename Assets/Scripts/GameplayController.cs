using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour {

    PlayerController player;

    public Text recordText;
    public Button startButton;
    private int secondsToStart = 3;
    private Text mainText;
    private float tiempoInicial;

    private float gameTime, timeRecord;


	void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.eliminado += RestartGame;
        player.endLevel += EndLevel;
        player.enabled = false;
        mainText = startButton.GetComponentInChildren<Text>();

        timeRecord = PlayerPrefs.GetFloat("time record");

        if (timeRecord > 0)
        {
            recordText.text = "Record: " + timeRecord.ToString("##.##");
        } else
        {
            recordText.enabled = false;
        }
    }
	
	
	void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    void EndLevel()
    {
        player.enabled = false;

        gameTime = (Time.time - tiempoInicial);

        mainText.text = "Final! " + gameTime;

        if (gameTime < timeRecord)
        {
            PlayerPrefs.SetFloat("time record", gameTime);
        }

        //Time.timeScale = 0;
    }

    public void StartGame()
    {
        startButton.enabled = false;
        mainText.text = secondsToStart.ToString();
        InvokeRepeating("CountDown", 1, 1);
    }

    void CountDown()
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

    void GameStarted()
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

}//GameplayController
