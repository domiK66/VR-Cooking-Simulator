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

    public bool colorChange = true;

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
            var ingredientObjects = GameObject.FindGameObjectsWithTag("Ingredient");
            if (ingredientObjects.Length < 30 && colorChange)
            {
                var buttonMaterial = button.GetComponent<Renderer>().material;
                buttonMaterial.color = Color.green;
            }
            else
            {
                var buttonMaterial = button.GetComponent<Renderer>().material;
                buttonMaterial.color = Color.red;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0, 0.015f, 0);
            onRelease.Invoke();
            isPressed = false;
            if (colorChange)
            {
                var buttonMaterial = button.GetComponent<Renderer>().material;
                buttonMaterial.color = Color.grey;
            }
        }
    }

    public void SpawnGameObject()
    {
        var ingredientObjects = GameObject.FindGameObjectsWithTag("Ingredient");
        if (ingredientObjects.Length < 30)
        {
            Vector3 newPosition = transform.position + offset;
            GameObject spawnedObject = Instantiate(prefab, newPosition, Quaternion.identity);
        }

        var TextIngredients = GameObject.FindGameObjectWithTag("TextIngredients");
        TextIngredients.GetComponent<TMPro.TextMeshProUGUI>().text =
            "Ingredients: " + ingredientObjects.Length + " / 30";
    }
}
