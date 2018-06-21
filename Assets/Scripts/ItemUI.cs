using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    
    private string m_Name;
    private int m_ItemID;
    private Image m_Image;
    private GameObject m_DatabaseItem;

    // Use this for initialization
    void Start()
    {
        m_Image = GetComponent<Image>();
        
    }

    public void SetItem(GameObject pItem)
    {
        #region SetItemMembers
        Item item = pItem.GetComponent<Item>();
        m_DatabaseItem = pItem;
        m_Name = item.m_ItemName;
        m_ItemID = item.m_ID;
        m_Image.sprite = item.m_ItemTexture;
        m_DatabaseItem = pItem;
        #endregion
    }

    public void ClearItem()
    {
        m_Name = null;
        m_ItemID = 0;
        m_Image.sprite = null;
        m_DatabaseItem = null;
    }

    public void SetImage(Sprite pSprite)
    {
        m_Image.sprite = pSprite;
    }

    public GameObject GetRootItem()
    {
        return m_DatabaseItem;
    }

    public int GetID()
    {
        return m_ItemID;
    }


}

