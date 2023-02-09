using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCristal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStatistics.instance.AddScore(1);
            Destroy(gameObject);
        }
    }
}
