using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    [SerializeField] private float threshold = .1f;
    [SerializeField] private float deadzone = .025f;

    private bool isPressed;
    private Vector3 startPosition;
    private ConfigurableJoint joint;
    
    public UnityEvent onPressed, onReleased;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPressed && GetValue() + threshold >= 1)
            Pressed();
        if(!isPressed && GetValue() - threshold <= 0)
            Released();
    }

    private float GetValue()
    {
        var value = Vector3.Distance(startPosition, transform.localPosition) / joint.linearLimit.limit;

        if (Math.Abs(value) < deadzone)
        {
            value = 0;
        }

        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        Debug.Log("Pressed");
        isPressed = true;
        onPressed.Invoke();
    }

    private void Released()
    {
        Debug.Log("Released");
        isPressed = false;
        onReleased.Invoke();
    }
}
