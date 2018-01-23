using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerups;
    private GameManager gameManager;

	// Use this for initialization
	void Start ()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

    public void StartSpawning()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine()
    {
        while (gameManager.gameOver == false)
        {
            float randomX = Random.Range(-7.5f, 7.5f);
            Instantiate(enemyShipPrefab, new Vector3(randomX, 7.0f, 0), Quaternion.identity);
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator PowerUpSpawnRoutine()
    {
        while (gameManager.gameOver == false)
        {
            float randomX = Random.Range(-7.5f, 7.5f);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup], new Vector3(randomX, 7.0f, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
}
