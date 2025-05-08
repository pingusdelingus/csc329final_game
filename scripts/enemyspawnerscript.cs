using UnityEngine;
//using System.Serializable;
public class enemyspawner : MonoBehaviour
{

  [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private float minSpawnTime = 1.0f;
    [SerializeField] private float maxSpawnTime = 5.0f;

    private float timeUntilNextSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
       setTimeUntilSpawn(); 
    }

    // Update is called once per frame
    void Update()
    {
       timeUntilNextSpawn -= Time.deltaTime;
       if (timeUntilNextSpawn <= 0){
        Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        setTimeUntilSpawn();
       }
    }

    private void setTimeUntilSpawn()
    {
        timeUntilNextSpawn = (float) Random.Range(minSpawnTime, maxSpawnTime);


    }// end of method
}
