using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public List<GameObject> Slot = new List<GameObject>();
    List<GameObject> items = new List<GameObject>();

    public GameObject SlotItems;

    public Sprite dirtSprite;
    public Sprite grassSprite;
    public Sprite waterSprite;




    // Start is called before the first frame update
    // 인벤토리 업데이트 시 호출
    
    public void UpdateInventory(Inventory myInven)
    {
        // 1. 기존 슬롯 초기화
        foreach(var slotItems in items)
        {
            Destroy(slotItems);
        }
        items.Clear();
        int idx = 0;

        // 2. 내 인벤토리 데이터를 전체 탐색
        foreach (var item in myInven.items)
        {
            var go = Instantiate(SlotItems, Slot[idx].transform);
            go.transform.localPosition = Vector3.zero;
            SlotItemPrefab sitem = go.GetComponent<SlotItemPrefab>();
            items.Add(go);
            switch (item.Key)
            {
                case BlockType.Dirt:
                    sitem.ItemSetting(dirtSprite,item.Value.ToString());
                    // Dirt 아이템을 슬롯에 생성
                    // Instantiate 활용
                    break;
                case BlockType.Grass:
                    break;
                case BlockType.Water:
                    break;
            }
            idx++;

        }

    }
}
