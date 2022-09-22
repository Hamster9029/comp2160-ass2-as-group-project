using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //Singleton
    static private UIManager instance;
    static public UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("There is no UI Manager in the scene.");
            }
            return instance;
        }
    }

    public Text gameOverText;
    public string winText = "YOU WIN!!!";
    public string loseText = "YOU LOSE!!!";
    public Text timeSinceStart;
    public float minutes, seconds, milliseconds;
    public float minAndSecFloatCounter = 60f;
    public int minIntCounter = 60;
    public float millisecondFloatCounter = 1000f;
    public int millisecondIntCounter = 1000;
    public string checkpointTime;
    public GameObject gameOverPanel;
    public Text checkpointA;
    public Text checkpointB;
    public Text checkpointC;
    public string checkpointOne;
    public string checkpointTwo;
    public string checkpointThree;

    public RectTransform healthBar;
    public GameManager gameManager;


    void Awake()
    {
        if (instance != null)
        {
            //there is already a UIManager in the scene, destroy this
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
        checkpointOne = "Checkpoint 1: Incomplete";
        checkpointTwo = "Checkpoint 2: Incomplete";
        checkpointThree = "Checkpoint 3: Incomplete";
    }

    // Update is called once per frame
    void Update()
    {
        minutes = (int)(Time.time / minAndSecFloatCounter) % minIntCounter;
        seconds = (int)(Time.time % minAndSecFloatCounter);
        milliseconds = (int)(Time.time * millisecondFloatCounter) % millisecondIntCounter;
        //To display the time in minutes:seconds:milliseconds
        timeSinceStart.text = string.Format(minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00"));
        healthBar.sizeDelta = new Vector2(gameManager.getHealth, healthBar.sizeDelta.y);
    }
    //Takes the name of the checkpoint passed and records the time when it happens
    public void CheckpointTime(string checkpointName, float time)
    {
        minutes = (int)(time / minAndSecFloatCounter) % minIntCounter;
        seconds = (int)(time % minAndSecFloatCounter);
        milliseconds = (int)(time * millisecondFloatCounter) % millisecondIntCounter;
        checkpointTime = string.Format(minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00"));
        if (checkpointName == "Checkpoint1")
        {
            checkpointOne = "Checkpoint 1:" + checkpointTime;
            Debug.Log(checkpointOne);
        }
        if (checkpointName == "Checkpoint2")
        {
            checkpointTwo = "Checkpoint 2:" + checkpointTime;
            Debug.Log(checkpointTwo);
        }
        if (checkpointName == "Checkpoint3")
        {
            checkpointThree = "Checkpoint 3:" + checkpointTime;
            Debug.Log(checkpointThree);
        }
    }

    //Show Game Over Panel with Win/Lose Text
    public void ShowGameOver(bool win)
    {
        if (win)
        {
            gameOverText.text = winText;
            checkpointA.text = checkpointOne;
            checkpointB.text = checkpointTwo;
            checkpointC.text = checkpointThree;
        }
        else
        {
            gameOverText.text = loseText;
            checkpointA.text = checkpointOne;
            checkpointB.text = checkpointTwo;
            checkpointC.text = checkpointThree;
        }
        gameOverPanel.SetActive(true);
    }
    //Restart the Game function
    public void Restart()
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }
}
