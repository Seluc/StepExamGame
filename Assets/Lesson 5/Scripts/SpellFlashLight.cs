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

  public event Action<Health> CollisionWithEnemy;
  public float Damage = 10f;
  private bool isActive = false;


  void Start() {
    DayCycleManager.instance.OnDayStateChenged += UpdateFlashLightVisability;
  }

  // Update is called once per frame
  void Update() {
    Rotate();
    transform.position = FlashLightTarget.position;
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

  private void OnTriggerEnter(Collider other) {
    if (isActive) {
      if (other.gameObject.CompareTag("Enemy")) {
        var hp = other.gameObject.GetComponent<Health>();
        hp.SetDamage(Damage);
      }
    }

    //В классе SpellFlashLight
    //Реализовать урон по врагу
    //Урон должен наноситься только ночью
    //Урон не должен моментально убивать врагов
  }
}
