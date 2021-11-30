using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePlace : MonoBehaviour
{
    [SerializeField]
    public string codeKey;
    
    public CodeValue codeAttached;
    
    private GameManager gameManager;

    public void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    
    public CodeValue GetCodeAttached()
    {
        return codeAttached;
    }
    
    public void SetCodeAttached(CodeValue codeValue)
    {
        codeAttached = codeValue;
        UpdateValue();
    }

    public void UpdateValue()
    {
        string keyCodeValue = codeAttached ? codeAttached.GetValue() : "0";
        gameManager.UpdateCodeKeyValue(codeKey, keyCodeValue);
    }
}
