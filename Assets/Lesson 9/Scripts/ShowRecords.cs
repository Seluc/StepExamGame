using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowRecords : MonoBehaviour
{
    public TextMeshProUGUI RecordLable;
    void Start()
    {
        if (PlayerPrefs.HasKey("Records"))
        {
            RecordLable.text = PlayerPrefs.GetInt("Records") + "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
