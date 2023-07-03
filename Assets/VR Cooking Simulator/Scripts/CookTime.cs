using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookTime : MonoBehaviour
{
    private float time = 0f; // Cooking time in seconds
    private float targetTime; // Random target cooking time between 30 and 40 seconds
    public TextMesh timeText; // TextMesh component to display the time
    public Renderer Renderer; // Renderer component of the sausage
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

    targetTime = Random.Range(15f, 30f);
        
    }

    private void OnTriggerStay(Collider other)
    {
        // Generate a random cooking time between 15 and 30 seconds
    

        if (other.gameObject == grillAreaCollider)
        {
            time += Time.deltaTime;
            timeText.text = time.ToString("F2") + "s";

            if (time >= targetTime && time < targetTime + 10f)
            {
                // Apply good material
                Renderer.material = goodMaterial;
            }
            else if (time >= targetTime + 10f)
            {
                // Apply overcooked material
                Renderer.material = overcookedMaterial;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        timeText.text = "";
    }
}
