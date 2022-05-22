using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider))]
public class BreakingPlateform : MonoBehaviour
{

    Collider collider;
    MeshRenderer mr;

    [SerializeField]
    private float breakDelay = 0.7f;
    [SerializeField]
    private float shakeIntensity = 0.15f;

    [SerializeField]
    private bool respawn = true;
    [SerializeField]
    private float respawnDelay = 5.0f;

    private bool breaking = false;
    private Vector3 originalPos;

    

    // Start is called before the first frame update
    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (breaking)
        {
            // shake effect
            transform.position = new Vector3(originalPos.x + Random.Range(-1, 1) * shakeIntensity, originalPos.y, originalPos.z);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(BreakPlateform());
        }
    }

    IEnumerator BreakPlateform()
    {
        breaking = true;
        originalPos = transform.position;
        
        yield return new WaitForSeconds(breakDelay);


        breaking = false;

        mr.enabled = false;
        collider.enabled = false;

        if (respawn)
        {
            yield return new WaitForSeconds(respawnDelay);

            mr.enabled = true;
            collider.enabled = true;
        }
    }
}
