using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RightHandTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private InputActionProperty triggered;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // float triggerValue = triggered.action.ReadValue<float>();
        
        //Debug.Log(triggerValue);
    }
}
