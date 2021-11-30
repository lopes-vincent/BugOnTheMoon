using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeKey : MonoBehaviour
{

    [SerializeField] private CodePlace placeValueToChange;

    [SerializeField] private string newValue;
    
    private bool wasActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!wasActivated && other.CompareTag("Player"))
        {
            placeValueToChange.codeAttached.gameObject.GetComponentInChildren<Text>().text = newValue;
            placeValueToChange.UpdateValue();
            wasActivated = true;
            
            other.GetComponent<PlayerController>().Bug();

        }
    }    
}
