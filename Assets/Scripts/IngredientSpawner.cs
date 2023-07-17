using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    public GameObject ingredient;
    public int maxIngredients;
    public float spawnOffsetY;
    private List<GameObject> _ingredients;
    
    // Start is called before the first frame update
    void Start()
    {
        _ingredients = new List<GameObject>();
        InvokeRepeating("SpawnIngredient", 1, 5);
    }

    void SpawnIngredient()
    {
        if(_ingredients.Count >= maxIngredients) return;
        
        var position = transform.position;
        var newIngredient = Instantiate(ingredient, new Vector3(position.x + (Random.value*0.1f), position.y + spawnOffsetY, position.z + (Random.value*0.1f)), Random.rotation);
        _ingredients.Add(newIngredient);
    }
}
