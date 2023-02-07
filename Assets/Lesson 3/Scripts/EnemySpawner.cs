using lesson2;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform Target;
    public GameObject EnemyPrefab;
    public float SpawnDelay = 5f;

    private float _timer;

    void Start()
    {
        
    }

    
    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= SpawnDelay)
        {
            GameObject enemyGO = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
            var enemyAi = enemyGO.GetComponent<EnemyAI>();
            enemyAi.Target = Target;
            _timer = 0;
        }
    }
}
