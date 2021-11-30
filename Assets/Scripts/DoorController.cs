using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator _animator;

    private bool _open;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        if (!_open)
        {
            _animator.SetTrigger("Open");
            _open = true;
        }
    }

    public void Close()
    {
        if (_open)
        {
            _animator.SetTrigger("Close");
            _open = false;
        }
    }
}
