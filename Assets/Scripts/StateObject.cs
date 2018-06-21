using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateObject : InteractObject
{

    public List<bool> m_StateList; //Must have atleast 2 States to count as an interactive object, put the right state true
    public Vector3 m_RotateAround;
    private int m_CurrentState;
    private bool m_IsCorrect;

    // Use this for initialization
    void Start()
    {
        if (m_StateList.Count < 2)
        {
            Debug.Log("Remember to give the Object 2 or more States!");
        }
    }

    private void Update()
    {
        if (m_StateList[m_CurrentState] == true)
        {
            m_IsCorrect = true;
        }
        else
        {
            m_IsCorrect = false;
        }
        Debug.Log(m_CurrentState);
    }

    public bool IsCorrect()
    {
        return m_IsCorrect;
    }

    public override void Interact()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = transform.rotation * Quaternion.Euler(m_RotateAround);
        if (m_CurrentState != m_StateList.Count - 1)
        {
            m_CurrentState++;
        }
        else
        {
            m_CurrentState = 0;
        }

        transform.rotation = Quaternion.Lerp(startRotation, endRotation, 1f);
    }

}
