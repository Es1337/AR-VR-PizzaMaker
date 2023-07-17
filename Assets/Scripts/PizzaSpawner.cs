 using UnityEngine;

public class PizzaSpawner : MonoBehaviour
{
    public GameObject pizza;
    private GameObject _pizza;

    void Start()
    {
        InvokeRepeating("SpawnPizza", 1, 10);
    }

    void SpawnPizza()
    {
        if (_pizza != null) { return; }
        var position = transform.position;
        _pizza = Instantiate(pizza, new Vector3(position.x + (Random.value*0.1f), position.y + (Random.value*0.1f), position.z + (Random.value*0.1f)), Quaternion.identity);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pizza"))
        {
            _pizza = null;
        }
    }
}
