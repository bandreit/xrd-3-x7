using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbableObject : MonoBehaviour
{
    [SerializeField]
    private XRDirectInteractor interactorLeft;
  
    [SerializeField]
    private  TextMeshProUGUI text;

    private int score;

    private void Start()
    {
        score = 0;
    }

    private void OnEnable()
    {
        interactorLeft.onSelectEntered.AddListener(TakeInput);
        interactorLeft.onSelectExited.AddListener(StopInput);

    }
   
    private void OnDisable()
    {
 
        interactorLeft.onSelectEntered.RemoveListener(TakeInput);
        interactorLeft.onSelectExited.RemoveListener(StopInput);
 
    }
 
    private void TakeInput(XRBaseInteractable interactable)
    {
        Debug.Log("is grabbing");
 
    }
 
    private void StopInput(XRBaseInteractable interactable)
    {
        
        Debug.Log("is releasing");
        Destroy(interactable.gameObject);
        score++;
        if (score == 5)
        {
            text.text = $"CONGRATS :)";
        }
        text.text = $"Collected items:{score}/5";
    }
    
}
