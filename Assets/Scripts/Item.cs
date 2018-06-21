using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public Sprite m_ItemTexture;
    [HideInInspector]
    public GameObject m_Item;
    public string m_ItemName;
    public int m_ID;


    // Use this for initialization
    void Start ()
    {
        m_Item = gameObject;
	}
}
