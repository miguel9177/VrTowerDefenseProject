using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    //this will save the start round button
    GameObject btnStartRound;

    Rigidbody rbd;

    [SerializeField]
    //this will save the text that counts the rounds
    Text txtRoundCounter;
    
    [SerializeField]
    //this will save the text that shows the user hp
    Text txtShowHp;

    [SerializeField]
    //this will save the text that shows the user Money
    Text txtShowMoney;

    //this will be called when the player loses
    public void PlayerLost()
    {
        //this will deactivate the btnstartround
        btnStartRound.SetActive(false);
        //this will deactivate the text that says in wich round we are
        txtRoundCounter.gameObject.SetActive(false);
    
    }

    //this will update the text of the round counter
    public void UpdateTxtRoundCounter(int currentRound_)
    {
        txtRoundCounter.text = "Round: " + currentRound_;
    }

    //this will update the text of the hp
    public void UpdateTxtHp(int currentHp_)
    {
        txtShowHp.text = "Hp: " + currentHp_;
    }

    //this will update the text of the Money
    public void UpdateTxtMoney(float currentMoney_)
    {
        txtShowMoney.text = "Money: " + currentMoney_;
    }

    //this will show btnstartround (the button to make the next round go)
    public void ShowBtnStartRound()
    {
        btnStartRound.SetActive(true);
    }

    //this will hide btnstartround (the button to make the next round go)
    public void HideBtnStartRound()
    {
        btnStartRound.SetActive(false);
    }

}
