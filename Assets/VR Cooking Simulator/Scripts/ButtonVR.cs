using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonVR : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    public GameObject prefab;
    GameObject presser;
    AudioSource sound;
    bool isPressed;
    public Vector3 offset;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0.003f, 0);
            presser = other.gameObject;
            onPress.Invoke();
            sound.Play();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0, 0.015f, 0);
            onRelease.Invoke();
            isPressed = false;
        }
    }

    public void SpawnGameObject()
    {
        GameObject[] ingredientObjects = GameObject.FindGameObjectsWithTag("Ingredient");

        if (ingredientObjects.Length < 20)
        {
            Vector3 newPosition = transform.position + offset;
            GameObject spawnedObject = Instantiate(prefab, newPosition, Quaternion.identity);
        }
    }
}
