using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DisplayBackPack : MonoBehaviour
{
    public MouseItem MouseItem = new MouseItem();

    public GameObject InventoryPrefab;
    public InventoryObject inventory;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SAPCE_BETWEEN_ITEM;
    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    void Start()
    {
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlot();
    }
    public void UpdateSlot()
    {
        foreach (KeyValuePair<GameObject,InventorySlot>_slot in itemsDisplayed)
        {
            if (_slot.Value.ID>=0)
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
    public void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Length; i++)
        {
            var obj = Instantiate(InventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj,EventTriggerType.PointerEnter,delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragExit(obj); });

            itemsDisplayed.Add(obj, inventory.Container[i]);
        }

    }
    private void AddEvent(GameObject obj,EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    public void OnEnter(GameObject obj)
    {
        MouseItem.hoverobj = obj;
        if (itemsDisplayed.ContainsKey(obj))
        {
            MouseItem.hoverItem = itemsDisplayed[obj];
        }
    }
    public void OnExit(GameObject obj)
    {
        MouseItem.hoverobj = null;

        MouseItem.hoverItem = null;
        
    }
    public void OnDragStart(GameObject obj)
    {
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta =new Vector2(50, 50);
        mouseObject.transform.SetParent(transform.parent);
        if (itemsDisplayed[obj].ID>=0)
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.GetItem(itemsDisplayed[obj].ID).sprite;
            img.raycastTarget = false;
        }
        MouseItem.obj = mouseObject;
        MouseItem.item = itemsDisplayed[obj];
    }
    public void OnDrag(GameObject obj)
    {
        if (MouseItem.obj != null)
        {
            MouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
    public void OnDragExit(GameObject obj)
    {
        if (MouseItem.hoverobj)
        {
            inventory.MoveItem(itemsDisplayed[obj], itemsDisplayed[MouseItem.hoverobj]);
        }
        else
        {

        }
        Destroy(MouseItem.obj);
        MouseItem.item = null;
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START+(X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)),Y_START+ ((-Y_SAPCE_BETWEEN_ITEM) * (i / NUMBER_OF_COLUMN)));
    }
}
public class MouseItem
{
    public GameObject obj;
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverobj;
}