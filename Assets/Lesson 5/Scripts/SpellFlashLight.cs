using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFlashLight : MonoBehaviour
{
    public float mouseSensitivity = 2f;
    public float upLimit = -50;
    public float downLimit = 50;
    public Transform FlashLightTarget;

    public GameObject FlashLightHolder;


    void Start()
    {
        DayCycleManager.instance.OnDayStateChenged += UpdateFlashLightVisability;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        transform.position = FlashLightTarget.position;
    }

    private void UpdateFlashLightVisability(bool isDay)
    {
        FlashLightHolder.SetActive(!isDay);
    }

    private void Rotate()
    {
        float verticalRotation = Input.GetAxis("Mouse Y");
        transform.Rotate(-verticalRotation * mouseSensitivity, 0, 0);

        Vector3 currentRotation = transform.localEulerAngles;
        if (currentRotation.x > 180) currentRotation.x -= 360;
        currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        //В классе SpellFlashLight
        //Реализовать урон по врагу
        //Урон должен наноситься только ночью
        //Урон не должен моментально убивать врагов
    }
}
