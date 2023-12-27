using System.Collections;
using System.Collections.Generic;
using System;
using Photon.Pun;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviourPunCallbacks
{
    private GameObject Deck;
    [SerializeField] private GameObject cardPrefab;
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
        if(photonView.IsMine)
        {
            intHandNumText.text=string.Join(", ",intHandArray);
            if(Input.GetMouseButtonDown(1))
            {
                // Debug.Log("photonView.Controller");
                Deck=GameObject.FindGameObjectsWithTag("Deck")[0];
                _deckPhoton=Deck.GetComponent<PhotonView>();
                photonView.RPC(nameof(DrawCard),RpcTarget.All,Deck.GetComponent<Deck>().GetDecktop());
                GameObject card=Instantiate(cardPrefab,new Vector3(-6+(intHandArray.Count)*2,1.5f,-7.0f),Quaternion.Euler(36,0,0));
                card.GetComponent<Card>().Init(Deck.GetComponent<Deck>().GetDecktop());
                _deckPhoton.RPC("Draw",RpcTarget.All);
                // Debug.Log(photonView.Controller);
                
            }
        }
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
        Debug.Log("draw");
        intHandArray.Add(card);
        // Debug.Log(string.Join(", ",intHandArray));
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
