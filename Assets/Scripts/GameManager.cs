using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject m_GameOverPanel;
    public GameObject m_InfoPanel;
    public Text m_ScoreText;
    public Text m_LivesText;
    public Text m_InfoText;

    public float timeLeft = 20.0f;
    private float m_TimeLeft;
    public Text gameTimeText;
    public bool isTimer = false;

    private bool paused;

    public int lives = 3;
    private int score;

    private AudioSource playerAudio;

    private SpawnManager spawnManager;

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
        m_LivesText.text = "Lives: " + lives;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        gameTimeText.gameObject.SetActive(false);
        m_TimeLeft = timeLeft;
        m_InfoPanel.gameObject.SetActive(false);
}

    // Update is called once per frame
    void Update()
    {
        if (isTimer)
        {
            StartTime();
        }
/*
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }*/

    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        m_ScoreText.text = ("Score: " + score);

        StartCoroutine(spawnManager.GenerateObjectsToFind());
    }
    public void UpadateLives(int livesTochange)
    {
        lives += livesTochange;
        m_LivesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            GameOver();
        }

    }
    public void GameOver()
    {
        m_GameOverPanel.SetActive(true);
        StopSound();
        ChangePaused();
    }
    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            Time.timeScale = 1;
        }
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
}
