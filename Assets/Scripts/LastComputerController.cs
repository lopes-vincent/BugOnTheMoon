using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LastComputerController : MonoBehaviour
{
    public GameObject SpriteLightContainer;
    public Sprite greenSprite;
    public ExplosionController explosionController;
    
    
    public GameObject RoomLightContainer;
    public GameManager _gameManager;
    public SpriteRenderer _spriteRenderer;
    public Sprite _finishSprite;

    private bool finish = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!finish)
        {
            _gameManager.Finish();

            foreach (Light2D light in gameObject.GetComponentsInChildren<Light2D>())
            {
                light.color = Color.green;
            }

            _spriteRenderer.sprite = _finishSprite;
            Light2D[] RoomLights = RoomLightContainer.GetComponentsInChildren<Light2D>();
            foreach (Light2D light in RoomLights)
            {
                light.enabled = true;
            }
            
            SpriteRenderer[] spriteLights = SpriteLightContainer.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer spriteLight in spriteLights)
            {
                spriteLight.sprite = greenSprite;
            }
            
            explosionController.StopAlert();
            
            gameObject.GetComponent<AudioSource>().Play();

            finish = true;
        }
    }
}
