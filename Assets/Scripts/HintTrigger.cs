using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintTrigger : MonoBehaviour {

    public GameObject   m_HintPanel;

	// Use this for initialization
	void Start ()
    {
        m_HintPanel.SetActive(false);
	}
	

    void OnTriggerEnter(Collider other)
    {
        m_HintPanel.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        m_HintPanel.SetActive(false);
    }
}
