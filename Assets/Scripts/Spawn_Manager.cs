using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject enemyContainer;
    [SerializeField]
    private GameObject[] powerUps;

    private bool stopSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.4f, 9.4f), 7.0f, 0);
            GameObject newEnemy = Instantiate(enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.4f, 9.4f), 7.0f, 0);
            int randomPowerUp = Random.Range(0,3);
            GameObject powerUp = Instantiate(powerUps[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(7.0f);
        }
    }

    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }
}
