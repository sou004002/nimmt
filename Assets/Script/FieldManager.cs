using System.Collections;
using System.Collections.Generic;
using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FieldManager : MonoBehaviourPunCallbacks,IPunObservable
{
    private int[] fieldCards01;
    private int[] fieldCards02;
    private int[] fieldCards03;
    private int[] fieldCards04;

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
        if(!(fieldCards01==null||fieldCards02==null||fieldCards03==null||fieldCards04==null))
        {
            Debug.Log("a");
            FieldCardsText.text=GetFieldCards();
        }
    }

    public string GetFieldCards()
    {
        string str = "";
        for (int j = 0; j < fieldCards01.Length; j++)
        {
            str = str + fieldCards01[j] + " ";
        }
        str+=" | ";
        for (int j = 0; j < fieldCards02.Length; j++)
        {
            str = str + fieldCards02[j] + " ";
        }
        str+=" | ";
        for (int j = 0; j < fieldCards03.Length; j++)
        {
            str = str + fieldCards03[j] + " ";
        }
        str+=" | ";
        for (int j = 0; j < fieldCards04.Length; j++)
        {
            str = str + fieldCards04[j] + " ";
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
    public void Init()
    {
        fieldCards01=new int[1];
        fieldCards02=new int[1];
        fieldCards03=new int[1];
        fieldCards04=new int[1];

        Deck=GameObject.FindGameObjectsWithTag("Deck")[0];
        _deckPhoton=Deck.GetComponent<PhotonView>();
        _deck=Deck.GetComponent<Deck>();
        fieldCards01[0]=(_deck.GetDecktop());
        _deckPhoton.RPC("Draw",RpcTarget.All);

        fieldCards02[0]=(_deck.GetDecktop());
        _deckPhoton.RPC("Draw",RpcTarget.All);

        fieldCards03[0]=(_deck.GetDecktop());
        _deckPhoton.RPC("Draw",RpcTarget.All);

        fieldCards04[0]=(_deck.GetDecktop());
        _deckPhoton.RPC("Draw",RpcTarget.All);
        Debug.Log(fieldCards01==null||fieldCards02==null||fieldCards03==null||fieldCards04==null);

    }
     
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
    {
            if(stream.IsWriting)
            {
                stream.SendNext(fieldCards01);
                stream.SendNext(fieldCards02);
                stream.SendNext(fieldCards03);
                stream.SendNext(fieldCards04);
            } 
            else
            {
                fieldCards01=(int[])stream.ReceiveNext();
                fieldCards02=(int[])stream.ReceiveNext();
                fieldCards03=(int[])stream.ReceiveNext();
                fieldCards04=(int[])stream.ReceiveNext();
            }
    }
}
