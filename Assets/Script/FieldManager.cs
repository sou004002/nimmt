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

    void Update()
    {
        if(!(fieldCards01==null||fieldCards02==null||fieldCards03==null||fieldCards04==null))
        {
            FieldCardsText.text=GetFieldCards();
        }
        if(Input.GetMouseButtonDown(2))
        {
            Judge();
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
    }

    public void Judge()
    {
        GameObject[] players=GameObject.FindGameObjectsWithTag("Player");
        List<(int,int)> JudgeList=new List<(int,int)>();
        foreach(GameObject p in players)
        {
            Player _player=p.GetComponent<Player>();
            (int,int) t = (p.GetComponent<PhotonView>().OwnerActorNr,_player.GetPlayedCard());
            JudgeList.Add(t);
            _player.removeCard(_player.GetPlayedCard());
            foreach(int i in _player.GetIntHandArray())
            {
                Debug.Log(i);
            }
        }
        JudgeList.Sort((tuple1,tuple2) => tuple1.Item2.CompareTo(tuple2.Item2));
        for(int i=0;i<players.Length;i++)
        {
            List<int> lastCards=new List<int>();
            lastCards.Add(fieldCards01[fieldCards01.Length-1]);
            lastCards.Add(fieldCards02[fieldCards02.Length-1]);
            lastCards.Add(fieldCards03[fieldCards03.Length-1]);
            lastCards.Add(fieldCards04[fieldCards04.Length-1]);
            foreach(var lc in lastCards)
            {
                Debug.Log(lc);
            }
            int playRowNum=-1;
            int cardDiff=105;
            for(int j=0;j<ROWNUM;j++)
            {
                if(JudgeList[i].Item2-lastCards[j]>0)
                {
                    if(cardDiff>=(JudgeList[i].Item2-lastCards[j]))
                    {
                        cardDiff=JudgeList[i].Item2-lastCards[j];
                        playRowNum=j;
                    }
                }
            }
            Debug.Log(playRowNum);
            if(playRowNum==-1)
            {
                //取る行を選ばせる処理
                continue;
            }
            switch(playRowNum)
            {
                case 0:
                    fieldCards01=AddFieldCard(fieldCards01,JudgeList[i].Item2);
                    Debug.Log("0");
                    break;
                case 1:
                    fieldCards02=AddFieldCard(fieldCards02,JudgeList[i].Item2);
                    Debug.Log("1");
                    break;
                case 2:
                    fieldCards03=AddFieldCard(fieldCards03,JudgeList[i].Item2);
                    Debug.Log("2");
                    break;
                case 3:
                    fieldCards04=AddFieldCard(fieldCards04,JudgeList[i].Item2);
                    Debug.Log("3");
                    break;
                default:
                    break;
            }
        }

    }


    public int[] AddFieldCard(int[] fieldcard,int card)
    {
        List<int> tmpList=new List<int>();
        foreach(int i in fieldcard)
        {
            tmpList.Add(i);
        }
        tmpList.Add(card);
        int[] newFieldCard=new int[tmpList.Count];
        for(int i=0;i<newFieldCard.Length;i++)
        {
            newFieldCard[i]=tmpList[i];
        }
        return newFieldCard;
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
