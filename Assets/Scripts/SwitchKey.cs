using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchKey : MonoBehaviour
{
    [SerializeField] private Transform firstKey;
    [SerializeField] private Transform secondKey;

    private bool wasActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!wasActivated && other.CompareTag("Player"))
        {
            Vector3 firstPosition = firstKey.position;
            Vector3 secondPosition = secondKey.position;

            firstKey.position = secondPosition;
            secondKey.position = firstPosition;
            
            other.GetComponent<PlayerController>().Bug();

            wasActivated = true;
        }
    }    
}
