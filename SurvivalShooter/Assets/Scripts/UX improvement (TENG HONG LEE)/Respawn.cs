using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class will be change the current player positon to the origin(player spawn point).
 * @author: Teng Hong Lee (201723459)
 */
public class Respawn : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        respawnPoint.transform.position = new Vector3(0, 0, -29);   
    }
    /*
     * This function will be called by PlayerHealth.cs under TakeDamaged(). 
     * When this function is called,player will be moved from current position to spawn point.
     * */
    public void RespawnPlayer()
    {
        player.transform.position = respawnPoint.transform.position;
    }
}
