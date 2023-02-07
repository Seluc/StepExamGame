using System;
using Unity.Mathematics;
using UnityEngine;

public class DayCycleManager : MonoBehaviour
{
    public static DayCycleManager instance;
    public event Action<bool> OnDayStateChenged;


    public Transform DirectionalLightTransform;
    public float CycleSpeed = 2f;

    public Material DaySkyBox;
    public Material NightSkyBox;

    public bool IsDay 
    {
        get => _isDay;
        private set {_isDay = value;}
    }

    [Space]
    [Header("Debug tools")]
    public bool UseStaticTime;
    public TimeOfDay StaticTimeOfDay = TimeOfDay.Day;

    public enum TimeOfDay
    {
        Day,
        Night
    }

    [SerializeField]
    private float _currentTime = 6;
    private bool _isDay = true;
    private bool _isSkyboxNeedToChange = true;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        if (UseStaticTime)
        {
            switch (StaticTimeOfDay)
            {
                case TimeOfDay.Day:
                    _isDay = true;
                    DirectionalLightTransform.rotation = Quaternion.Euler(90f, 0f, 0f);
                    ChangeSkyBox(_isDay);
                    break;
                case TimeOfDay.Night:
                    _isDay = false;
                    ChangeSkyBox(_isDay);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(UseStaticTime == false)
        {
            DayCycle();
        }         
    }

    private void DayCycle()
    {
        _currentTime += Time.deltaTime * CycleSpeed;
        if (_currentTime >= 24)
        {
            _currentTime = 0;
        }

        if (_currentTime >= 6 && _currentTime <= 18) //DAY
        {
            var angle = math.remap(6, 18, 0, 180, _currentTime);
            DirectionalLightTransform.rotation = Quaternion.Euler(angle, 0f, 0f);
            _isDay = true;
            if (_isSkyboxNeedToChange)
            {
                ChangeSkyBox(_isDay);
            }

            //Skybox
            var skyboxAngle = math.remap(6, 18, 0, 360, _currentTime);
            DaySkyBox.SetFloat("_Rotation", skyboxAngle);
        }
        else //NIGHT
        {
            _isDay = false;

            if (_isSkyboxNeedToChange == false)
            {
                ChangeSkyBox(_isDay);
            }
            var skyboxAngle = math.remap(18, 6, 0, 360, _currentTime);
            NightSkyBox.SetFloat("_Rotation", skyboxAngle);
        }
    }

    private void ChangeSkyBox(bool isDay)
    {
        if (isDay)
        {
            //Day sky box
            DirectionalLightTransform.gameObject.SetActive(true);
            RenderSettings.skybox = DaySkyBox;
            RenderSettings.ambientIntensity = 1f;
        }
        else
        {
            //Night skybox
            DirectionalLightTransform.gameObject.SetActive(false);
            RenderSettings.skybox = NightSkyBox;
            RenderSettings.ambientIntensity = 0f;
        }
        _isSkyboxNeedToChange = !_isSkyboxNeedToChange;
        OnDayStateChenged?.Invoke(isDay);
        Debug.Log("ChangeSkyBox");
    }
}
