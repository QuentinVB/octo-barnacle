using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DeathBox : MonoBehaviour
{
    [SerializeField]
    private int playerRespawnPoint;


    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().Respawn(playerRespawnPoint);
        }
    }
}
