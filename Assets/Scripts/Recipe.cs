using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Recipe : MonoBehaviour
{
    public TextMeshProUGUI titleComponent;
    public TextMeshProUGUI ingredientsComponent;

    private EPizzaTypes _pizzaType;
    private Dictionary<EIngredientTypes, int> _recipe;
    private TimeController _timeController;
    private int _score;
    private bool _isProcessed;
    
    private void Start()
    {
        _timeController = GameObject.Find("TimeControl").GetComponent<TimeController>();
        _pizzaType = (EPizzaTypes)GetRandomEnumValue(typeof(EPizzaTypes));
        _recipe = new Dictionary<EIngredientTypes, int>();
        var ingredientTypes = Enum.GetValues(typeof(EIngredientTypes));
        foreach(EIngredientTypes ingredientType in ingredientTypes)
        {
            _recipe.Add(ingredientType, Random.Range(0, 5));
        }

        titleComponent.text = _pizzaType + " Pizza";
        
        foreach (KeyValuePair<EIngredientTypes, int> ingredient in _recipe)
        {
            if(ingredient.Value != 0)
                ingredientsComponent.text += ingredient.Key + ": " + ingredient.Value + " pcs\n";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_isProcessed) return;
        _isProcessed = true;
        if (other.CompareTag("Pizza"))
        {
            PizzaBaker pizzaBaker = other.GetComponent<PizzaBaker>();
            float scoreMultiplier = pizzaBaker.GetPercentBaked() >= 0.5f ? pizzaBaker.GetPercentBaked() : 0f;
            scoreMultiplier *= _pizzaType.Equals(pizzaBaker.pizzaType) ? 1.0f : 0.5f;
            _score += (int) ((int)pizzaBaker.pizzaType * scoreMultiplier);
            
            Dictionary<EIngredientTypes, int> ingredientsAdded = other.GetComponent<PizzaBaker>().ingredientsAdded;
            foreach (KeyValuePair<EIngredientTypes, int> line in _recipe)
            {
                if (line.Value.Equals(0))
                {
                    continue;
                }
                
                if (!ingredientsAdded[line.Key].Equals(line.Value))
                {
                    _score -= Math.Abs(line.Value - ingredientsAdded[line.Key]);
                }
            }
            _score = Math.Max(0, _score);
            _timeController.GetComponent<TimeController>().AddScore(_score);
            Destroy(other.gameObject);
            _isProcessed = false;
            Destroy(gameObject);
        }
    }

    public Dictionary<EIngredientTypes, int> GetRecipe()
    {
        return _recipe;
    }
    
        
    public static Enum GetRandomEnumValue(Type t)
    {
        return Enum.GetValues(t)          // get values from Type provided
            .OfType<Enum>()               // casts to Enum
            .OrderBy(e => Guid.NewGuid()) // mess with order of results
            .FirstOrDefault();            // take first item in result
    }
}
