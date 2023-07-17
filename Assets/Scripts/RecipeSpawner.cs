using UnityEngine;

public class RecipeSpawner : MonoBehaviour
{
    public GameObject recipe;
    private GameObject _recipe;

    void Start()
    {
        InvokeRepeating("SpawnRecipe", 1, 10);
    }

    void SpawnRecipe()
    {
        if (_recipe != null) { return; }
        var position = transform.position;
        _recipe = Instantiate(recipe, new Vector3(position.x + (Random.value*0.1f), position.y + (Random.value*0.1f), position.z + (Random.value*0.1f)), Quaternion.identity);
    }
}
