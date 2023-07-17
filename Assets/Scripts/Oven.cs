using System;
using Unity.VisualScripting;
using UnityEngine;

public class Oven : MonoBehaviour
{
    public Light lightObject;
    public Transform pizzaSpot;

    private bool _isOvenOn;
    private float _lightIntensity;
    private OpenDoor _doorScript;

    private void Start()
    {
        _lightIntensity = lightObject.intensity;
        lightObject.intensity = 0;
        _isOvenOn = false;
        _doorScript = GetComponentInChildren<OpenDoor>();
    }

    public void TurnOn()
    {
        lightObject.intensity = _lightIntensity;

    }

    public void TurnOff()
    {
        lightObject.intensity = 0;
    }

    public void Switch()
    {
        if (_doorScript.IsDoorOpen()) {return;}
        if (_isOvenOn)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }

        _isOvenOn = !_isOvenOn;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pizza"))
        {
            other.transform.position = pizzaSpot.position;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pizza"))
        {
            if (_isOvenOn)
            {
                other.gameObject.GetComponent<PizzaBaker>().SetInOven(true);
            }
            else
            {
                other.gameObject.GetComponent<PizzaBaker>().SetInOven(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pizza"))
        {
            other.gameObject.GetComponent<PizzaBaker>().SetInOven(false);
        }
    }

}
