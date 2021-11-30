using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyController : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    
    [SerializeField]
    private float _xOffset;    
    [SerializeField]
    private float _yOffset;
    

    private Transform _transform;
    private Vector3 _velocity = Vector3.zero;

    
    void Awake()
    {
        _transform = GetComponent<Transform>();
    }
    
    void LateUpdate()
    {
        Vector3 desiredPosition = new Vector2(_player.transform.position.x * 0.5f + _xOffset, _player.transform.position.y * 0.5f + _yOffset);
        _transform.position = desiredPosition;
    }
}
