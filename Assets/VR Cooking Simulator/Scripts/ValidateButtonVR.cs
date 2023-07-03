using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ValidateButtonVR : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    public Collider targetCollider; // The collider area to check against
    private List<GameObject> objectsToValidate = new List<GameObject>(); // List to store objects that collide with the target collider
    GameObject presser;
    AudioSource sound;
    bool isPressed;

    public TextMeshProUGUI displayText; // Reference to the TextMeshProUGUI component in the Canvas
    public string[] ingredientsArray; // Array to store ingredient names

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

            ValidateObjects();
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

    public void ValidateObjects()
    {
        Collider[] colliders = Physics.OverlapBox(targetCollider.bounds.center, targetCollider.bounds.extents, targetCollider.transform.rotation);

        int ingredientCount = 0; // Counter for the number of ingredients found
        displayText.text = ""; // Clear the text display

        foreach (Collider collider in colliders)
        {
            GameObject obj = collider.gameObject;

            if (obj.CompareTag("Ingredient"))
            {
                // Display the ingredient name in the TextMeshProUGUI component
                displayText.text += obj.name + "\n";

                // Access the highest parent GameObject
                GameObject highestParent = GetHighestParent(obj);

                // Access the component in the highest parent GameObject
                CookTime cookTime = highestParent.GetComponent<CookTime>();

                // Check if the cooktime script is present in the highest parent GameObject
                if (cookTime != null)
                {
                    // Access the public variable of the cooktime script
                    CookingState cookingState = cookTime.cookingState;

                    // Use the cookingTime variable as needed
                    Debug.Log("Cooking time of " + obj.name + "'s highest parent: " + cookingState);
                }
                else
                {
                    Debug.Log("No cooktime script found in " + obj.name + "'s highest parent");
                }
            }
        }
    }

    private GameObject GetHighestParent(GameObject obj)
    {
        Transform parentTransform = obj.transform;
        while (parentTransform.parent != null)
        {
            parentTransform = parentTransform.parent;
        }
        return parentTransform.gameObject;
    }

}
