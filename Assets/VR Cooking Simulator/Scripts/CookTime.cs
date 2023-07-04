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
    private float targetTime; // Random target cooking time between 30 and 40 seconds
    public TextMesh timeText; // TextMesh component to display the time
    public Renderer Renderer; // Renderer component of the sausage
    public Material goodMaterial;
    public Material overcookedMaterial;
    public string grillAreaTag; // Tag of the grill area collider
    public AudioSource sound;
    private GameObject grillAreaCollider; // Reference to the grill area collider object

    public ParticleSystem smokeParticles;

    private void Start()
    {
        grillAreaCollider = GameObject.FindGameObjectWithTag("GrillAreaCollider");
        if (grillAreaCollider == null)
        {
            Debug.LogWarning("Grill area collider not found with tag: " + grillAreaTag);
        }

        targetTime = Random.Range(15f, 30f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == grillAreaCollider)
        {
            sound.Play();
            smokeParticles.Play();
        }
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
                cookingState = CookingState.good;
            }
            else if (time >= targetTime + 10f)
            {
                // Apply overcooked material
                Renderer.material = overcookedMaterial;
                cookingState = CookingState.burned;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        timeText.text = "";
        sound.Stop();
        smokeParticles.Stop();
    }
}
