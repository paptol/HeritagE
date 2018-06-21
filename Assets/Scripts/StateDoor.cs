using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDoor : MonoBehaviour
{
    public List<GameObject> m_InteractObjectList;
    private List<bool> m_TriggerList;
    private Animator m_Animator;
    private bool m_IsUnlocked;
    public bool m_LockButtonsIfCorrect;
    

	// Use this for initialization
	void Start ()
    {
        m_TriggerList = new List<bool>();
        m_Animator = GetComponent<Animator>();
        for(int i = 0; i < m_InteractObjectList.Count; i++)
        {
            m_TriggerList.Add(false);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        int m_Counter = 0;
		foreach(GameObject gameObject in m_InteractObjectList)
        {
            
            StateObject intObject = gameObject.GetComponent<StateObject>();
            if(intObject.IsCorrect() == true)
            {
                m_TriggerList[m_Counter] = true;
            }
            else
            {
                m_TriggerList[m_Counter] = false;
            }
            m_Counter++;
        }
        if(IsAllTrue())
        {                      
            if(m_LockButtonsIfCorrect == true)
            {
                foreach (GameObject gameObject in m_InteractObjectList)
                {
                    InteractObject intObject = gameObject.GetComponent<InteractObject>();
                    Destroy(intObject);
                }
                Destroy(this);
            }
            
        }


        m_Animator.SetBool("Trigger", IsAllTrue());
    }

    public bool IsAllTrue()
    {
        for (int i = 0; i < m_InteractObjectList.Count; i++)
        {
            if(m_TriggerList[i] != true)
            {
                return false;
            }

        }
        return true;
    }
}
