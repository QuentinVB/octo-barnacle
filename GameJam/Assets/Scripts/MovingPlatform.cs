using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private List<WayPoint> waypoints;

    [SerializeField]
    private float speed = 2.0f;

    [SerializeField]
    private int targetidx = 0;

    private Vector2 referencePoint;

    private Vector3 startPoint;
    private Vector3 targetPoint;

    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        referencePoint = new Vector2(transform.position.x, transform.position.y);
        startPoint = transform.position;
        targetPoint = new Vector3(referencePoint.x + waypoints[targetidx].position.x, referencePoint.y + waypoints[targetidx].position.y, transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float position = speed * waypoints[targetidx].speedFactor * (Time.time - startTime);
        transform.position = Vector3.Lerp(startPoint, targetPoint, position);
    }

    private void Update()
    {
        // check distance to target point
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y) - referencePoint;
        float distance = Vector2.Distance(currentPos, waypoints[targetidx].position);

        if (Mathf.Abs(distance) < 0.1f)
        {
            targetidx++;

            if (targetidx >= waypoints.Count)
            {
                targetidx = 0;
            }

            startTime = Time.time;
            startPoint = transform.position;
            targetPoint = new Vector3(referencePoint.x + waypoints[targetidx].position.x, referencePoint.y + waypoints[targetidx].position.y, transform.position.z);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0);

        Vector3 center;

        if (referencePoint.x == 0 && referencePoint.y == 0)
        {
            center = transform.position;
        }
        else
        {
            center = new Vector3(referencePoint.x, referencePoint.y, transform.position.z);
        }

        for(int i = 0; i < waypoints.Count; i++)
        {

            Vector3 pointPos = center + new Vector3(waypoints[i].position.x, waypoints[i].position.y);
            int nextPoint = (i + 1 < waypoints.Count) ? i + 1 : 0;

            Vector3 nextPointPos = center + new Vector3(waypoints[nextPoint].position.x, waypoints[nextPoint].position.y);

            drawPoint(pointPos);

            Gizmos.DrawLine(pointPos, nextPointPos);

        }
    }

    void drawPoint(Vector3 pos)
    {
        Gizmos.DrawSphere(pos, 0.35f);
    }
}

[System.Serializable]
class WayPoint
{
    public Vector2 position;
    public float speedFactor = 1.0f;
}
