using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] private GameObject restartButton;
    [Header("Player Params")]
    public int health;
    public int maxHealth;
    public int strength;
    public float throwDelay = 1f;
    [SerializeField] Slider healthBar;
    
    [Header("Throw Ball Params")]
    [SerializeField] Transform throwPoint;
    [SerializeField] float detectionRadius = 10f;
    [SerializeField] Collider[] zombies;
    [SerializeField] float lastThrowTime;
    [SerializeField] BallController ball;
    
    [Header("Wallet Params")]

    [Header("UI Params")]
    [SerializeField] private UiManager ui;
    //[SerializeField] private GameObject showDamage;
    [SerializeField] private GameObject healthbarCanvas;
    [SerializeField] private GameObject damageAnimPrefab;
    

    private void Awake()
    {
        health = maxHealth;
        Application.targetFrameRate = 60;
    }
    

    public void TakeDamage(int damage)
    {

        GameObject dmg = Instantiate(damageAnimPrefab);
        dmg.transform.SetParent(healthbarCanvas.transform);
        dmg.transform.localPosition = Vector3.zero;
        Debug.Log(dmg.transform.localPosition );
        dmg.GetComponent<TextMeshProUGUI>().SetText(damage.ToString());
        dmg.GetComponent<Animator>().Play("Damage");
        Destroy(dmg, 2);
        if (damage >= health)
        {
            Defeated();
            restartButton.SetActive(true);
        }
        else
        {
            health -= damage;
            UpdateHealthBar();
        }
    }

    public void UpdateHealthBar()
    {
        healthBar.value = (float) health / maxHealth;
    }

    public void Heal()
    {
        health = maxHealth;
        UpdateHealthBar();
    }
    
    public void UpgradeSpeed(float value)
    {
        if (throwDelay >= 0.3f)
        {
            throwDelay -= value;
        }
    }
    
    public void UpgradeStrength(int value)
    {
        strength += value;
    }
    
    public void UpgradeHealth(int value)
    {
        maxHealth += value;
        UpdateHealthBar();
    }
    
    private void Defeated()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        zombies = Physics.OverlapSphere(transform.position, detectionRadius, LayerMask.GetMask("Zombie"));
        
        if (zombies.Length > 0)
        {
            GameObject targetZombie = null;
            float minDistance = Mathf.Infinity;

            foreach (Collider zombie in zombies)
            {
                float distance = Vector3.Distance(transform.position, zombie.transform.position);

                if (distance < minDistance)
                {
                    targetZombie = zombie.gameObject;
                    minDistance = distance;
                }
            }
            
            if (Time.time - lastThrowTime > throwDelay)
            {
                ball.SetTarget(targetZombie);
                ball.LaunchProjectile();
                ball.damage = strength;
                lastThrowTime = Time.time;
            }
        }
    }
}