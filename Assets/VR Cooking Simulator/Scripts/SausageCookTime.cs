using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageCookTime : MonoBehaviour
{
    public Collider GrillCollider;
    private float time = 0f; // Cooking time in seconds
    public TextMesh timeText; // TextMesh component to display the time
    public Renderer sausageRenderer; // Renderer component of the sausage
    public Material goodMaterial;
    public Material overcookedMaterial;

    private void Start()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (other == GrillCollider)
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
