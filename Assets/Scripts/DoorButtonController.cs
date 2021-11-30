using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonController : MonoBehaviour
{
    [SerializeField] private DoorController _door;
    [SerializeField ] private float _doorTimer = 1f;
    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _closeSprite;
    [SerializeField] private Sprite _openSprite;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _spriteRenderer.sprite = _openSprite;
            _door.Open();
        }
    }    
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_doorTimer > 0f)
            {
                StartCoroutine(CloseDoor());
            }
        }
    }

    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(_doorTimer);
        _spriteRenderer.sprite = _closeSprite;
        _door.Close();
    }
}
