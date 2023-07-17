using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenController : MonoBehaviour
{
    private bool state;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState()
    {
        state = !state;
    }
}
