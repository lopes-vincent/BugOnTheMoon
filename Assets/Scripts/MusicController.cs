using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource _source;
    
    [SerializeField]
    private AudioClip _loop;
    
    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_source.isPlaying)
        {
            _source.clip = _loop;
            _source.loop = true;
            _source.Play();
        }
    }
}
