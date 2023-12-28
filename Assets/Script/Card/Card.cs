using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Card : MonoBehaviourPunCallbacks
{
    private int cardNum;
    private int cardDamage;
    private Sprite[] images;
    private BoxCollider _col;

    void Start()
    {
        // _col=GetComponent<BoxCollider>();
        // _col.enabled=false;
    }
    
    public int GetCardNum()
    {
        return cardNum;
    }
    public void SetCardNum(int n)
    {
        cardNum=n;
    }
    public void Init(int number)
    {
        _col=GetComponent<BoxCollider>();
        images = Resources.LoadAll<Sprite>("Sprites/");
        GameObject frontImage=transform.Find("FrontImage").gameObject;
        GameObject backImage=transform.Find("BackImage").gameObject;
        SetCardNum(number);
        frontImage.GetComponent<SpriteRenderer>().sprite=images[number];
        backImage.GetComponent<SpriteRenderer>().sprite=images[0];
        _col.enabled=true;

    }

}
