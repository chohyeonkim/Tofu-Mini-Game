using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region Singleton

    public static GameManager Instance;

    private void Awake() {
        if (Instance == null) Instance = this;
    }

    #endregion

    public float currentScore = 0f;
    
    public bool isPlaying = false;

    public UnityEvent onPlay = new UnityEvent();

    public UnityEvent onGameOver = new UnityEvent();

    private void Update() {
        if (isPlaying) {
            currentScore += Time.deltaTime;
        }
    }

    public void StartGame() {
        onPlay.Invoke();
        isPlaying = true;
    }

    public void GameOver() {
        onGameOver.Invoke();
        currentScore = 0;
        isPlaying = false;
    }
    public string PrettyScore () {
        return Mathf.RoundToInt(currentScore).ToString();
    }

    public void returnToVillage()
    {
        SceneManager.LoadScene("Village");
    }
}
