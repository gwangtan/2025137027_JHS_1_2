using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    public List<GameObject> slotParent = new List<GameObject>();
    List<GameObject> items = new List<GameObject>();

    public GameObject SlotItem;

    public Sprite dirtSprite;
    public Sprite grassSprite;
    public Sprite waterSprite;

    public int selectedIndex = -1;


    private void Update()
    {
        for (int i = 0; i<Mathf.Min(9, slotParent.Count); i++)
        {
            if (Input .GetKeyDown(KeyCode.Alpha1 + i))
            {
                SetSelectedIndex(i);
            }
        }
    }

    public  void SetSelectedIndex(int idx)
    {
        ResetSelection();
        if (selectedIndex == idx)
        {
            selectedIndex = -1;
        }
        else
        {
            if (idx >= items.Count)
            {
                selectedIndex = -1;
            }
            else
            {
                SetSelection(idx);
                selectedIndex = idx;
            }
        }
    }

    public void ResetSelection()
    {
        foreach(var slot in slotParent)
        {
            slot.GetComponent<Image>().color = Color.white;
        }

    }

    void SetSelection(int _idx)
    {
        slotParent[_idx].GetComponent<Image>().color = Color.yellow;
    }

    public BlockType GetInventorySlot()
    {
        return items[selectedIndex].GetComponent<SlotItemPrefab>().blockType;
    }
    
    public void UpdateInventory(Inventory myInven)
    {
        
        foreach(var slotItems in items)
        {
            Destroy(slotItems);
        }
        items.Clear();
        int myIdx = 0;        
        foreach (var item in myInven.items)
        {
            var go = Instantiate(SlotItem);
            go.transform.SetParent(slotParent[myIdx].gameObject.transform);
            go.transform.localPosition = Vector3.zero;

            SlotItemPrefab sitem = go.GetComponent<SlotItemPrefab>();
            items.Add(go);

            switch (item.Key)
            {
                case BlockType.Dirt:
                    sitem.ItemSetting(dirtSprite,"x" + item.Value.ToString(), item.Key);
                    
                    break;
                case BlockType.Grass:
                    sitem.ItemSetting(grassSprite, "x" + item.Value.ToString(), item.Key);
                    break;
                case BlockType.Water:
                    sitem.ItemSetting(waterSprite, "x" + item.Value.ToString(), item.Key);
                    break;
            }
            myIdx++;

        }

    }
}
