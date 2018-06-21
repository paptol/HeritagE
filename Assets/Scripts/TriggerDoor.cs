using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    public GameObject m_TriggerObject;
    private TriggerObject m_TriggerScript;
    private Animator m_Anim;

	// Use this for initialization
	void Start ()
    {
        m_TriggerScript = m_TriggerObject.GetComponent<TriggerObject>();
        m_Anim = GetComponent<Animator>();
	}
	
    public void StartAnimCoroutine()
    {
        StartCoroutine("SetAnim");
    }

    private IEnumerator SetAnim()
    {
        yield return new WaitForSeconds(1);
        m_Anim.SetBool("Trigger", m_TriggerScript.GetTrigger());
    }
}
