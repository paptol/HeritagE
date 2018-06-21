using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    private ItemUI m_ItemUI;
    public int m_IntSlot;
    private bool m_IsVacant;

    private void Start()
    {
        m_ItemUI = GetComponentInChildren<ItemUI>();
        m_IsVacant = true;
    }

    public ItemUI GetSlotItem()
    {
        return m_ItemUI;
    }

    public void SetVacancy(bool pBool)
    {
        m_IsVacant = pBool;
    }

    public bool IsSlotVacant()
    {
        return m_IsVacant;
    }

    public void SetSlotItem(ItemUI pItemUI)
    {
        m_ItemUI = pItemUI;
    }

    
}
