using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour {
  public static PlayerStatistics instance;
  public Health PlayerHealthComponent;
  public int Score;
  public event Action<float> GetScoreEvent;


  private void Awake() {
    if (instance == null) {
      instance = this;
    } else {
      Destroy(this);
    }
  }

  public float GetHealth() {
    return PlayerHealthComponent.CurrentHealth;
  }

  public void AddScore(int count) {
    Score += count;
    GetScoreEvent?.Invoke(Score);
  }

  public void SavePlayerData() {
    PlayerPrefs.SetInt("Records", Score);
  }

}
