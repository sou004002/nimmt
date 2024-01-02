using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
public class CardClick : MonoBehaviourPunCallbacks,IPointerClickHandler
{
    private GameObject[] players;
    private GameObject[] cards;


    public void OnPointerClick(PointerEventData eventData)
    {
        //見た目を消す
        if(photonView!=null && photonView.IsMine)
        {
            int card=GetComponent<Card>().GetCardNum();
            players=GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject obj in players)
            {
                if(obj.GetComponent<PhotonView>().OwnerActorNr==photonView.OwnerActorNr)
                {
                    Debug.Log(photonView.OwnerActorNr);
                    obj.GetComponent<PhotonView>().RPC("PlayCard",RpcTarget.All,card);
                }
            }
            cards=GameObject.FindGameObjectsWithTag("Card");
            foreach(GameObject c in cards)
            {
                c.GetComponent<Card>().SetIsdrawing(true);
            }
            GetComponent<Card>().SetIsdrawing(false);
            //Destroy(this.gameObject);

        }
    }
}
