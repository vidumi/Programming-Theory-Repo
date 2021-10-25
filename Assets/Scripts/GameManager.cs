using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject m_GameOverPanel;
    public Text m_ScoreText;
    public Text m_LivesText;

    private bool paused;

    public int lives = 3;
    private int score;

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
        // UpadateLives(3);
        m_LivesText.text = "Lives: " + lives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        m_ScoreText.text = ("Score: " + score);
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
}
