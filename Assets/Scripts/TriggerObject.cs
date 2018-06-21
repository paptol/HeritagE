using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : InteractObject {


    private bool m_IsTriggered;
    private Animator m_Anim;
    public GameObject m_Door;
    private TriggerDoor m_TriggerDoor;
    private GameObject m_LooseLever;
    public bool m_MissingTrigger;

    private void Start()
    {
        m_Anim = GetComponent<Animator>();
        m_TriggerDoor = m_Door.GetComponent<TriggerDoor>();
        m_LooseLever = transform.Find("LeverPivot").gameObject.transform.Find("Lever").gameObject;
    }

    // Update is called once per frame
    void Update ()
    {
        m_Anim.SetBool("Trigger", m_IsTriggered);
        m_LooseLever.SetActive(m_MissingTrigger ? false : true); //If the trigger missing is true then it is not active
	}

    public override void Interact()
    {
        InventorySystem inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventorySystem>();
        if(m_MissingTrigger == true)
        {
            if(inventory.FindItemAndRemove(1))
            {
                m_MissingTrigger = false;
            }
        }
        else if(m_MissingTrigger == false)
        {
            m_IsTriggered = !m_IsTriggered;
            m_TriggerDoor.StartAnimCoroutine();
            NavigationArrow.Target = GameObject.Find("Door 1").transform.position + new Vector3(1,0,0);
        }
        
    }

    public bool GetTrigger()
    {
        return m_IsTriggered;
    }
}
