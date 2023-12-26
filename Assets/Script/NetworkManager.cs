using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField]private GameObject camera;
    private int playerOffset=20;//プレイヤー同士の間隔
    private void Start() {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        Debug.Log("Setting");
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster() {
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom() {
        //1人目なら場の生成
        //それ以外なら、山札から手札配る
        int playerNumber=PhotonNetwork.CurrentRoom.PlayerCount;
        if(playerNumber==1)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                GameObject deckArray=PhotonNetwork.InstantiateRoomObject("Deck",Vector3.zero,Quaternion.identity);
                deckArray.GetComponent<Deck>().generateDeckArray();
                // int[] intDeckArray=deckArray.GetComponent<Deck>().GetIntDeckArray();
                // // Debug.Log(deckArray.GetComponent<Deck>().GetIntDeckArray().Length);
                // PhotonNetwork.CurrentRoom.SetDeckArray(intDeckArray);
                // // Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties["DeckArray"]);
                // deckNumText.text=String.Join(",",PhotonNetwork.CurrentRoom.TryGetDeckArray());
                // // PhotonNetwork.CurrentRoom.SetDeckArray(deckArray.GetComponent<Deck>().GetDeckArray());
            }

        }
        var position = new Vector3(0,playerNumber*playerOffset,0);
        GameObject player=PhotonNetwork.Instantiate("Player", position, Quaternion.identity);
        // camera.transform.parent = player.transform;
        // camera.transform.position = player.transform.position;
    }
}