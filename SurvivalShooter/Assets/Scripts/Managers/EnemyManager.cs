using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    //public EnemyHealth enemyHealth;
    public float spawnTime =3f;
    public Transform[] spawnPoints;
    
    public void Start ()
    {
        CancelInvoke("Spawn");
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }

    public void updateSpawner(float st)
    {
        CancelInvoke("Spawn");
        InvokeRepeating("Spawn", 2, st);
    }
}
