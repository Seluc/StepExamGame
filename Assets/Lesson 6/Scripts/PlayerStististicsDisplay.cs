using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStististicsDisplay : MonoBehaviour
{
    public TextMeshProUGUI HealthValue;
    void Start()
    {
        UpdateHealth(PlayerStatistics.instance.GetHealth());
        PlayerStatistics.instance.PlayerHealthComponent.GetDamageEvent += UpdateHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(float hp)
    {
        HealthValue.text = hp + "";
    }
}
