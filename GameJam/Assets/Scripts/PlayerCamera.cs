using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    
    [SerializeField]
    private float distance;
    private Vector3 offset;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Rect bounds;

    [SerializeField]
    [Tooltip("Draw the bounding rectangle of the camera in the editor")]
    private bool drawBounds;

    [SerializeField]
    private float followSpeed = 0.1f;

    [SerializeField]
    private float approachThreshold = 0.1f;
    [SerializeField]
    private float leavingThreshold = 0.5f;

    private bool wasCloseToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        offset = -new Vector3(0, 0, distance);

        wasCloseToPlayer = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = followPlayer();
        newPos = clampToBounds(newPos);

        transform.position = newPos;
    }

    Vector3 followPlayer()
    {
        Vector3 newPos = player.position + offset;

        // calc distance to player
        float distToPlayer = Vector3.Distance(newPos, transform.position);

        if (wasCloseToPlayer)
        {
            if (distToPlayer > leavingThreshold)
            {
                // was close, but is now further away
                wasCloseToPlayer = false;
                Debug.Log("was close, is far");
                return Vector3.Lerp(transform.position, newPos, followSpeed);
            }
            else
            {
                // was close, is close
                Debug.Log("was close, is close");
                return newPos;
            }

        }
        else
        {
            if (distToPlayer > approachThreshold)
            {
                // was far, is far
                Debug.Log("was far, is far");
                return Vector3.Lerp(transform.position, newPos, followSpeed);
            }
            else
            {
                // was far, is close
                wasCloseToPlayer = true;
                Debug.Log("was far, is close");
                return Vector3.Lerp(transform.position, newPos, followSpeed);
            }
        }
    }

    Vector3 clampToBounds(Vector3 pos)
    {
        pos.x = Mathf.Clamp(pos.x, bounds.xMin, bounds.xMax);
        pos.y = Mathf.Clamp(pos.y, bounds.yMin, bounds.yMax);

        return pos;
    }

    private void OnDrawGizmos()
    {
        if (drawBounds)
        {
            Gizmos.color = new Color(0, 1, 0);
            Gizmos.DrawWireCube(new Vector3(bounds.center.x, bounds.center.y, player.position.z), new Vector3(bounds.size.x, bounds.size.y, 0.01f));
        }
    }
}
