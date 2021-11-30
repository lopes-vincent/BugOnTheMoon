using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;
    
    [SerializeField]
    private GameObject _foot; 
    [SerializeField]
    private GameObject _bug; 
    
    [SerializeField]
    private Animator _fade;
    
    [SerializeField]
    private AudioSource jumpAudioSource;
    
    public Transform lastCheckpointPosition;
    
    private Rigidbody2D _rigidbody2D;
    private Transform _transform;
    private float _horizontalMovement;
    private Animator _animator;
    private float _flip = 1;
    
    public bool _blockMovement = false;

    private bool _onAir = false;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_blockMovement)
        {
            return;
        }

        float horizontalValue = Input.GetAxis("Horizontal");
        _horizontalMovement = horizontalValue * 5;
        
        if (horizontalValue > 0)
        {
            if (_gameManager.GetCodeValue("rightKey") == "Left")
            {
                _horizontalMovement = _horizontalMovement * -1;
            }
            else if (_gameManager.GetCodeValue("rightKey") != "Right")
            {
                _horizontalMovement = 0;
            }
        }
        if (horizontalValue < 0)
        {
            if (_gameManager.GetCodeValue("leftKey") == "Right")
            {
                _horizontalMovement = _horizontalMovement * -1;
            }
            else if (_gameManager.GetCodeValue("leftKey") != "Left")
            {
                _horizontalMovement = 0;
            }
        }
        
        _animator.SetBool("Move", _horizontalMovement != 0f);

        bool wasOnAir = _onAir;
        string[] layerToCheck = {"Ground"};
        RaycastHit2D hit2D = Physics2D.CircleCast(_foot.transform.position, 0.2f, Vector2.down, 0.1f, LayerMask.GetMask(layerToCheck));
        _onAir = hit2D.collider == null;
        if (!wasOnAir && _onAir)
        {
            jumpAudioSource.Play();
        } else if (wasOnAir && !_onAir)
        {
            jumpAudioSource.Stop();
        }
        
        if (Input.GetButtonDown("Jump") && _gameManager.GetCodeValue("canJump") == "true")
        {
            if (!_onAir)
            {
                _rigidbody2D.AddForce(new Vector2(0, _gameManager.GetCodeValueAsFloat("jumpForce")), ForceMode2D.Impulse);
            }
        }

        _animator.SetBool("Fly", _onAir);
    }

    public void Bug()
    {
        StartCoroutine(doBug());
    }
    
    IEnumerator doBug()
    {
        _bug.SetActive(true);
        yield return new WaitForSeconds(1f);
        _bug.SetActive(false);
    }
    
    public void FixedUpdate()
    {
        if (_blockMovement)
        {
            return;
        }

        // Movement
        float speed = _horizontalMovement > 0
            ? _gameManager.GetCodeValueAsFloat("rightSpeed")
            : _gameManager.GetCodeValueAsFloat("leftSpeed");
    
        speed = speed * 1.2f;

        if (_onAir)
        {
            speed = Mathf.Clamp(speed, 0, 10);
        }
        

        float movement = _horizontalMovement != 0
            ? _horizontalMovement * speed
            : _gameManager.GetCodeValueAsFloat("rightMove") - _gameManager.GetCodeValueAsFloat("leftMove");

        _rigidbody2D.velocity = new Vector2(movement, _rigidbody2D.velocity.y);


        // Scale
        float sizeValue = _gameManager.GetCodeValueAsFloat("size");
        if (sizeValue < 0.25)
        {
            sizeValue = 0.25f;
        }
        Vector2 newScale = new Vector2(sizeValue, sizeValue);
        if (_horizontalMovement != 0)
        {
            _flip = _horizontalMovement <= 0 ? -1 : 1;
        }
        newScale.x = Mathf.Abs(newScale.x) * _flip;

        _transform.localScale = newScale;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Death") && !_blockMovement)
        {
            Death();
        }
    }

    public void Death()
    {
        jumpAudioSource.Stop();
        _blockMovement = true;
        _horizontalMovement = 0;
        _rigidbody2D.velocity =  new Vector2(0, 0);
        _rigidbody2D.gravityScale = 0;
        _animator.SetTrigger("Boom");
        StartCoroutine(doDeath());
    }

    IEnumerator doDeath()
    {
        yield return new WaitForSeconds(.5f);
        _fade.SetTrigger("Fade");
        yield return new WaitForSeconds(1f);
        _animator.SetTrigger("Revive");
        _rigidbody2D.position = lastCheckpointPosition.position;
        _rigidbody2D.gravityScale = 0.8f;

        _blockMovement = false;
    }
}
