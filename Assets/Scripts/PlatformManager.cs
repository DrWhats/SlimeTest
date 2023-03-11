using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject lastPlatform;
    [SerializeField] private GameObject currentPlatform;
    [SerializeField] private EnemyAi player;
    [SerializeField] private WaveManager waves;

    private void Start()
    {
        currentPlatform = GameObject.Find("Platform");
        waves = FindObjectOfType<WaveManager>();
        player = GameObject.Find("Player").GetComponent<EnemyAi>();
        CreateNextPlatform();
    }

    public void SetCurrentPlatform(GameObject platform)
    {
        lastPlatform = currentPlatform;
        currentPlatform = platform;
        DestroyLastPlatform();
        CreateNextPlatform();
        waves.StartWave();
    }

    public GameObject GetCurrentPlatform()
    {
        return currentPlatform;
    }


    public void DestroyLastPlatform()
    {
        Destroy(lastPlatform, 5);
    }

    public void CreateNextPlatform()
    {
        Destroy(GameObject.Find("WayPoint"));
        GameObject platform = Instantiate(platformPrefab);
        var pos = currentPlatform.GetComponent<Transform>().position;
        platform.transform.position = new Vector3(pos.x - 20, pos.y, pos.z);
        player.FindWayPoint();
        gameObject.GetComponent<WaveManager>().UpdateSpawner();
    }
}