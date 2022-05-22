using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DeathBox : MonoBehaviour
{
    [SerializeField]
    public int playerRespawnPoint;


    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.name == "bob")
        {
            other.gameObject.GetComponent<Player>().Respawn(playerRespawnPoint);
        }
    }
}
