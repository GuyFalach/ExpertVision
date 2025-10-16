using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using Photon.Pun;
using Photon.Realtime;
using System;
using Oculus.Avatar2;
using Unity.VisualScripting;
using UnityEngine.Video;

public class ConectionManager : MonoBehaviourPunCallbacks
{
    [Space] [SerializeField] private GameObject GuestPrefab; 
    [SerializeField] private GameObject watch;
    [SerializeField] private GameObject board;
    [Space] [SerializeField] private GameObject onlineIndicator;
    [SerializeField] private Material onlineMat;
    [SerializeField] private Material offlineMat;
    // Start is called before the first frame update
    void Start()
    {
        watch.SetActive(false);
        Debug.LogWarning("Connecting...");
        Logger.Instance.LogInfo("Connecting");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.LogWarning("Connected to Server");
        Logger.Instance.LogInfo("Connected to Server");
        PhotonNetwork.JoinLobby();
        watch.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        onlineIndicator.GetComponent<Renderer>().material = offlineMat;
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        PhotonNetwork.JoinOrCreateRoom("room", roomOptions, TypedLobby.Default);
        Debug.LogWarning("We're connected and in in a room new");
        Logger.Instance.LogInfo("We're connected and in in a room new");
        watch.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.LogWarning("Player spawned");
        Logger.Instance.LogInfo("Player spawned");
        watch.SetActive(false);
        onlineIndicator.GetComponent<Renderer>().material = onlineMat;
        VideoPlayer videoPlayer = board.GetComponent<VideoPlayer>();
        videoPlayer.enabled = true;
        //videoPlayer.Play();
        AddGuest();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        watch.SetActive(true);
        Debug.LogWarning("A new player joined the room");
    }

    private void AddGuest()
    {
        GameObject gameObject = PhotonNetwork.Instantiate(GuestPrefab.name, Vector3.zero, Quaternion.identity);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
