using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    public static PlayerStatistics instance;
    public Health PlayerHealthComponent;
    public int Score;


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

    public float GetHealth()
    {
        return PlayerHealthComponent.CurrentHealth;
    }

    public void SavePlayerData()
    {
        PlayerPrefs.SetInt("Records", Score);
    }

}
