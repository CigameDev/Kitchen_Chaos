using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;

    public enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }    

    private State state;
    //private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer ;
    private float gamePlayingTimerMax = 60f;
    private bool isPauseGame = false;
    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += Instance_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        /*
         * bat dau vao game o trang thai waitingToStart
         * Nhan E (Interact chuyen sang trang thai CountdownToStart) thuc hien delegate OnStateChanged
         */
        if(state == State.WaitingToStart)
        {
            state = State.CountdownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }    
    }

    private void Instance_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                //waitingToStartTimer -= Time.deltaTime;
                //if(waitingToStartTimer <= 0f)
                //{
                //    state = State.CountdownToStart;
                //    OnStateChanged?.Invoke(this, EventArgs.Empty);
                //}    
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if(countdownToStartTimer <= 0f)
                {
                    state=State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }    
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if(gamePlayingTimer <= 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }    
                break;
            case State.GameOver:
                break;
        }

    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }    

    public bool IsCoundownToStartActive()
    {
        return state == State.CountdownToStart;
    }    

    public float GetCountDownToStartTimer()
    {
        return countdownToStartTimer;
    }    

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }    

    public float GetGamePlayingTimerNormalized()
    {
        return 1 - gamePlayingTimer / gamePlayingTimerMax;
    }    

    public void TogglePauseGame()
    {
        isPauseGame = !isPauseGame;
        if (isPauseGame)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);
        }    
    }    
}
