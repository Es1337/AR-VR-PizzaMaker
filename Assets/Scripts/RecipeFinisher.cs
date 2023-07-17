using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeFinisher : MonoBehaviour
{
    private TimeController _timeController;
    private int _score;
    private Dictionary<EIngredientTypes, int> _recipe;
    private void Start()
    {
        _timeController = GameObject.Find("TimeControl").GetComponent<TimeController>();
        _recipe = gameObject.GetComponent<Recipe>().GetRecipe();
        if(_recipe == null) Debug.LogError("Recipe is null");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pizza"))
        {
            Dictionary<EIngredientTypes, int> ingredientsAdded = other.GetComponent<PizzaBaker>().ingredientsAdded;
            foreach (KeyValuePair<EIngredientTypes, int> line in _recipe)
            {
                if (ingredientsAdded[line.Key].Equals(line.Value))
                {
                    _score++;
                }
            }
            _timeController.GetComponent<TimeController>().AddScore(_score);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
