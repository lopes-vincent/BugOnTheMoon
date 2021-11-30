using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeDrop : MonoBehaviour
{
    [SerializeField]
    private GameObject codeValue;
    
    private bool _activated = false;

    void Update()
    {
        if (_activated && !gameObject.GetComponent<AudioSource>().isPlaying)
        {
            codeValue.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<AudioSource>().Play();
            _activated = true;


        }
    }
}
