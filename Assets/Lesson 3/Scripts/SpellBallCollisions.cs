using System;
using UnityEngine;

public class SpellBallCollisions : MonoBehaviour
{
    public event Action<Health> CollisionWithEnemy;

    void Start()
    {
     
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision with " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var hp = collision.gameObject.GetComponent<Health>();
            CollisionWithEnemy?.Invoke(hp);
        }
    }
}
