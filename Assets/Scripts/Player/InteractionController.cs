using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    private InputController m_Input;

    private Image m_UI;
    public Canvas m_Canvas;
    public Transform m_Eyes;
    public LayerMask m_ObjectMask;
    public Sprite m_Hand;
    public Sprite m_Dot;
    public Sprite m_Eye;
    public float m_InteractionRange;

    RaycastHit m_Hit;


    // Use this for initialization
    void Start ()
    {
        m_UI = m_Canvas.GetComponentInChildren<Image>();
        m_Input = GetComponent<InputController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_UI.sprite = m_Dot;
        if(m_Input.GetState() == InputController.enum_GameState.Playing)
        {
            m_UI.enabled = true;

            if (Physics.Raycast(m_Eyes.position, m_Eyes.forward, out m_Hit, m_InteractionRange, m_ObjectMask))
            {
                if (m_Hit.collider.CompareTag("Interactable") || m_Hit.collider.CompareTag("Item"))
                {
                    m_UI.sprite = m_Hand;
                }
                if (m_Hit.collider.CompareTag("Interest"))
                {
                    m_UI.sprite = m_Eye;
                }
                if (Input.GetButtonDown("Interact"))
                {
                    Interact(m_Hit.collider.gameObject);
                }
            }
        }
        else
        {
            m_UI.enabled = false;
        }       
	}

    private void Interact(GameObject pInteractedObject)
    {

            if (pInteractedObject.CompareTag("Interactable"))
            {
                InteractObject interactableObject = m_Hit.collider.gameObject.GetComponent<InteractObject>();
                interactableObject.Interact();
            }
            else if (pInteractedObject.CompareTag("Item"))
            {
                InventorySystem inventorySystem = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventorySystem>();
                inventorySystem.AddItem(pInteractedObject);

                NavigationArrow.Target = GameObject.Find("TriggerButton").transform.position;
        }

        /*catch
        {
            Debug.Log("Nothing to Press");
        }*/
    }
}
