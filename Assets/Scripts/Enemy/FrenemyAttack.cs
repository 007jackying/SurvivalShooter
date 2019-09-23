using UnityEngine;
using System.Collections;

public class FrenemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject player;
    AudioSource enemyAudio;
    public AudioClip deathClip;
    string enemyName;
    Transform enemy;
    EnemyHealth frenemyHealth;
    EnemyHealth enemyHealth;
    bool enemyInRange;
    float timer;

    void Awake()
    {
        frenemyHealth = GetComponent<EnemyHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("You have bumped into an enemy" + other.gameObject.name);
        enemyName = other.gameObject.name;
        if (enemyName.Contains("Zombunny(Clone)") || enemyName.Contains("ZomBear(Clone)") || enemyName.Contains("Hellephant(Clone)"))
        {
            enemyInRange = true;
            enemy = other.gameObject.transform;
            enemyHealth = enemy.GetComponent<EnemyHealth>();
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (enemyName.Contains("Zombunny(Clone)") || enemyName.Contains("ZomBear(Clone)") || enemyName.Contains("Hellephant(Clone)"))
        {
            enemyInRange = false;
        }
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && enemyInRange && enemyHealth.currentHealth > 0)
        {
            Attack();
        }
    }


    void Attack()
    {
        timer = 0f;

        if (enemyHealth.currentHealth > 0)
        {
            enemyHealth.TakeDamage(attackDamage,gameObject.transform.position);
            frenemyHealth.TakeDamage(attackDamage, enemy.position);

        }
    }


}
