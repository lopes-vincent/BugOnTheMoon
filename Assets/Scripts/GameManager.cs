using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _codeCanvas; 
    
    [SerializeField]
    private GameObject _pauseCanvas;    

    [SerializeField]
    private GameObject _codeWindow;    
    
    [SerializeField]
    private Texture2D _cursorHover;

    
    private float _timeElapsed = 0f;
    
    [SerializeField]
    private GameObject _finishGameCanvas;
    
    [SerializeField]
    private Text _endGameText;

    public Dictionary<string, string> _codeValues = new Dictionary<string, string>
    {
        {"jumpForce", "0"},
        {"canJump", "false"},
        {"rightMove", "0"},
        {"leftMove", "0"},
        {"rightSpeed", "1"},
        {"leftSpeed", "1"},
        {"size", "1"},
        {"leftKey", "Left"},
        {"rightKey", "Right"},
    };

    public void Restart()
    {
        TogglePause();
        SceneManager.LoadScene("Game");
    }

    public void TogglePause()
    {
        _pauseCanvas.SetActive(!_pauseCanvas.activeSelf);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public float GetCodeValueAsFloat(string key)
    {
        string value = _codeValues[key];
        return float.Parse(value);
    }
    public string GetCodeValue(string key)
    {
        return _codeValues[key];
    }

    // Start is called before the first frame update
    void Start()
    {
        Text[] codes = _codeWindow.GetComponentsInChildren<Text>(true);
        foreach (Text code in codes)
        {
            code.text = ColorCode(code.text);
        }
    }

    public static string ColorCode(string code)
    {
        string coloredCode = code.Replace("class", "<color=#ff79c6>class</color>");
        coloredCode = coloredCode.Replace("property", "<color=#ffb86c>property</color>");
        coloredCode = coloredCode.Replace("this", "<color=#bd93f9>this</color>");
        coloredCode = coloredCode.Replace("function", "<color=#ff79c6>function</color>");
        coloredCode = coloredCode.Replace("everyFrame", "<color=#50fa7b>everyFrame</color>");
            
        coloredCode = coloredCode.Replace(" = ", "<color=#8be9fd> = </color>");
        coloredCode = coloredCode.Replace(" == ", "<color=#8be9fd> == </color>");
        coloredCode = coloredCode.Replace(" + ", "<color=#8be9fd> + </color>");
        coloredCode = coloredCode.Replace(" - ", "<color=#8be9fd> - </color>");
        coloredCode = coloredCode.Replace(" && ", "<color=#8be9fd> && </color>");

        return coloredCode;
    }

    void Update()
    {
        _timeElapsed += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.H))
        {
            _codeCanvas.SetActive(!_codeCanvas.activeSelf );
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.TogglePause();
        }
    }

    public void Finish()
    {
        _finishGameCanvas.SetActive(true);
        _endGameText.text = "<color=#ff79c6>";
        float hours = ((_timeElapsed / 60)/60);
        if (hours > .5f)
        {
            string suffix = hours > 1 ? "s " : " ";
            _endGameText.text += hours.ToString("00") + " hour" + suffix;
        }
        float minutes = ((_timeElapsed / 60)%60);
        if (minutes > .5f)
        {
            string suffix = minutes > 1 ? "s " : " ";
            _endGameText.text += minutes.ToString("00") + " minute" + suffix;
        }
        float seconds = ((_timeElapsed)%60);
        if (seconds > .5f)
        {
            string suffix = seconds > 1 ? "s " : " ";
            _endGameText.text += seconds.ToString("00") + " second" + suffix;
        }
        _endGameText.text += "</color>";
    }

    public void UpdateCodeKeyValue(string key, string value)
    {
        _codeValues[key] = value;
    }
    
    public void CursorHover()
    {
        Cursor.SetCursor(_cursorHover, new Vector2(10f, 10f), CursorMode.Auto);
    }

    public void CursorExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
