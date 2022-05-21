using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{

    [SerializeField]
    private float radius;

    [SerializeField]
    private Vector3 center;

    [SerializeField]
    private float angle = 0;

    [SerializeField]
    private float speed = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        setPositionOnCircle();
    }

    // Update is called once per frame
    void Update()
    {
        angle += 360 * speed * Time.deltaTime;
        setPositionOnCircle();
    }

    void setPositionOnCircle()
    {
        transform.position = new Vector3(center.x + radius * Mathf.Cos(Mathf.Deg2Rad * angle), center.y + radius * Mathf.Sin(Mathf.Deg2Rad * angle), transform.position.z);
    }

    private void OnDrawGizmosSelected()
    {
        DrawWireDisk(center, radius, new Color(0, 1, 0));

        // set at correct position on disk
        setPositionOnCircle();
    }

    private const float GIZMO_DISK_THICKNESS = 0.01f;
    public static void DrawWireDisk(Vector3 position, float radius, Color color)
    {
        Color oldColor = Gizmos.color;
        Gizmos.color = color;
        Matrix4x4 oldMatrix = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(position, Quaternion.identity, new Vector3(1, 1, GIZMO_DISK_THICKNESS));
        Gizmos.DrawWireSphere(Vector3.zero, radius);
        Gizmos.matrix = oldMatrix;
        Gizmos.color = oldColor;
    }
}
