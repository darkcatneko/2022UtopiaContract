using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState {START, PLAYERTURN, MONSTERTURN, WIN, LOSS}
public class Battle_system : MonoBehaviour
{
    public BattleState state;
    public InventoryObject Player_information;
    public InventoryObject craftingtable;
    public InventoryObject PotionBack;
    public Monster_class This_Monster;
    public GameObject Player;
    public GameObject Monster;

    //Monster UI
    public Text Monster_name;
    public Text Monster_LV;
    public Slider Monster_hp;
    //Player  UI
    public Text Player_lv;
    public Slider Player_hp;
    //Dialogue
    public GameObject dialogue;
    public GameObject In_battle;
    //Attack Button
    public GameObject[] buttons;
    void Start()
    {
        state = BattleState.START;
        This_Monster = GameObject.Find("Monster_encounter_data").GetComponent<On_encounter>().Monster;
        Destroy(GameObject.Find("Monster_encounter_data"));
        SetupBattle();
    }

    // Update is called once per frame
    void Update()
    {
        Monster_hp.value = This_Monster.Monster_Health;
        Player_hp.value = Player_information.BattleStatus.Player_Health;
        Update_skill();
    }
    void SetupBattle()
    {
        Monster_name.text = This_Monster.Monster_name;
        Monster_LV.text = "LV:  " + This_Monster.Monster_Lv;
        Monster_hp.maxValue = This_Monster.Monster_MaxHP;
        Monster_hp.value = This_Monster.Monster_Health;
        Player_lv.text = Player_information.BattleStatus.Player_Level.ToString();
        Player_hp.maxValue = Player_information.BattleStatus.Player_Health;
        Player_hp.value = Player_information.BattleStatus.Player_Health;
        //UI建置
        Update_skill();
        //技能設置
        dialogue.GetComponentInChildren<Text>().text = "A Wild " + This_Monster.Monster_name + " approaches !!!";
        StartCoroutine(Delay.DelayToInvokeDo(() => 
        { 
            dialogue.GetComponentInChildren<Text>().text = null;
            dialogue.SetActive(false);
            In_battle.SetActive(true);
        }, 1.5f));
        state = BattleState.PLAYERTURN;
    }

    private void OnApplicationQuit()
    {
        Player_information.Clear();
        craftingtable.Clear();
        PotionBack.Clear();
    }
    public void dialogue_update(string a)
    {
        In_battle.SetActive(false);
        dialogue.SetActive(true);
        dialogue.GetComponentInChildren<Text>().text = a;
        StartCoroutine(Delay.DelayToInvokeDo(() =>
        {
            dialogue.GetComponentInChildren<Text>().text = null;
            dialogue.SetActive(false);
            In_battle.SetActive(true);
        }, 1.5f));
    }
    IEnumerator PlayerAttack(int num)
    {
        Give_damage(num);
        state = BattleState.MONSTERTURN;
        dialogue_update("success!!!");
        yield return new WaitForSeconds(2f);
        if (This_Monster.change_health(This_Monster.Monster_Health))
        {
            state = BattleState.WIN;
            EndBattle();
        }
        else
        {
            StartCoroutine("MonsterAttack", num);
        }
    }
    IEnumerator MonsterAttack()
    {
        dialogue_update("attack!!!");
        yield return new WaitForSeconds(1f);
        Player_information.BattleStatus.health_change(Player_information.BattleStatus.Player_Health - 3);
        if (Player_information.BattleStatus.Player_Health<=0)
        {
            state = BattleState.LOSS;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
        }
    }
    public void EndBattle()
    {
        if (state == BattleState.WIN)
        {
            In_battle.SetActive(false);
            dialogue.SetActive(true);
            dialogue.GetComponentInChildren<Text>().text = "You Win!!";
            StartCoroutine(Delay.DelayToInvokeDo(() =>
            {
                Player_information.Save();
                craftingtable.Save();
                PotionBack.Save();
                SceneManager.LoadScene(2);
            }, 1.5f));
        }
        if (state == BattleState.LOSS)
        {
            In_battle.SetActive(false);
            dialogue.SetActive(true);
            dialogue.GetComponentInChildren<Text>().text ="You Loss";
            StartCoroutine(Delay.DelayToInvokeDo(() =>
            {
                Player_information.Save();
                craftingtable.Save();
                PotionBack.Save();
                SceneManager.LoadScene(2);
            }, 1.5f));
        }
    }
    public void On_Attack_Button_clicked(int num)
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        StartCoroutine("PlayerAttack", num);
    }
    public void Give_damage(int num)
    {
        if (PotionBack.Container[num].ID >= 0)
        {
            Potion_Effect(PotionBack.Container[num].ID);
            PotionBack.MinusAmount(PotionBack.Container[num].item, 1);
            return;
        }
    }
    public void Potion_Effect(int id)
    {
        switch(id)
        {
            case 6:
                This_Monster.change_health(This_Monster.Monster_Health - 5);
                return;
        }
    }
    public void Update_skill()
    {
        for (int i = 0; i < PotionBack.Container.Length; i++)
        {
            if (PotionBack.Container[i].ID>=0)
            {
                buttons[i].GetComponentInChildren<Text>().text = PotionBack.Container[i].Player_named;
            }
            else
            {
                buttons[i].GetComponentInChildren<Text>().text = "none";
            }
        }
    }

}
