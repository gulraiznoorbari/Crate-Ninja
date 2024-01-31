using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector] public bool isGameActive;

    [SerializeField] private GameObject[] _targets;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _titleScreenPanel;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _livesText;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private Button _restartbutton;
    [SerializeField] private Slider _volumeSlider;

    private bool paused;
    private int lives;
    private int score;
    private float timeLeft = 60.0f;
    private float spawnRate = 1.0f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        // Detect button press for Game Pause:
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
        // Timer: 
        if (timeLeft > 0 && isGameActive)
        {
            timeLeft -= Time.deltaTime;
            _timeText.SetText(Mathf.Round(timeLeft).ToString());
        }
        else if (timeLeft <= 0)
        {
            GameOver();
        }
    }

    private IEnumerator SpawnTargets()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, _targets.Length);
            Instantiate(_targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        _scoreText.SetText(score.ToString());
    }

    public void UpdateLives(int livesToAdd)
    {
        lives += livesToAdd;
        _livesText.SetText(lives.ToString());
        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnTargets());
        UpdateScore(0);
        UpdateLives(5);
        _titleScreenPanel.gameObject.SetActive(false);
    }

    public void PauseGame()
    {
        if (!paused)
        {
            paused = true;
            _pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        isGameActive = false;
        _gameOverText.gameObject.SetActive(true);
        _restartbutton.gameObject.SetActive(true);
    }
}
