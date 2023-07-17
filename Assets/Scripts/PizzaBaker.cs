using System;
using System.Collections.Generic;
using UnityEngine;

public class PizzaBaker : MonoBehaviour
{
    public EPizzaTypes pizzaType;
    public Color bakedColor;
    public float bakingTime;
    public Dictionary<EIngredientTypes, int> ingredientsAdded;
    
    private float _bakeTimePassedPercent;
    private Color _rawColor;
    private float _elapsedTime;
    private Material _material;
    private bool _isInOven;

    private void Start()
    {
        ingredientsAdded = new Dictionary<EIngredientTypes, int>();
        var ingredientTypes = Enum.GetValues(typeof(EIngredientTypes));
        foreach(EIngredientTypes ingredientType in ingredientTypes)
        {
            ingredientsAdded.Add(ingredientType, 0);
        }
    }

    private void Awake()
    {
        _material = GetComponentInChildren<Renderer>().material;
        _rawColor = _material.color;
        _isInOven = false;
    }

    void Update()
    {
        if (!_isInOven) return;
        
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime < bakingTime)
        {
            _bakeTimePassedPercent = _elapsedTime / bakingTime;
            _material.color = Color.Lerp(_rawColor, bakedColor, _bakeTimePassedPercent);
        }
        else if (_elapsedTime > 1.5 * bakingTime)
        {
            _bakeTimePassedPercent = 0f;
            _material.color = new Color(bakedColor.r * 0.5f, bakedColor.g * 0.5f, bakedColor.b * 0.5f);
        }
    }

    public void SetInOven(bool isInOven)
    {
        _isInOven = isInOven;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            EIngredientTypes ingredientType = other.GetComponent<Ingredient>().ingredientType;
            ingredientsAdded[ingredientType] += 1;
            Debug.Log("Ingredient " + ingredientType + " amount: " + ingredientsAdded[ingredientType]);
        }
    }

    public float GetPercentBaked()
    {
        return _bakeTimePassedPercent;
    }
}
