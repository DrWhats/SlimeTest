using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int maxHealth;
    [SerializeField] int strength;
    [SerializeField] int cost;
    [SerializeField] float _attackCooldown = 2f;
    [SerializeField] Slider healthBar;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private GameObject showDamage;
    
    private float _timeSinceLastAttack;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private Animator anim;
    public BallController balls;
    public SpawnManager spawner;
    
    [SerializeField] private GameObject healthbarCanvas;
    [SerializeField] private GameObject damageAnimPrefab;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        _wallet = GameObject.Find("Game Manager").GetComponent<Wallet>();
    }

    public void SetHealth(int baseHealth)
    {
        maxHealth = baseHealth;
        health = maxHealth;
    }
    
    public void SetCost(int value)
    {
        cost = value;
    }

    public void SetStrenght(int baseStrenght)
    {
        strength = baseStrenght;
    }

    public void UpdateHealthBar()
    {
        healthBar.value = (float)health / maxHealth;
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
            spawner.ZombieKilled();
        }
        else
        {
            health -= damage;
            UpdateHealthBar();
        }
    }

    private void Defeated()
    {
        Destroy(gameObject);
        _wallet.AddCoins(cost);
    }

    public void SetPlayerPosition()
    {
        agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_timeSinceLastAttack >= _attackCooldown)
            {
                Attack(other.gameObject);
                anim.SetTrigger("Attack");
                _timeSinceLastAttack = 0f;
            }
            else
            {
                _timeSinceLastAttack += Time.deltaTime;
            }
        }

        if (other.gameObject.CompareTag("Ball"))
        {
            TakeDamage(balls.damage);
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_timeSinceLastAttack >= _attackCooldown)
            {
                Attack(other.gameObject);
                anim.SetTrigger("Attack");
                _timeSinceLastAttack = 0f;
            }
            else
            {
                _timeSinceLastAttack += Time.deltaTime;
            }
        }
    }

    private void Attack(GameObject player)
    {
        Debug.Log("ATTACK!");
        player.GetComponent<Player>().TakeDamage(strength);
    }
}