using UnityEngine;

public class ColliderFollower : MonoBehaviour
{
    public Transform anchor;

    private void LateUpdate()
    {
        if (anchor != null)
        {
            // Get the current position of the collider
            Vector3 currentPosition = transform.position;

            // Set the new position using the x and z axes of the anchor and the y position of the collider
            transform.position = new Vector3(anchor.position.x, currentPosition.y, anchor.position.z);

        }
    }
}
