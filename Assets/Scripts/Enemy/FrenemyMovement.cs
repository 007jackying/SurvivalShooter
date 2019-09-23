using UnityEngine;
using System.Collections;

public class FrenemyMovement : MonoBehaviour
{
    Transform enemy;
    Transform frenmy;
    EnemyHealth enemyHealth;
    EnemyHealth frenemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    EnemyMovement enemyMovement;
    Collider other2;
    bool enemyInRange;
    Vector3[] spawnPoints;
    Vector3 randomSpawnPos;
    string zomname;


    public void Awake()
    {
        Debug.Log("You have awakened a " + gameObject.name);
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
        enemyInRange = false;
        Vector3[] spawnPoints = new Vector3[3];
        spawnPoints[0] = new Vector3(-25.5f, 0, 12.5f);
        spawnPoints[1] = new Vector3(22.5f, 0, 15f);
        spawnPoints[2] = new Vector3(0, 0, 32f);
        int rand = Random.Range(0, 99);
        if (rand < 22) { randomSpawnPos = spawnPoints[0]; }
        else if (rand > 22 && rand < 55) { randomSpawnPos = spawnPoints[1]; }
        else if (rand > 55) { randomSpawnPos = spawnPoints[2]; }
        frenmy = gameObject.transform;
        
    }   

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("You have bumped into an enemy" + other.gameObject.name);
        var objName = other.gameObject.name;
        if (objName.Contains("Zombunny(Clone)") || objName.Contains("ZomBear(Clone)") || objName.Contains("Hellephant(Clone)"))
        {
            enemyInRange = true;
            enemy = other.gameObject.transform;
            enemyHealth = enemy.GetComponent<EnemyHealth>();
        }
        else
        {
            enemyInRange = false;
        }
    }


    void Update()
    {
        //var dist1 = Vector3.Distance(frenmy.position, spawnPoints[0]);
        //Debug.Log(dist1);
        //var dist2 = Vector3.Distance(frenmy.position, spawnPoints[1]);
        //var dist3 = Vector3.Distance(frenmy.position, spawnPoints[2]);

        nav.SetDestination(randomSpawnPos);
        if (enemyHealth.currentHealth > 0 && enemyInRange == true)
        {
            //Debug.Log("IF");
            nav.SetDestination(enemy.position);
        }
        /*if (enemyInRange == false)
        {
            Debug.Log("no enemy");
            if (dist1 < dist2 && dist1 < dist3) { nav.SetDestination(spawnPoints[0]); }
            if (dist2 < dist1 && dist2 < dist3) { nav.SetDestination(spawnPoints[1]); }
            if (dist3 < dist1 && dist3 < dist2) { nav.SetDestination(spawnPoints[3]); }
        }*/

        //nav.SetDestination(randomSpawnPos);//assigning zombilly path to one of the spawnpoint(randomlly selected


    }

}
