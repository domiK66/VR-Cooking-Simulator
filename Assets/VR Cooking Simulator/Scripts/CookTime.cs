using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CookingState
{
    raw,
    good,
    burned
}

public class CookTime : MonoBehaviour
{
    public float time = 0f; // Cooking time in seconds
    public CookingState cookingState = CookingState.raw;
    private float targetTime; // Random target cooking time between 15 and 30 seconds
    public Renderer Renderer; // Renderer component of the sausage
    public Material goodMaterial;
    public Material overcookedMaterial;
    public string grillAreaTag; // Tag of the grill area collider
    public AudioSource sound;
    private GameObject grillAreaCollider;
    private GameObject grillAreaCollider2; // Reference to the grill area collider object
    private GameObject grillAreaCollider3;
    public ParticleSystem smokeParticles;

    private void Start()
    {
        // Find the grill area colliders based on their tags
        grillAreaCollider = GameObject.FindGameObjectWithTag("GrillAreaCollider");
        grillAreaCollider2 = GameObject.FindGameObjectWithTag("GrillAreaCollider2");
        grillAreaCollider3 = GameObject.FindGameObjectWithTag("GrillAreaCollider3");

        if (grillAreaCollider == null)
        {
            Debug.LogWarning("Grill area collider not found with tag: " + grillAreaTag);
        }
        if (grillAreaCollider2 == null)
        {
            Debug.LogWarning("Grill area collider 2 not found with tag: " + grillAreaTag);
        }
        if (grillAreaCollider3 == null)
        {
            Debug.LogWarning("Grill area collider 3 not found with tag: " + grillAreaTag);
        }

        // Generate a random target cooking time between 15 and 30 seconds
        targetTime = Random.Range(15f, 30f);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the item enters the grill area collider
        if (
            other.gameObject == grillAreaCollider
            || other.gameObject == grillAreaCollider2
            || other.gameObject == grillAreaCollider3
        )
        {
            // Play the sound and start the smoke particles
            sound.Play();
            smokeParticles.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the item stays within the grill area collider
        if (
            other.gameObject == grillAreaCollider
            || other.gameObject == grillAreaCollider2
            || other.gameObject == grillAreaCollider3
        )
        {
            time += Time.deltaTime; // Increment the cooking time

            if (time >= targetTime && time < targetTime + 10f)
            {
                // Apply good material to the renderer
                Renderer.material = goodMaterial;
                cookingState = CookingState.good;
            }
            else if (time >= targetTime + 30f)
            {
                // Apply overcooked material to the renderer
                Renderer.material = overcookedMaterial;
                cookingState = CookingState.burned;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the item exits the grill area collider
        if (
            other.gameObject == grillAreaCollider
            || other.gameObject == grillAreaCollider2
            || other.gameObject == grillAreaCollider3
        )
        {
            // Stop the sound and smoke particles
            sound.Stop();
            smokeParticles.Stop();
        }
    }
}
