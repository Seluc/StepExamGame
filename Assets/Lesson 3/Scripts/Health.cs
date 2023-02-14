using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {
  public GameObject HitEffect;
  public TextMeshPro HealthLable;
  public event Action<float> GetDamageEvent;

  public float CurrentHealth {
    private set { health = value; }
    get { return health; }
  }
  [SerializeField]
  private float health = 100f;
  private float maxHealth = 200f;

  private bool isAlive = true;
  public bool IsAlive {
    get => isAlive;
    private set { isAlive = value; }
  }

  private void Start() {
    if (HealthLable) {
      HealthLable.text = health + " HP";
    }
  }

  void Update() {}

  public void SetDamage(float damage) {
    health = Mathf.Clamp(health - damage, 0, maxHealth);

    if (HitEffect) {
      Instantiate(HitEffect, transform.position, Quaternion.identity);
    }

    if (health == 0) {
      IsAlive = false;
      Death();
    }
    if (HealthLable) {
      HealthLable.text = health + " HP";
    }
    GetDamageEvent?.Invoke(health);
  }

  private void Death() {
    if (gameObject.CompareTag("Enemy")) {
      Destroy(gameObject, 1f);
    } else if (gameObject.CompareTag("Player")) {
      PlayerStatistics.instance?.SavePlayerData();
      SceneManager.LoadScene(0);
    }
  }
}
