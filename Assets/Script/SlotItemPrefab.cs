using TMPro;
using UnityEngine;
using UnityEngine.UI;

// @Unity 스크립트(자산 참조 1개)
public class SlotItemPrefab : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI itemText;

    public void ItemSetting(Sprite itemSprite, string txt)
    {
        itemImage.sprite = itemSprite;
        itemText.text = txt;

    }
}