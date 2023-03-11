using UnityEngine;


public class WaveManager : MonoBehaviour
{
    public int enemyHealth;
    public int enemyStrenght;
    public int currentWave = 0;
    public int enemyCost;
    [SerializeField] private float enemyCostMultiplier;
    [SerializeField] private PlatformManager platforms;
    [SerializeField] private SpawnManager spawner;
    [SerializeField] private GameObject currentPlatform;
    [SerializeField] private EnemyAi playerAI;
    [SerializeField] private Player player;

    [Range(1f, 1.5f)] [SerializeField] private float enemyHealthMultiplier = 1;

    [Range(1f, 1.5f)] [SerializeField] private float enemyDamageMultiplier = 1;

    public void StartWave()
    {
        UpdateSpawner();
        if (currentWave != 1)
        {
            player.Heal();
            enemyHealth = (int)(enemyHealth * enemyHealthMultiplier);
            enemyStrenght = (int) (enemyStrenght * enemyDamageMultiplier);
            enemyCost = (int) (enemyCost * enemyCostMultiplier);
        }
        currentWave += 1;
        spawner.StartWave();
    }

    public void UpdateSpawner()
    {
        platforms = gameObject.GetComponent<PlatformManager>();
        currentPlatform = platforms.GetCurrentPlatform();
        spawner = currentPlatform.GetComponentInChildren<SpawnManager>();
    }

    public void GoNext()
    {
        playerAI.currentState = SlimeAnimationState.Walk;
    }


}