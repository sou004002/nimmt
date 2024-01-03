using System.Collections;
using System.Collections.Generic;
using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerDraw : MonoBehaviourPunCallbacks
{
    private GameObject deck;
    private GameObject GameManager;
    private int HP;
    private const int DEFAULT_HP=66;

    private string nickName;
    // [SerializeField] GameObject cardPrefab;

    // [SerializeField] TextMeshProUGUI intHandNumText;
    private List<int> intHandArray=new List<int>();
    private PhotonView _deckPhoton;
    private Deck _deck;

    private int playedCard=0;


    // Start is called before the first frame update
    void Start()
    {
        intHandArray=new List<int>();
        //この後すぐにカードを引くとintHandArrayがnullになる
    }

    void Update()
    {
        // intHandNumText.text=playedCard.ToString();
        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log(intHandArray.Count);
            StartCoroutine("DrawCardCoroutine");
        }
    }

    IEnumerator DrawCardCoroutine() //deckがnullになっちゃう
    {
        for(int i=0;i<10;i++)
        {
            Init();
            yield return new WaitForSeconds(0.3f);
        }
    }
    public void Init()
    {
        if(photonView.IsMine && photonView!=null)
        {
            deck=GameObject.FindGameObjectsWithTag("Deck")[0];
            _deckPhoton=deck.GetComponent<PhotonView>();
            _deck=deck.GetComponent<Deck>();
            photonView.RPC(nameof(DrawCard),RpcTarget.All,_deck.GetDecktop());
            //カードボタンを生成、数字と画像を設定
            // GameObject cardButton=Instantiate("Card",new Vector3(-6+(intHandArray.Count)*2,-1.5f,-7.0f),Quaternion.Euler(36,0,0));
            // card.GetComponent<Card>().Init(deck.GetComponent<Deck>().GetDecktop());
            _deckPhoton.RPC("Draw",RpcTarget.All);//一番上を消す
        } 
    }
    // Update is called once per frame
    public List<int> GetIntHandArray()
    {
        return intHandArray;
    }

    public int GetPlayedCard()
    {
        return playedCard;
    }

    public void removeCard(int card)
    {
        intHandArray.Remove(card);
    }
    [PunRPC]
    public void DrawCard(int card)
    {        
        intHandArray.Add(card);
    }

    [PunRPC]
    public void PlayCard(int card)
    {
        playedCard=card;
    }
}
