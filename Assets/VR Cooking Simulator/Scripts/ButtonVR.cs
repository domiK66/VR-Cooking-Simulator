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
        this.gameObject.GetComponent<Collider>().enabled = true;
    }

    // Called when a collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0.003f, 0);

            presser = other.gameObject;
            onPress.Invoke();
            sound.Play();
            isPressed = true;

            // Check the number of ingredients and change button color accordingly
            var ingredientObjects = GameObject.FindGameObjectsWithTag("Ingredient");
            if (ingredientObjects.Length <= 29 && colorChange)
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

    // Called when a collider exits the trigger zone
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0, 0.015f, 0);
            onRelease.Invoke();
            isPressed = false;

            // Reset button color to grey when released
            if (colorChange)
            {
                var buttonMaterial = button.GetComponent<Renderer>().material;
                buttonMaterial.color = Color.grey;
            }

            // Disable the collider briefly to prevent multiple triggers
            StartCoroutine(DisableColliderForSeconds(1f, this.gameObject.GetComponent<Collider>()));
        }
    }

    // Coroutine to disable a collider for a specified duration
    private System.Collections.IEnumerator DisableColliderForSeconds(
        float duration,
        Collider colliderToDisable
    )
    {
        colliderToDisable.enabled = false;

        yield return new WaitForSeconds(duration);

        colliderToDisable.enabled = true;
    }

    // Spawn a new game object
    public void SpawnGameObject()
    {
        var ingredientObjects = GameObject.FindGameObjectsWithTag("Ingredient");
        if (ingredientObjects.Length <= 29)
        {
            Vector3 newPosition = transform.position + offset;
            GameObject spawnedObject = Instantiate(prefab, newPosition, Quaternion.identity);

            // Update the text displaying the number of ingredients
            var TextIngredients = GameObject.FindGameObjectWithTag("TextIngredients");
            TextIngredients.GetComponent<TMPro.TextMeshProUGUI>().text =
                "Ingredients: " + (ingredientObjects.Length + 1) + " / 30";
        }
    }
}
