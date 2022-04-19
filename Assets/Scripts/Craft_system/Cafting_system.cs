using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cafting_system : MonoBehaviour
{
    GameObject decribe_block;

    public InventoryObject Crafting_slot;
    public ItemObject[] Crafting_result;
    public GameObject result_button;
    //顯示出確認視窗
    public GameObject Result_big;
    public GameObject result_screen;//小方格
    public GameObject first_step;
    public GameObject second_step;
    private string named_temp;
    public InputField input;
    public Text the_name_of_it;

    //物品回傳包包
    bool can_recieve = false;
    ItemObject final_result;
    public InventoryObject player;
    private void Start()
    {
        AddEvent(result_screen, EventTriggerType.PointerEnter, delegate { OnEnter(); });
        AddEvent(result_screen, EventTriggerType.PointerExit, delegate { OnExit(); });
    }
    public void OnEnter()
    {       
              decribe_block = Instantiate(Resources.Load<GameObject>("describe"), Input.mousePosition, Quaternion.identity);
              decribe_block.GetComponent<Follow_mouse>().set_information(final_result.Item_ID.ToString(), final_result.name, final_result.description);
    }
    public void OnExit()
    {       
        Destroy(decribe_block);
        decribe_block = null;
    }
    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
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
    public void craft_check(ItemObject result)//第一步
    {
        if (can_recieve == false)
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
    }
    public void recieve_button()//close the second step
    {
        if (can_recieve == true)
        {
            TrueItem TI = new TrueItem(final_result);
            TI.Player_named = named_temp;
            player.AddNamedPotion(TI,5,named_temp);
            final_result = null;
            can_recieve = false;
            named_temp = null;
            result_button.GetComponentsInChildren<Image>()[1].sprite = null;
            result_button.GetComponentsInChildren<Image>()[1].color = new Color(1, 1, 1, 0);
            second_step.SetActive(false);
            Result_big.SetActive(false);
        }
    }
    public void check_screen_Open()
    {
        if (can_recieve == true)
        {
            Result_big.SetActive(true);
            first_step.SetActive(true);
            result_screen.GetComponentsInChildren<Image>()[1].sprite = result_button.GetComponentsInChildren<Image>()[1].sprite;
            result_screen.GetComponentsInChildren<Image>()[1].color = new Color(1, 1, 1, 1);
            UI_Controller.instance.player.playerState = PlayerState.Typing;
        }
    }
    public void check_screen_Close()
    {
        result_screen.GetComponentsInChildren<Image>()[1].sprite = null;
        result_screen.GetComponentsInChildren<Image>()[1].color = new Color(1, 1, 1, 0);
        final_result = null;
        can_recieve = false;
        named_temp = null;
        result_button.GetComponentsInChildren<Image>()[1].sprite = null;
        result_button.GetComponentsInChildren<Image>()[1].color = new Color(1, 1, 1, 0);
        second_step.SetActive(false);
        Result_big.SetActive(false);
        UI_Controller.instance.player.playerState = PlayerState.Brewing;
    }
    public void Second_step()
    {
        named_temp = input.text;
        first_step.SetActive(false);
        second_step.SetActive(true);
        UI_Controller.instance.player.playerState = PlayerState.Brewing;
        the_name_of_it.text = named_temp;
    }
}

