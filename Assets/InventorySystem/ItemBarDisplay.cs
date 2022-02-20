using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemBarDisplay : MonoBehaviour
{
    public AudioClip ItemChangeSF;
    AudioSource audioSource;
    public GameObject Focus;
    public GameObject InventoryPrefab;
    public InventoryObject inventory;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SAPCE_BETWEEN_ITEM;

    int MainToolNum = 0;
    public InventorySlot items;

    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    public static ItemBarDisplay instance;
    private void Awake()
    {
        instance = this;        
    }
    void Start()
    {
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlot();
        if (inventory.Container.Length>0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                audioSource =  GameObject.FindGameObjectWithTag("system").GetComponent<AudioSource>();
                audioSource.PlayOneShot(ItemChangeSF,0.2f* GameObject.FindGameObjectWithTag("system").GetComponent<BGM_Center>().volume);
                MainToolNum--;
                MainToolNum = Mathf.Clamp(MainToolNum, 0, Mathf.Clamp(inventory.Container.Length-1, 0, 5));
                Debug.Log(MainToolNum);
                if (inventory.Container[MainToolNum] != null)
                {
                    items = inventory.Container[MainToolNum];
                    Focus.GetComponent<RectTransform>().localPosition = GetPosition(MainToolNum);
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                audioSource = GameObject.FindGameObjectWithTag("system").GetComponent<AudioSource>();
                audioSource.PlayOneShot(ItemChangeSF, 0.2f * GameObject.FindGameObjectWithTag("system").GetComponent<BGM_Center>().volume);
                MainToolNum++;
                MainToolNum = Mathf.Clamp(MainToolNum, 0, Mathf.Clamp(inventory.Container.Length-1, 0, 5));
                Debug.Log(MainToolNum);
                items = inventory.Container[MainToolNum];
                Focus.GetComponent<RectTransform>().localPosition = GetPosition(MainToolNum);

            }
        }        
    }
    public void UpdateSlot()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if (_slot.Value.ID >= 0)
            {
                _slot.Key.GetComponentsInChildren<Image>()[1].sprite = inventory.GetItem(_slot.Value.ID).sprite;
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
        var obj2 = Instantiate(Focus, Vector3.zero, Quaternion.identity, transform);
        Focus = obj2;
        Focus.GetComponent<RectTransform>().localPosition = GetPosition(0);
        for (int i = 0; i < NUMBER_OF_COLUMN; i++)
        {
            var obj = Instantiate(InventoryPrefab, Vector3.zero, Quaternion.identity,transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            itemsDisplayed.Add(obj, inventory.Container[i]);
        }
        
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + ((-Y_SAPCE_BETWEEN_ITEM) * (i / NUMBER_OF_COLUMN)));
    }
    public void OnLoad()
    {
        if (inventory.Container.Length>0)
        {
            items = inventory.Container[0];
        }        
    }
}
