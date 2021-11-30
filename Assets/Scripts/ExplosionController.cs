using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ExplosionController : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;
    
    [SerializeField]
    private GameObject _fireContainer;

    [SerializeField]
    private GameObject _barilContainer;    
    
    [SerializeField]
    private GameObject _codeValueContainer;
    
    [SerializeField]
    private CameraController _camera;

    private bool _activated;
    
    private Dictionary<string, string> _nullCodeValues = new Dictionary<string, string>
    {
        {"jumpForce", "0"},
        {"canJump", "false"},
        {"rightMove", "0"},
        {"leftMove", "0"},
        {"rightSpeed", "0"},
        {"leftSpeed", "0"},
        {"size", "1"},
        {"leftKey", ""},
        {"rightKey", ""},
    };

    public void StopAlert()
    {
        foreach (AudioSource audio in  gameObject.GetComponentsInChildren<AudioSource>())
        {
            audio.Stop();
        }
        
        GameObject[] alerts = GameObject.FindGameObjectsWithTag("Alert");
        foreach (GameObject alert in alerts)
        {
            alert.GetComponent<Light2D>().enabled = false;
        }
        
        foreach (Transform fire in _fireContainer.transform)
        {
            fire.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_activated && other.CompareTag("Player"))
        {
            foreach (AudioSource audio in  gameObject.GetComponentsInChildren<AudioSource>())
            {
                audio.Play();
            }

            _gameManager._codeValues = _nullCodeValues;

            _activated = true;
            other.GetComponent<PlayerController>().lastCheckpointPosition = transform;

            foreach (Transform fire in _fireContainer.transform)
            {
                fire.gameObject.SetActive(true);
            }

            GameObject[] alerts = GameObject.FindGameObjectsWithTag("Alert");
            foreach (GameObject alert in alerts)
            {
                alert.GetComponent<Light2D>().enabled = true;
            }
            
            foreach (Transform _baril in _barilContainer.transform)
            {
                _baril.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(500,500));
            }

            CodeValue[] _codeValues = GameObject.FindObjectsOfType<CodeValue>();
            foreach (CodeValue codeValue in _codeValues)
            {
                if (null != codeValue.target)
                {
                    codeValue.transform.SetParent(_codeValueContainer.transform);
                    codeValue.target.GetComponent<CodePlace>().SetCodeAttached(null);
                    codeValue.target = null;
                    codeValue.codeRigidbody.AddForce(new Vector2(5000000, 100000));
                }
            }
            
            other.GetComponent<PlayerController>().Bug();

            
            
            _camera.TriggerShake(3f);
        }
    }
}
