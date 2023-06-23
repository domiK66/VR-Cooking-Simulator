using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotDogCookTime : MonoBehaviour
{
    private float time = 0f; // Cooking time in seconds
    public TextMesh timeText; // TextMesh component to display the time
    public Renderer sausageRenderer; // Renderer component of the sausage
    public Material goodMaterial;
    public Material overcookedMaterial;
    public string grillAreaTag; // Tag of the grill area collider

    private GameObject grillAreaCollider; // Reference to the grill area collider object

    private void Start()
    {
        grillAreaCollider = GameObject.FindGameObjectWithTag("GrillAreaCollider");
        if (grillAreaCollider == null)
        {
            Debug.LogWarning("Grill area collider not found with tag: " + grillAreaTag);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == grillAreaCollider)
        {
            time += Time.deltaTime;
            timeText.text = "Cooking Time: " + time.ToString("F2") + "s";

            if (time >= 30f && time < 40f)
            {
                // Apply good material
                sausageRenderer.material = goodMaterial;
            }
            else if (time >= 40f)
            {
                // Apply overcooked material
                sausageRenderer.material = overcookedMaterial;
            }
        }
    }
}
