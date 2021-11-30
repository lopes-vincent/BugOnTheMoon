using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class OscillateLightBrightness : MonoBehaviour
{
    Light2D lightComponent;
    [SerializeField, Range(0f, 10f)]
    float lower;

    [SerializeField, Range(0f, 10f)]
    float upper;
    // Start is called before the first frame update
    void Start()
    {
        lightComponent = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lightComponent.intensity = Random.Range(lower, upper);
    }
}
