using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public abstract class UserInterface : MonoBehaviour
{
    public PlayerBackPack player;
    public InventoryObject inventory;
    public Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    void Start()
    {
        for (int i = 0; i < inventory.Container.Length; i++)
        {
            inventory.Container[i].parent = this;
        }
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlot();
    }
    public void UpdateSlot()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if (_slot.Value.ID >= 0)
            {
                _slot.Key.GetComponentsInChildren<Image>()[1].sprite = inventory.GetItem(_slot.Value.item.Id).sprite;
                _slot.Key.GetComponentsInChildren<Image>()[1].color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<Text>().text = _slot.Value.amount.ToString("n0");

            }
            else
            {
                _slot.Key.GetComponentsInChildren<Image>()[1].sprite = null;
                _slot.Key.GetComponentsInChildren<Image>()[1].color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<Text>().text = "";
            }
        }

    }
    public abstract void CreateSlots();

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    public void OnEnter(GameObject obj)
    {
        player._MouseItem.hoverobj = obj;
        if (itemsDisplayed.ContainsKey(obj))
        {
            player._MouseItem.hoverItem = itemsDisplayed[obj];
        }
    }
    public void OnExit(GameObject obj)
    {
        player._MouseItem.hoverobj = null;

        player._MouseItem.hoverItem = null;

    }
    public void OnDragStart(GameObject obj)
    {
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(50, 50);
        mouseObject.transform.SetParent(transform.parent);
        if (itemsDisplayed[obj].ID >= 0)
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.GetItem(itemsDisplayed[obj].ID).sprite;
            img.raycastTarget = false;
        }
        player._MouseItem.obj = mouseObject;
        player._MouseItem.item = itemsDisplayed[obj];
    }
    public void OnDrag(GameObject obj)
    {
        if (player._MouseItem.obj != null)
        {
            player._MouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
    public void OnDragExit(GameObject obj)
    {
        if (player._MouseItem.hoverobj)
        {
            inventory.MoveItem(itemsDisplayed[obj],  player._MouseItem.hoverItem.parent.itemsDisplayed[player._MouseItem.hoverobj]);
        }
        else
        {

        }
        Destroy(player._MouseItem.obj);
        player._MouseItem.item = null;
    }
    
}
public class MouseItem
{
    public GameObject obj;
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverobj;
}
