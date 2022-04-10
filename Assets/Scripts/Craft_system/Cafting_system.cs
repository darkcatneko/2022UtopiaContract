using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cafting_system : MonoBehaviour
{
    public InventoryObject Crafting_slot;
    public ItemObject[] Crafting_result;
    public GameObject result_button;

    //物品回傳包包
    bool can_recieve = false;
    ItemObject final_result;
    public GameObject player;
    public void CraftButtonClicked()
    {
        var slot1 = Crafting_slot.Container[0];
        var slot2 = Crafting_slot.Container[1];
        if (slot1 != null && slot2 != null )//速度藥水
        {
            switch(slot1.ID, slot2.ID)
            {
                case (0, 4):
                    craft_check(Crafting_result[0]);            
                    return;
                    
            }
        }
    }
    public void craft_check(ItemObject result)
    {
        var slot1 = Crafting_slot.Container[0];
        var slot2 = Crafting_slot.Container[1];
        if (slot1.amount >= 1 && slot2.amount >= 1)
        {
            slot1.amount--;
            slot2.amount--;
            if (slot1.amount == 0)
            {
                Crafting_slot.RemoveItem(slot1.item);
            }
            if (slot2.amount == 0)
            {
                Crafting_slot.RemoveItem(slot2.item);
            }
            result_button.GetComponentsInChildren<Image>()[1].sprite = result.sprite;
            result_button.GetComponentsInChildren<Image>()[1].color = new Color(1, 1, 1, 1);
            final_result = result;
            can_recieve = true;
        }
    }
    public void recieve_button()
    {
        if (can_recieve == true)
        {
            player.gameObject.GetComponentInParent<PlayerBackPack>().AddItemInBackPack(final_result,1);
            final_result = null;
            can_recieve = false;
            result_button.GetComponentsInChildren<Image>()[1].sprite = null;
            result_button.GetComponentsInChildren<Image>()[1].color = new Color(1, 1, 1, 0);
        }
    }
}

