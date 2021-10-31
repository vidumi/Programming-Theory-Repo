using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject m_GameOverPanel;
    public GameObject m_InfoPanel;
    public Text m_ScoreText;
    public Text m_InfoText;

    public float timeLeft = 20.0f;
    private float m_TimeLeft;
    public Text gameTimeText;
    public Text alertText;
    public bool isTimer = false;

    

    private bool paused;

    private int score;

    private AudioSource playerAudio;

    private SpawnManager spawnManager;

    public Text m_LivesText;

    public GameObject setupPanel;
    public GameObject inputField;



    private int m_Lives = 3;
    public int lives
    {
        get { return m_Lives; } // getter returns backing fieldset 
        set
        {
            if ( value < 1 || value > 8)
            {              
                // Debug.LogError("The value must be between 1 and 8!");
                alertText.gameObject.SetActive(true);
            }
            else
            {
                m_Lives = value; // original setter now in if/else statement
                m_LivesText.text = "Lives: " + m_Lives;
            }
        } // setter uses backi
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

        playerAudio = GetComponent<AudioSource>();
        m_LivesText.text = "Lives: " + m_Lives;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        gameTimeText.gameObject.SetActive(false);
        alertText.gameObject.SetActive(false);
        m_TimeLeft = timeLeft;
        m_InfoPanel.gameObject.SetActive(false);
        setupPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimer)
        {
            StartTime();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }

    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        m_ScoreText.text = ("Score: " + score);

        StartCoroutine(spawnManager.GenerateObjectsToFind());
    }
    public void UpadateLives(int livesTochange)
    {
        m_Lives += livesTochange;
        m_LivesText.text = "Lives: " + m_Lives;
        if (m_Lives <= 0)
        {
            GameOver();
        }

    }
    public void GameOver()
    {
        m_GameOverPanel.SetActive(true);
        StopSound();
        PauseOn();
    }
    void ChangePaused()
    {
        if (!paused)
        {
            PauseOn();
        }
        else
        {
            PauseOff();
        }
    }

    private void PauseOn()
    {
        paused = true;
        Time.timeScale = 0;
    }
    private void PauseOff()
    {
        paused = false;
        Time.timeScale = 1;
    }



    public void IsTimer()
     {
        isTimer = true;
     }
    public void StartTime()
    {
        gameTimeText.gameObject.SetActive(true);
        m_TimeLeft -= Time.deltaTime;
        gameTimeText.text = "Remaining time: " + Mathf.Round(m_TimeLeft);
        if (m_TimeLeft < 0)
        {
            ResetTimer();
            GameOver();
        }
    }

    public void ResetTimer()
    {
        isTimer = false;
        m_TimeLeft = timeLeft;

        gameTimeText.gameObject.SetActive(false);
    }

    public void Info(string text)
    {
        m_InfoPanel.SetActive(true);
        m_InfoText.text= text;
    }

    public void SoundClip(AudioClip audioClip)
    {
       playerAudio.PlayOneShot(audioClip); 
    }

    public void StopSound()
    {
        playerAudio.Stop();
    }


    public void SetTimeLeft()
    {
        lives = System.Int32.Parse(inputField.GetComponent<Text>().text);
    }

    private bool setup;
    public void OpenSetupPanel()
    {
        if (!setup)
        {
            setupPanel.gameObject.SetActive(true);
            ChangePaused();
            setup = true;
        }
        else
        {
            setupPanel.gameObject.SetActive(false);
            ChangePaused();
            setup = false;

        }
    }
}
