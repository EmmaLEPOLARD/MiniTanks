using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Networking.Menus
{
    public class MainMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField] private int m_MaxPlayerPerRoom = 4;
        [SerializeField] GameObject m_FinfOpponentPanel = null;
        [SerializeField] GameObject m_WaitingstatusPanel = null;
        [SerializeField] Text m_WaitingStatusText = null;

        private bool m_IsConnected = false;

        private const string m_GameVersion = "0.1";

        private void Awake() => PhotonNetwork.AutomaticallySyncScene = true;

        public void FindOpponent()
        {
            m_IsConnected = true;
            m_FinfOpponentPanel.SetActive(false);
            m_WaitingstatusPanel.SetActive(true);
            m_WaitingStatusText.text = "searching...";

            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.GameVersion = m_GameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected To  Master");

            if (m_IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            m_WaitingstatusPanel.SetActive(false);
            m_FinfOpponentPanel.SetActive(true);

            Debug.Log($"Disconnected due to: {cause}");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("No clients are looking for an  opponent, creating a new room ");

            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = (byte)m_MaxPlayerPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Client successfully  joined  a room");

            int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

            if (playerCount < 1)
            {
                m_WaitingStatusText.text = "waating for opponent";
                Debug.Log("Client is  waitin  for an opponent");
            }
            else
            {
                m_WaitingStatusText.text = "Oponent   found";
                Debug.Log("Matching is ready to begin");
            }
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount > 1 && PhotonNetwork.CurrentRoom.PlayerCount <= m_MaxPlayerPerRoom)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;

                m_WaitingStatusText.text = "Opponent found";
                Debug.Log("Match is  ready to begin");

                PhotonNetwork.LoadLevel("scene_Main");
            }
        }
    }
}