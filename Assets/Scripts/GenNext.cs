using UnityEngine;

public class GenNext : MonoBehaviour
{
    
    [SerializeField] private PlatformManager platformManager;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            platformManager.SetCurrentPlatform(gameObject.transform.parent.gameObject);
        }
        
    }

    private void Awake()
    {
        platformManager = GameObject.Find("Game Manager").GetComponent<PlatformManager>();
    }

}
