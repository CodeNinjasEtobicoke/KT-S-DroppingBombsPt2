using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class GameManager : MonoBehaviour
{
    private Spawner spawner;
    public GameObject title;
    private Vector2 screenBounds;

    [Header("Player")]
    public GameObject playerPrefab;
    private GameObject player;
    private bool gameStarted = false;
    public GameObject splash;

    [Header("Score")]
    public TMP_Text scoreText;
    public int pointsWorth = 1;
    public int score;

    private bool smokeCleared = true;


    private int bestScore = 0;
    public TMP_Text bestScoreText;
    private bool beatBestScore;


    private void Awake()
    {
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        scoreText.enabled = false;

        bestScoreText.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        title.SetActive(true);
        splash.SetActive(false);

        bestScore = PlayerPrefs.GetInt("BestScore");
        bestScoreText.text = "Best Score:" + bestScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted)
        {
            if (Input.anyKeyDown && smokeCleared)
            {
                smokeCleared = false;
                ResetGame();
            }
            if (Input.anyKeyDown)
            {
                ResetGame();
            }
        }
        else
        {
            if (!player)
            {
                OnPlayerKilled();
            }
        }



        var nextBomb = GameObject.FindGameObjectsWithTag("Bomb");

        foreach (GameObject bombObject in nextBomb)
        {
            if (bombObject.transform.position.y < (-screenBounds.y - 12))
            {
                if (gameStarted)
                {
                    score += pointsWorth;
                    scoreText.text = "Score:" + score.ToString();

                }
                Destroy(bombObject);
            }
        }
    }
    void OnPlayerKilled()
    {
        spawner.active = false;
        gameStarted = false;
        Invoke("SplashScreen", 2);

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            beatBestScore = true;
            bestScoreText.text = "Best Score:" + bestScore.ToString();
        }


    }
    void SplashScreen()
    {
        smokeCleared = true;
        splash.SetActive(true);
    }

    void ResetGame()
    {
        spawner.active = true;
        title.SetActive(false);
        player = Instantiate(playerPrefab, new Vector3(0, 0, 0), playerPrefab.transform.rotation);
        gameStarted = true;
        splash.SetActive(false);

        scoreText.enabled = true;
        score = 0;

        beatBestScore = false;
        bestScoreText.enabled = true;

        scoreText.text = "Score: " + score.ToString();
    }
}