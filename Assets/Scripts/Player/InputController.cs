using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InputController : MonoBehaviour {

    private static float m_InputAxisX;
    private static float m_InputAxisY;
    private static bool m_IsShiftDown;

    public enum enum_GameState
    {
        Paused,
        Dialogue,
        Playing
    }

    static public enum_GameState m_GameState;

    // Use this for initialization
    void Start ()
    {
        m_GameState = enum_GameState.Playing;
        m_IsShiftDown = false;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(m_GameState == enum_GameState.Playing)
        {
            m_InputAxisX = Input.GetAxis("Horizontal");
            m_InputAxisY = Input.GetAxis("Vertical");

            if(Input.GetButton("Run"))
            {
                m_IsShiftDown = true;
            }
            else
            {
                m_IsShiftDown = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (m_GameState == enum_GameState.Paused)
            {
                m_GameState = enum_GameState.Playing;
                Cursor.lockState = CursorLockMode.Locked;

            }
            else if (m_GameState == enum_GameState.Playing)
            {
                m_GameState = enum_GameState.Paused;
                Cursor.lockState = CursorLockMode.None;
            }
        }

    }

    static public Vector2 GetAxis()
    {
        return new Vector2(m_InputAxisX, m_InputAxisY);
    }

    static public bool IsShiftDown()
    {
        return m_IsShiftDown;
    }

    public enum_GameState GetState()
    {
        return m_GameState;
    }
}
