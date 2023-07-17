using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public Rigidbody rb;
    public EIngredientTypes ingredientType;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Let animation control the rigidbody and ignore collisions.
    void DisableRagdoll()
    {
        rb.isKinematic = true;
        rb.detectCollisions = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pizza"))
        {
            gameObject.transform.SetParent(other.transform);
            gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            Vector3 currentPos = gameObject.transform.position;
            gameObject.transform.position = new Vector3(currentPos.x, other.transform.position.y + 0.03f, currentPos.z);
            DisableRagdoll();
        }
    }
}
