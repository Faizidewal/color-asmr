using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneOfPart : MonoBehaviour
{
    public Vector3 center;

    public Vector3 offset;

    public float width, height;

    public Color trueColor = new(0, 0, 0, 1);

    private void OnDrawGizmosSelected()
    {
        center = offset + transform.position;

        Gizmos.color = Color.red;

        Gizmos.DrawLine(new Vector3(center.x + width, center.y + height, center.z), new Vector3(center.x + width, center.y - height, center.z));

        Gizmos.DrawLine(new Vector3(center.x + width, center.y - height, center.z), new Vector3(center.x - width, center.y - height, center.z));

        Gizmos.DrawLine(new Vector3(center.x - width, center.y - height, center.z), new Vector3(center.x - width, center.y + height, center.z));

        Gizmos.DrawLine(new Vector3(center.x - width, center.y + height, center.z), new Vector3(center.x + width, center.y + height, center.z));
    }
}
