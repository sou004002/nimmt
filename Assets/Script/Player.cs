using System.Collections;
using System.Collections.Generic;
using System;
using Photon.Pun;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviourPunCallbacks
{
    private GameObject Deck;
    [SerializeField] TextMeshProUGUI intHandNumText;
    private List<int> intHandArray;
    // private Deck _deck;
    private PhotonView _deckPhoton;

    // Start is called before the first frame update
    void Start()
    {
        intHandArray=new List<int>();

    }

    // Update is called once per frame
    void Update()
    {
        intHandNumText.text=string.Join(", ",intHandArray);
    }
    public List<int> GetIntHandArray()
    {
        return intHandArray;
    }
    public void Init()
    {
        // intHandArray=new List<int>();
        // Deck=GameObject.FindGameObjectsWithTag("Deck");
        // if(Deck!=null)
        // {
        //     _deckPhoton=Deck.GetComponent<PhotonView>();
        // }
        
        // if(_deckPhoton !=null && _deckPhoton.IsMine)
        // {
        //     Debug.Log("draw");
        //     _deckPhoton.RPC("Draw",RpcTarget.All);
        // }
        // photonView.RPC("DrawCard",)
    }

    [PunRPC]
    public void DrawCard(int card)
    {
        if(photonView.IsMine)
        {
        Debug.Log("draw");
        Debug.Log(photonView.Controller);
        intHandArray.Add(card);
        }
    }
    // public void CopyIntHandArray(List<int> handarray)
    // {
    //     intHandArray=new int[handarray.Count];
    //     for(int i=0;i<intHandArray.Length;i++)
    //     {
    //         intHandArray[i]=handarray[i];
    //     }
    // }

}
