using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisibilityTester : MonoBehaviour
{
    // Public instance fields
    public Vector3 headTransform;

    // Gets the layer mask used for visibility testing
    private int LayerMask()
    {
        // Mask: layer 9 (player) | layer 10 (level)
        return (1 << 9) | (1 << 10);
    }


    // IsVisible can be used to determine if the location
    // can see the player's head by performing a raycast.
    public bool IsVisible(Vector3 position)
    {
        Vector3 playerPosition = this.gameObject.transform.position + this.headTransform;
        RaycastHit hit;
        if (Physics.Raycast(position, playerPosition - position, out hit, 300f, this.LayerMask()))
        {
            return hit.transform == this.gameObject.transform;
        }

        return true;
    }

    // DrawGizmo draws a Gizmo to the given location,
    // showing the position of hit if relevant
    public void DrawGizmo(Vector3 position)
    {
        Vector3 playerPosition = this.gameObject.transform.position + this.headTransform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(playerPosition, 0.125f);
        Gizmos.DrawRay(position, playerPosition - position);
        RaycastHit hit;
        if (Physics.Raycast(position, playerPosition - position, out hit, 300f, this.LayerMask()))
        {
            Gizmos.DrawSphere(playerPosition, 0.125f);
            if (hit.transform == this.gameObject.transform) {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(position, hit.point - position);
                Gizmos.DrawSphere(hit.point, 0.125f);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(position, hit.point - position);
                Gizmos.DrawSphere(hit.point, 0.125f);
            }
        }
        else
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(position, playerPosition - position);
        }
    }
}
