using System.Collections;
using System.Collections.Generic;
using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FieldManager : MonoBehaviourPunCallbacks,IPunObservable
{
    private int[,] fieldCards;
    private const int ROWNUM=4;
    [SerializeField] TextMeshProUGUI FieldCardsText;
    private GameObject Deck;
    private PhotonView _deckPhoton;
    private Deck _deck;
    void Start()
    {

    }

    void Update()
    {
        if(fieldCards!=null)
        {
            FieldCardsText.text=GetFieldCards();
        }
    }

    public string GetFieldCards()
    {
        string str = "";
        for (int i = 0;i < fieldCards.GetLength(0);i++)
        {
            for (int j = 0; j < fieldCards.GetLength(1); j++)
            {
                str = str + fieldCards[i, j] + " ";
            }
            str+=" | ";
        }
        return str;
    }

    // public void PlayCard(int card)
    // {
    //     List<int> tmpArray=new List<int>();
    //     foreach(int i in playedCards)
    //     {
    //         tmpArray.Add(i);
    //     }
    //     tmpArray.Add(card);
    //     CopyIntDeckArray(tmpArray);
    // }

    // public void CopyIntDeckArray(List<int> tmparray)
    // {
    //     playedCards=new int[tmparray.Count];
    //     for(int i=0;i<playedCards.Length;i++)
    //     {
    //         playedCards[i]=tmparray[i];
    //     }
    // }
    [PunRPC]
    public void Init()
    {
        fieldCards=new int[ROWNUM,1];
        Deck=GameObject.FindGameObjectsWithTag("Deck")[0];
        _deckPhoton=Deck.GetComponent<PhotonView>();
        _deck=Deck.GetComponent<Deck>();
        for(int i=0;i<ROWNUM;i++)
        {
            fieldCards[i,0]=(_deck.GetDecktop());
            Debug.Log(fieldCards[i,0]);
            _deckPhoton.RPC("Draw",RpcTarget.All);
        }
    }
    async void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
    {
            if(stream.IsWriting)
            {
                for (int i = 0;i < fieldCards.GetLength(0);i++)
                {
                    for (int j = 0; j < fieldCards.GetLength(1); j++)
                    {
                        stream.SendNext(fieldCards[i,j]);
                    }
                }
            } 
            else
            {
                for (int i = 0;i < fieldCards.GetLength(0);i++)
                {
                    for (int j = 0; j < fieldCards.GetLength(1); j++)
                    {
                        fieldCards[i,j]=(int)stream.ReceiveNext();
                    }
                }

            }
    }
}
