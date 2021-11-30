using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D _cursorHover;
    
    public void CursorHover()
    {
        Cursor.SetCursor(_cursorHover, new Vector2(10f, 10f), CursorMode.Auto);
    }

    public void CursorExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
