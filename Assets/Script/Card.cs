using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private int cardNum;
    private int cardDamage;
    private Sprite[] images;
    
    public void SetCardNum(int n)
    {
        cardNum=n;
    }
    public void Init(int number)
    {
        images = Resources.LoadAll<Sprite>("Sprites/");
        GameObject frontImage=transform.Find("FrontImage").gameObject;
        SetCardNum(number);
        frontImage.GetComponent<SpriteRenderer>().sprite=images[number];
    }

}
