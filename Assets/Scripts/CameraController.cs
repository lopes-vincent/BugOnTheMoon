using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    
    [SerializeField]
    private Vector3 _offset;
    
    [SerializeField] [Range(0.01f, 2f)]
    private float _smoothSpeed = 0.5f;

    private Transform _transform;
    private Vector3 _velocity = Vector3.zero;

    private Vector3 _initialPosition;
    private float _shakeDuration = 0f;

    void Awake()
    {
        _transform = GetComponent<Transform>();
        _initialPosition = _transform.position;
    }

    public void TriggerShake(float shakeDuration = 2.0f)
    {
        StartCoroutine(Shake(shakeDuration));
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = _player.transform.position + _offset;
        // _transform.position = desiredPosition;
        _transform.position = Vector3.SmoothDamp(_transform.position, desiredPosition, ref _velocity, _smoothSpeed);
    }
    
    
    IEnumerator Shake(float duration)
    {
        float force = 1f;
        float magnitude = 0.5f;
        float elapsed = 0.0f;


        Vector3 originalCamPos =new Vector3(_player.transform.position.x,_player.transform.position.y,Camera.main.transform.position.z);

        while (elapsed < duration) {

            force = Mathf.Clamp(force - 0.5f*elapsed, 0f, 1f);
            
            elapsed += Time.deltaTime;

            float calPerc= elapsed / duration;
            float zigzag= 1.0f - Mathf.Clamp(4.0f * calPerc- 3.0f, 0.0f, 1.0f);

            

// camera position near about player transform position

            float FX = Random.Range (-1.0f, 1.0f);
            float x = Random.Range (_player.transform.position.x,_player.transform.position.x+FX);

            float FY=Random.Range (-1.0f, 1.0f);
            float y = Random.Range (_player.transform.position.y,_player.transform.position.y + FY);

            x += magnitude + zigzag * force;
            y += magnitude + zigzag * force;

            Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);

            yield return null;
        }

        Camera.main.transform.position = new Vector3(_player.transform.position.x,_player.transform.position.y,Camera.main.transform.position.z);


    } 
}
