using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DeathBox : MonoBehaviour
{
    [SerializeField]
    private int playerRespawnPoint;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().Respawn(playerRespawnPoint);
        }
    }
}
