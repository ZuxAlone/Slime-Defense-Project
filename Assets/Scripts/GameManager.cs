using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake() 
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject gameoverObject;
    [SerializeField] private AudioClip gameoverSound;
    [SerializeField] private GameObject winObject;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private GameObject pauseObject;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;
    private bool isPaused = false;
    private bool isFinished = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused) 
        {
            Unpause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseObject.SetActive(true);
    }

    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseObject.SetActive(false);
    }

    public bool CanPlay() 
    {
        return !isPaused & !isFinished;
    }

    public void PlaySound(AudioClip clip) 
    {
        audioSource.PlayOneShot(clip);
    }

    public void UpdateScore(int points) 
    {
        score += points;
        scoreText.text = "Score: " + score.ToString();
        if (score == 200) 
        {
            Win();
        }
    }

    public void Win()
    {
        isFinished = true;
        Time.timeScale = 0;
        PlaySound(winSound);
        winObject.SetActive(true);
    }

    public void GameOver() 
    {
        isFinished = true;
        Time.timeScale = 0;
        PlaySound(gameoverSound);
        gameoverObject.SetActive(true);
    }
}
