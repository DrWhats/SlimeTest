using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemy configuration")] [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField] private int numberOfEnemies;
    [SerializeField] private int spawnDelay;
    [SerializeField] private BallController balls;
    private WaveManager waveManager;
    private int _killedZombiesCount;

    private void Awake()
    {
        waveManager = FindObjectOfType<WaveManager>();
        balls = GameObject.FindGameObjectWithTag("Player").GetComponent<BallController>();
    }


    public void StartWave()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            Enemy enemyInst = enemy.GetComponent<Enemy>();
            enemyInst.SetHealth(waveManager.enemyHealth);
            enemyInst.SetStrenght(waveManager.enemyStrenght);
            enemyInst.SetCost(waveManager.enemyCost);
            enemyInst.SetPlayerPosition();
            enemyInst.balls = balls;
            enemyInst.spawner = this;
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void ZombieKilled()
    {
        _killedZombiesCount++;
        if (_killedZombiesCount == numberOfEnemies)
        {
            LevelComplete();
        }
    }

    public void LevelComplete()
    {
        waveManager.GoNext();

    }
}