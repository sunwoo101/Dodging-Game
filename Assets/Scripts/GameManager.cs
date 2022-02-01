using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variables

    // References
    [SerializeField] private Text m_ScoreText;

    [SerializeField] private Vector2 m_MinPos;
    [SerializeField] private Vector2 m_MaxPos;
    [SerializeField] private GameObject m_Coin;

    private int m_Score;
    private int m_HighScore;
    private GameObject m_NewCoin;

    public int Score
    {
        get { return m_Score; }
        set { m_Score = value; }
    }

    #endregion

    #region Awake

    private void Awake()
    {
        m_HighScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    #endregion

    #region Start

    private void Start()
    {
        SpawnCoin();
    }

    #endregion

    #region Update

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }

        // Set score text
        m_ScoreText.text =  "Score: " + m_Score + "\n" +
                            "High Score : " + m_HighScore;
        
        if (m_NewCoin == null)
        {
            SpawnCoin();
        }
    }

    #endregion

    #region GameOver

    public void GameOver()
    {
        if (m_Score > m_HighScore)
        {
            m_HighScore = m_Score;
            PlayerPrefs.SetInt("HighScore", m_HighScore);
        }
    }

    #endregion

    #region SpanwCoin

    public void SpawnCoin()
    {
        // Generate random position
        float xPos = Random.Range(m_MinPos.x, m_MaxPos.x);
        float yPos = Random.Range(m_MinPos.y, m_MaxPos.y);

        // Spawn new coin
        m_NewCoin = Instantiate(m_Coin, new Vector3(xPos, yPos, 0), Quaternion.identity);
    }

    #endregion
}
