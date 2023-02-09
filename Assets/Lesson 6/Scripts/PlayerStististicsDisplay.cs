using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStististicsDisplay : MonoBehaviour {
  public TextMeshProUGUI HealthValue;
  public TextMeshProUGUI ScoreValue;

  void Start() {
    UpdateHealth(PlayerStatistics.instance.GetHealth());
    PlayerStatistics.instance.PlayerHealthComponent.GetDamageEvent += UpdateHealth;
    PlayerStatistics.instance.GetScoreEvent += UpdateScore;
  }

  // Update is called once per frame
  void Update() {

  }

  public void UpdateHealth(float hp) {
    HealthValue.text = hp + "";
  }

  public void UpdateScore(float score) {
    ScoreValue.text = score + "";
  }
}
