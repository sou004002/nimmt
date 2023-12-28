using System.Collections;
using System.Collections.Generic;
using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviourPunCallbacks
{
    private GameObject Deck;
    private GameObject GameManager;
    // [SerializeField] GameObject cardPrefab;

    [SerializeField] TextMeshProUGUI intHandNumText;
    private List<int> intHandArray;
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
        intHandNumText.text=playedCard.ToString();
        if(Input.GetMouseButtonDown(1))
        {
        if(photonView.IsMine && photonView!=null)
        {
            Deck=GameObject.FindGameObjectsWithTag("Deck")[0];
            _deckPhoton=Deck.GetComponent<PhotonView>();
            _deck=Deck.GetComponent<Deck>();
            photonView.RPC(nameof(DrawCard),RpcTarget.All,_deck.GetDecktop());
            GameObject card=PhotonNetwork.Instantiate("Card",new Vector3(-6+(intHandArray.Count)*2,1.5f,-7.0f),Quaternion.Euler(36,0,0));

            card.GetComponent<Card>().Init(Deck.GetComponent<Deck>().GetDecktop());
            _deckPhoton.RPC("Draw",RpcTarget.All);
            
        }
        }
    }

    // Update is called once per frame
    public List<int> GetIntHandArray()
    {
        return intHandArray;
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
        intHandArray.Remove(card);
    }
}
