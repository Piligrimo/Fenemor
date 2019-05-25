using UnityEngine;
using System.Collections;

public class CursorCntrl : MonoBehaviour {
    public Texture2D light;
    public Texture2D dflt;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Enter()
    {
        Cursor.SetCursor(light, new Vector2(0.33f, 0.15f), CursorMode.Auto);
    }
    public void Exit()
    {
        Cursor.SetCursor(dflt, new Vector2(0.33f, 0.15f), CursorMode.Auto);
    }
}
