using System.Collections;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Transform pivotTransform;
    public float rotationDuration = 1f;
    
    private float _rotationSpeed;
    private Vector3 _currentEulerAngles;
    private bool _isDoorOpen;

    private void Start()
    {
        _rotationSpeed = 90f / rotationDuration;
    }


    public void DoorSwitch()
    {
        if (!_isDoorOpen)
        {
            StartCoroutine(MoveDoorCoroutine(Vector3.right));
        }
        else
        {
            StartCoroutine(MoveDoorCoroutine(Vector3.left));
        }

        _isDoorOpen = !_isDoorOpen;
    }

    private IEnumerator MoveDoorCoroutine(Vector3 turnAxis)
    {
        float elapsedTime = 0f;
        while (elapsedTime < rotationDuration)
        {
            float t = elapsedTime / rotationDuration;
            _currentEulerAngles += turnAxis * (_rotationSpeed * Time.deltaTime);
            pivotTransform.localEulerAngles = _currentEulerAngles;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public bool IsDoorOpen()
    {
        return _isDoorOpen;
    }
}
