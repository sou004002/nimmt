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
    private GameObject frontImage;
    private GameObject backImage;
    private bool isDrawing;
    private bool isUpdate;
    void Start()
    {
        // _col=GetComponent<BoxCollider>();
        // _col.enabled=false;
        isDrawing=true;
        isUpdate=false;
    }
    void Update()
    {
        if(frontImage!=null && backImage!=null && _col!=null)
        {
            if(isDrawing)
            {
                frontImage.GetComponent<SpriteRenderer>().enabled=true;
                backImage.GetComponent<SpriteRenderer>().enabled=true;
                _col.enabled=true;
            }
            else
            {
                frontImage.GetComponent<SpriteRenderer>().enabled=false;
                backImage.GetComponent<SpriteRenderer>().enabled=false;
                _col.enabled=false;
            }
        }
    }
    
    public int GetCardNum()
    {
        return cardNum;
    }
    public void SetCardNum(int n)
    {
        cardNum=n;
    }
    public int GetCardDamage()
    {
        return cardDamage;
    }
    public void SetCardDamage(int n)
    {
        int d;
        if(n==55)
        {
            d=7;
        }
        else if(n%11==0)
        {
            d=5;
        }
        else if(n%10==0)
        {
            d=3;
        }
        else if(n%5==0)
        {
            d=2;
        }
        else
        {
            d=1;
        }
        cardDamage=d;
    }
    public void SetIsdrawing(bool b)
    {
        isDrawing=b;
    }
    public void Init(int number)
    {
        _col=GetComponent<BoxCollider>();
        if(images==null)
        {
            images = Resources.LoadAll<Sprite>("Sprites/");
        }
        if(frontImage==null)
        {
            frontImage=transform.Find("FrontImage").gameObject;
        }
        if(backImage==null)
        {
            backImage=transform.Find("BackImage").gameObject;
        }
        SetCardNum(number);
        SetCardDamage(number);
        frontImage.GetComponent<SpriteRenderer>().sprite=images[number];
        backImage.GetComponent<SpriteRenderer>().sprite=images[0];
        _col.enabled=true;
        isDrawing=true;
    }

}
