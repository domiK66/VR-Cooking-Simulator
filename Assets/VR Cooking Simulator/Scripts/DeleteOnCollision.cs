using UnityEngine;

public class DeleteOnCollision : MonoBehaviour
{
    public string parentTag = "IngredientDelete";

    // Called when this object collides with another collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ingredient"))
        {
            // Find and destroy the highest parent object with the specified tag
            Transform parent = GetHighestParentWithTag(other.transform, parentTag);
            if (parent != null)
            {
                Destroy(parent.gameObject);
            }
        }

        // Update the text displaying the number of remaining ingredients
        var ingredientObjects = GameObject.FindGameObjectsWithTag("Ingredient");
        var TextIngredients = GameObject.FindGameObjectWithTag("TextIngredients");
        TextIngredients.GetComponent<TMPro.TextMeshProUGUI>().text =
            "Ingredients: " + (ingredientObjects.Length - 1) + " / 30";
    }

    // Recursively search for the highest parent object with the specified tag
    private Transform GetHighestParentWithTag(Transform child, string tag)
    {
        Transform parent = child.parent;
        while (parent != null)
        {
            if (parent.CompareTag(tag))
            {
                return parent;
            }
            parent = parent.parent;
        }
        return null;
    }
}
