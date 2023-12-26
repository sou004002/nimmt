
using System.Collections;
using System.Collections.Generic;
using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Deck : MonoBehaviourPunCallbacks,IPunObservable
{
    // private List<int> deckArray;
    private int[] intDeckArray;
    [SerializeField] TextMeshProUGUI intDeckNumText;
    [SerializeField] private GameObject cardPrefab;
    private GameObject[] playerPrefabs;


    public void generateDeckArray()//デッキ作成
    {
        List<int> deckArray=new List<int>();
        // if(deckArray==null)
        // {
        //     deckArray=new List<int>();
        // }
        // else
        // {
        //     deckArray.Clear();
        // }

        for (int i=1;i<105;i++)
        {
            deckArray.Add(i);
        }
        int n=deckArray.Count;
        while(n>1)
        {
            n--;
            int k=UnityEngine.Random.Range(0,n+1);
            int temp=deckArray[k];
            deckArray[k]=deckArray[n];
            deckArray[n]=temp;
        }
        CopyIntDeckArray(deckArray);
    }
    // public List<int> GetDeckArray()
    // {
    //     foreach(int i in deckArray)
    //     {
    //         Debug.Log(i);
    //     }
    //     return deckArray;
    // }
    public int[] GetIntDeckArray()
    {
        // foreach(int i in intDeckArray)
        // {
        //     Debug.Log(i);
        // }
        return intDeckArray;
    }
    public void CopyIntDeckArray(List<int> deckarray)
    {
        intDeckArray=new int[deckarray.Count];
        for(int i=0;i<intDeckArray.Length;i++)
        {
            intDeckArray[i]=deckarray[i];
        }
    }

    public int GetDecktop()
    {
        return intDeckArray[0];
    }
    // public void CopyDeckArray()
    // {
    //     deckArray=new List<int>();
    //     for(int i=0;i<intDeckArray.Length;i++)
    //     {
    //         deckArray.Add(intDeckArray[i]);
    //     }
    // }


    private void Update()
    {
        intDeckNumText.text=String.Join(",",GetIntDeckArray());
        if(Input.GetMouseButtonDown(1))
        {
            photonView.RPC(nameof(Draw),RpcTarget.All);
        }
    }



    [PunRPC]
    public void Draw(PhotonMessageInfo info)
    {
        playerPrefabs=GameObject.FindGameObjectsWithTag("Player");
        List<int> deckArray=new List<int>();
        foreach(int i in intDeckArray)
        {
            deckArray.Add(i);
        }
        int removedCard=deckArray[0];
        deckArray.RemoveAt(0);
        foreach(var obj in playerPrefabs)
        {
            PhotonView _playerPhoton=obj.GetComponent<PhotonView>();
            if(_playerPhoton!=null && _playerPhoton.IsMine)
            {
                Debug.Log("a");
                _playerPhoton.RPC("DrawCard",RpcTarget.All,removedCard);
            }
        }
        CopyIntDeckArray(deckArray);
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
            if (stream.IsWriting) {
                stream.SendNext(GetIntDeckArray());
            } else {
                intDeckArray = (int[])stream.ReceiveNext();
            }
        }

}
