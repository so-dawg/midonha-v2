using System;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{

  public static KitchenGameManager Instance { get; private set; }
  private enum State { WaitingToStart, CountdownToStart, GamePlaying, GameOver }
  public event EventHandler OnStateChanged;

  private State state;
  private float WaitingToStartTimer = 1f;
  private float CountdownToStartTimer = 3f;
  private float GamePlayingTimer;
  private float GamePlayingTimerMax = 999f;

  private void Awake()
  {
    Instance = this;
    state = State.WaitingToStart;
  }

  private void Update()
  {
    switch (state)
    {
      case State.WaitingToStart:
        WaitingToStartTimer -= Time.deltaTime;
        if (WaitingToStartTimer < 0f)
        {
          state = State.CountdownToStart;
        }
        OnStateChanged?.Invoke(this, EventArgs.Empty);
        break;

      case State.CountdownToStart:
        CountdownToStartTimer -= Time.deltaTime;
        if (CountdownToStartTimer < 0f)
        {
          state = State.GamePlaying;
          GamePlayingTimer = GamePlayingTimerMax;
        }
        OnStateChanged?.Invoke(this, EventArgs.Empty);
        break;

      case State.GamePlaying:
        GamePlayingTimer -= Time.deltaTime;
        if (GamePlayingTimer < 0f)
        {
          state = State.GameOver;
        }
        OnStateChanged?.Invoke(this, EventArgs.Empty);
        break;

      case State.GameOver:
        break;
    }

  }

  public bool IsGamePlaying()
  {
    return state == State.GamePlaying;
  }

  public bool IsCountdownToStartActive()
  {
    return state == State.CountdownToStart;
  }

  public float GetCountdownToStartTimer()
  {
    return CountdownToStartTimer;
  }

  public float GetPlayingTimerMax()
  {
    return GamePlayingTimerMax;
  }

  public float GetPlayingTimer()
  {
    return GamePlayingTimer;
  }
  public bool IsGameOver()
  {
    return state == State.GameOver;
  }


  public float GetGamePlayingTimer()
  {
    return GamePlayingTimer / GamePlayingTimerMax;
  }
}
