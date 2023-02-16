using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFlashLight : MonoBehaviour {
  public float mouseSensitivity = 2f;
  public float upLimit = -50;
  public float downLimit = 50;
  public Transform FlashLightTarget;

  public GameObject FlashLightHolder;

  //public event Action<Health> CollisionWithEnemy;
  public float Damage = 10f;
  private bool isActive = false;
  private List<Collider> enemies = new List<Collider>();
  private bool canStartDamage = false;


  void Start() {
    if (DayCycleManager.instance != null) {
      DayCycleManager.instance.OnDayStateChenged += UpdateFlashLightVisability;
    } else {
      isActive = true;
    }

    StartCoroutine(DamageFromLight());
  }

  void Update() {
    Rotate();
    transform.position = FlashLightTarget.position;

    if(canStartDamage == true) {
      canStartDamage = false;
      StartCoroutine(DamageFromLight());
    }
  }

  private void UpdateFlashLightVisability(bool isDay) {
    FlashLightHolder.SetActive(!isDay);
    isActive = !isDay;
  }

  private void Rotate() {
    float verticalRotation = Input.GetAxis("Mouse Y");
    transform.Rotate(-verticalRotation * mouseSensitivity, 0, 0);

    Vector3 currentRotation = transform.localEulerAngles;
    if (currentRotation.x > 180) currentRotation.x -= 360;
    currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
    transform.localRotation = Quaternion.Euler(currentRotation);
  }

  private IEnumerator DamageFromLight() {
    for (var i = 0; i < enemies.Count; ++i) {
      if (enemies[i] == null) {
        enemies.RemoveAt(i);
        --i;
        continue;
      }

      enemies[i].gameObject.GetComponent<Health>().SetDamage(Damage);
    }

    yield return new WaitForSeconds(1f);

    canStartDamage = true;
  }

  private void OnTriggerEnter(Collider other) {
    if (isActive) {
      if (other.gameObject.CompareTag("Enemy")) {
        enemies.Add(other);
      }
    }
  }

  private void OnTriggerExit(Collider other) {
    if(enemies.Contains(other) == true) {
      enemies.Remove(other);
    }
  }
}
