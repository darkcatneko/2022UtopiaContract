using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Follow_mouse : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject des;
    public Text ID;
    public Text PlayerName;
    public Text describe;
    public string[] information;
    void Start()
    {
        des.transform.position = Input.mousePosition;
        ID.text = information[0];
        PlayerName.text = information[1];
        describe.text = information[2];
        des.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        des.transform.position = Input.mousePosition;
        ID.text = information[0];
        PlayerName.text = information[1];
        describe.text = information[2];
        if (UI_Controller.instance.BackPack.activeSelf == false)
        {
            Destroy(this.gameObject);
        }
    }
    public void set_information(string _id,string _name,string _describe)
    {
        information[0] = _id;
        information[1] = _name;
        information[2] = _describe;
    }
}
