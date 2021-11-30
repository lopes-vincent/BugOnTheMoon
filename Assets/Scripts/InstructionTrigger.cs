using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject _instructionsCanvas;
    
    [SerializeField]
    private string _conditionalKey;

    [SerializeField]
    private string _conditionalValue;
    
    [SerializeField]
    private string _equalConditionalValue;

    private IEnumerator closeCoroutine;

    private GameManager gameManager;

    public void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (
            ("" != _conditionalValue && gameManager.GetCodeValue(_conditionalKey) != _conditionalValue)
            ||
            ("" != _equalConditionalValue && gameManager.GetCodeValue(_conditionalKey) == _equalConditionalValue)
            )
        {
            return;
        }

        if (null != closeCoroutine) {
            StopCoroutine(closeCoroutine);
            closeCoroutine = null;
        }
        _instructionsCanvas.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        closeCoroutine = HideInstructions();
        StartCoroutine(closeCoroutine);
    }
    
    IEnumerator HideInstructions()
    {
        yield return new WaitForSeconds(1f);
        _instructionsCanvas.SetActive(false);
    }
}
