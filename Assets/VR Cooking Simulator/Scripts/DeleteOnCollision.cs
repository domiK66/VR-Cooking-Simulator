using UnityEngine;

public class DeleteOnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {   
        //TODO:
        if (collision.gameObject.CompareTag("Ingredient"))
        {
            Destroy(collision.gameObject);
        }
    }
}
