using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidationAndPoints : MonoBehaviour
{
    public Collider targetCollider; // The collider area to check against
    private List<GameObject> objectsToValidate = new List<GameObject>(); // List to store objects that collide with the target collider

    private void OnTriggerEnter(Collider other)
    {
        if (!objectsToValidate.Contains(other.gameObject))
        {
            objectsToValidate.Add(other.gameObject);
            Debug.Log(other.gameObject.name + " entered the collider area.");
            // Add your desired logic or actions here when an object enters the collider area
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectsToValidate.Contains(other.gameObject))
        {
            objectsToValidate.Remove(other.gameObject);
            Debug.Log(other.gameObject.name + " exited the collider area.");
            // Add your desired logic or actions here when an object exits the collider area
        }
    }
}
