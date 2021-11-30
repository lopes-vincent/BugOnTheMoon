using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CodeValue : MonoBehaviour
{
    [SerializeField]
    private string placeTag;
    
    private Boolean drag = false;

    public Rigidbody2D codeRigidbody;
    Transform transform;

    public GameObject target = null;
    
    private Transform baseParent;
    
    private Vector3 currentPosition;
    private Vector3 previousPosition;
    
    private Vector3 _velocity;
    private bool _underInertia;
    private float _time = 0.0f;
    private float SmoothTime = 1f;
    

    void Start()
    {
        codeRigidbody = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        baseParent = transform.parent;
    }

    void Update()
    {
        if(_underInertia && _time <= SmoothTime && _velocity != Vector3.zero)
        {
            codeRigidbody.MovePosition(transform.position + _velocity);

            // transform.position += _velocity;
            float interpolationRatio = (float)_time / SmoothTime;

            _velocity = Vector3.Lerp(_velocity, Vector3.zero, interpolationRatio);
            _time += Time.smoothDeltaTime;
        }
        else
        {
            _underInertia = false;
            _time = 0.0f;
        }
    }

    void FixedUpdate()
    {
        move();
    }

    public string GetValue()
    {
       return gameObject.GetComponentInChildren<Text>().text;
    }

    private void move()
    {
        if (drag)
        {
            previousPosition = currentPosition;
            currentPosition = transform.position;
            _velocity = currentPosition - previousPosition;
            codeRigidbody.MovePosition(Input.mousePosition);
            return;
        }
        if (null != target)
        {
            codeRigidbody.MovePosition(target.transform.position);
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(placeTag))
        {
            CodePlace codePlace = other.GetComponent<CodePlace>();
            if (null == codePlace.GetCodeAttached())
            {
                if (null != target)
                {
                    target.GetComponent<CodePlace>().SetCodeAttached(null);
                }
                
                codePlace.SetCodeAttached(this);
                transform.SetParent(other.gameObject.transform.parent);
                target = other.gameObject;
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(placeTag) && null != target && transform.parent.gameObject.activeInHierarchy)
        {
            transform.SetParent(baseParent);
            target.GetComponent<CodePlace>().SetCodeAttached(null);
            if (other.GetComponent<CodePlace>().GetCodeAttached() == this)
            {
                other.GetComponent<CodePlace>().SetCodeAttached(null);
            }
            target = null;
        }
    }


    public void OnMouseDrag()
    {            
        drag = true;
    }


    public void EndDrag()
    {
        drag = false;
        _underInertia = true;
    }
}
