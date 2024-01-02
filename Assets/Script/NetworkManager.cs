using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField]private GameObject camera;
    private int playerOffset=20;//プレイヤー同士の間隔
    private static int nextID=1;
    GameObject deckArray;
    private void Start() {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.NickName = "Player";
        Debug.Log("Setting");
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster() {
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom() { //ルームオブジェクトの処理のみ行う
        int playerNumber=PhotonNetwork.CurrentRoom.PlayerCount;
        var position = new Vector3(0,playerNumber*playerOffset,0);
        GameObject player=PhotonNetwork.Instantiate("Player", position, Quaternion.identity);
        //1人目なら場の生成
        //それ以外なら、山札から手札配る
        if(playerNumber==1)
        {
            deckArray=PhotonNetwork.InstantiateRoomObject("Deck",Vector3.zero,Quaternion.identity);
            GameObject gameManager=PhotonNetwork.InstantiateRoomObject("GameManager",new Vector3(-5,-5,0),Quaternion.identity);
            deckArray.GetComponent<Deck>().generateDeckArray();
            gameManager.GetComponent<FieldManager>().Init();
        }
    }
}