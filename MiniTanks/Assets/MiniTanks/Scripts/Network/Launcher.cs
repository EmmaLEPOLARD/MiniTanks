using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;

public class Launcher : MonoBehaviourPunCallbacks
{

    [SerializeField] private GameObject m_ControlPanel;
    [SerializeField] private byte m_MaxPlayers = 4;

    [SerializeField] private InputField m_PlayerNameInput;
    [SerializeField] private InputField m_RoomNameInput;

    [SerializeField] private Text m_PlayerStatusText;
    [SerializeField] private Text m_ConnectionStatusText;
    [SerializeField] private GameObject m_RoomJoinUI;
    [SerializeField] private GameObject m_LoadLvlButtonUI;
    [SerializeField] private GameObject m_JoinRoomButtonUI;

    private bool m_IsConnected;
    private string m_GameVersion = "1";
    private string m_PlayerName = "";
    private string m_RoomName = "";

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    private void Start()
    {
        InitLauncher();
    }

    /// <summary>
    /// Init Launcher components 
    /// </summary>
    private void InitLauncher()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Connecting to master photon server");

        m_RoomJoinUI.SetActive(false);
        m_LoadLvlButtonUI.SetActive(false);

        ConnectToPhoton();
    }

    /// <summary>
    /// Connect client to master photon server
    /// </summary>
    private void ConnectToPhoton()
    {
        m_ConnectionStatusText.text = "Connecting ...";
        PhotonNetwork.GameVersion = m_GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    /// <summary>
    /// Join room or create with name (m_RoomName)
    /// </summary>
    public void JoinRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LocalPlayer.NickName = m_PlayerName;
            Debug.Log("Client has been conected -> try to Join Room: " + m_RoomNameInput.text + " if room doesn't exist, create room");

            RoomOptions roomOps = new RoomOptions();
            TypedLobby typedLobby = new TypedLobby(m_RoomName, LobbyType.Default);
            PhotonNetwork.JoinOrCreateRoom(m_RoomName, roomOps, typedLobby);
        }
    }

    /// <summary>
    /// Load Scene
    /// </summary>
    /// <param name="_lvlName">scene name</param>
    public void LoadLvl(string _lvlName)
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerCount > 1 && playerCount < m_MaxPlayers)
        {
            PhotonNetwork.LoadLevel(_lvlName);
        }
        else
        {
            m_PlayerStatusText.text = "Min 2 player to launch and " + m_MaxPlayers + " player max for launch level";
        }

    }

    #region PhotonMethods

    /// <summary>
    /// Override event when client is connected to master Photon server
    /// </summary>
    public override void OnConnected()
    {
        base.OnConnected();
        m_PlayerStatusText.text = "Client has been connected to Photon ! ";
        m_PlayerStatusText.color = Color.green;
        m_ConnectionStatusText.text = PhotonNetwork.CountOfRooms + " rooms Online";

        Debug.Log("Client has been connected to Photon ! " + PhotonNetwork.CountOfRooms + " rooms Online");

        m_RoomJoinUI.SetActive(true);
        m_LoadLvlButtonUI.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        m_IsConnected = false;
        m_ControlPanel.SetActive(true);
        Debug.LogError("Disconnected. Please check your internet connection: " + cause.ToString());
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            m_LoadLvlButtonUI.SetActive(true);
            m_JoinRoomButtonUI.SetActive(false);
            m_PlayerStatusText.text = "You are Lobby Leader";
        }
        else
        {
            m_PlayerStatusText.text = "Connected to Lobby";
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        Debug.Log(roomList.Count);
    }

    #endregion


    #region Getters/Setters
    public void SetPlayerName() => m_PlayerName = m_PlayerNameInput.text;
    public void SetRoomName() => m_RoomName = m_RoomNameInput.text;
    #endregion
}