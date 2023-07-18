using UnityEngine;

public class DeleteOnCollision : MonoBehaviour
{
    public string parentTag = "IngredientDelete";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ingredient"))
        {
            Transform parent = GetHighestParentWithTag(other.transform, parentTag);
            if (parent != null)
            {
                Destroy(parent.gameObject);
            }
        }
        var ingredientObjects = GameObject.FindGameObjectsWithTag("Ingredient");
        var TextIngredients = GameObject.FindGameObjectWithTag("TextIngredients");
        TextIngredients.GetComponent<TMPro.TextMeshProUGUI>().text =
            "Ingredients: " + (ingredientObjects.Length - 1) + " / 30";
    }

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
