using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CheckpointController : MonoBehaviour
{
    public GameObject RoomLightContainer;
    public Light2D CheckpointLight;
    public Sprite SpriteOn;
    
    private bool _isActive;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isActive && other.CompareTag("Player"))
        {
            _isActive = true;
            other.GetComponent<PlayerController>().lastCheckpointPosition = transform;

            Light2D[] RoomLights = RoomLightContainer.GetComponentsInChildren<Light2D>();
            foreach (Light2D light in RoomLights)
            {
                light.enabled = true;
            }

            gameObject.GetComponent<SpriteRenderer>().sprite = SpriteOn;
            gameObject.GetComponent<AudioSource>().Play();
            CheckpointLight.color = Color.green;
        }
    }
}
