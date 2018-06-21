using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractHint : InteractObject{



    public GameObject m_HintPanel;
    
    public string m_StringHint;
    private Text m_TextBox;

    // Use this for initialization
    void Start ()
    {
        m_TextBox = m_HintPanel.GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

}
