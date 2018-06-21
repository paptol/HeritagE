using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour {

    private ItemDatabase m_ItemDatabase;
    private InputController m_Input;
    public GameObject m_Player;
    public ItemSlot[] m_ItemSlotList = new ItemSlot[16];

    private void Start()
    {
        m_Input = m_Player.GetComponent<InputController>();

        m_ItemDatabase = GameObject.FindGameObjectWithTag("Database").GetComponent<ItemDatabase>();

        GameObject[] itemSlots = GameObject.FindGameObjectsWithTag("ItemSlot");
        for(int i = 0; i < m_ItemSlotList.Length; i++)
        {
            for(int j = 0; j < itemSlots.Length; j++)
            {
                ItemSlot item = itemSlots[j].GetComponent<ItemSlot>();
                if(item.m_IntSlot == i)
                {
                    m_ItemSlotList[i] = item;
                }
            }
        }

    }

    public bool FindItemAndRemove(int pID)
    {
        for (int i = 0; i < m_ItemSlotList.Length; i++)
        {
            if(m_ItemSlotList[i].GetSlotItem().GetID() == pID)
            {
                m_ItemSlotList[i].GetSlotItem().ClearItem();
                return true;
            }          
        }
        return false;
    }

    public void AddItem(GameObject pItem)
    {
        Item item = pItem.GetComponent<Item>();
        for (int i = 0; i < m_ItemSlotList.Length; i++)
        {
                if (m_ItemSlotList[i].IsSlotVacant())
                {
                    m_ItemSlotList[i].GetSlotItem().SetItem(FindItemInDatabase(item));
                    m_ItemSlotList[i].SetVacancy(false);
                    Destroy(item.m_Item);
                    return;
                }
        }
    }    

    public ItemSlot[] GetSlotArray()
    {
        return m_ItemSlotList;
    }

    public GameObject FindItemInDatabase(Item pItem)
    {
        foreach(GameObject item in m_ItemDatabase.m_ItemDataList)
        {
            Item itemScript = item.GetComponent<Item>();
            if(pItem.m_ID == itemScript.m_ID)
            {
                return item;
            }
        }
        return null;
    }

}
