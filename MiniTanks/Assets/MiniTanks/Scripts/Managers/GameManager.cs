using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Photon.Pun.Demo.PunBasics
{


    public class GameManager : MonoBehaviourPunCallbacks
    {

        [SerializeField] List<Transform> m_PlayerSpawnPostition = new List<Transform>();
         string m_PlayerPrefabs = "Player0";
        List<GameObject> m_Players = new List<GameObject>();

        void Start()
        {
            returnMenu();

            if (PlayerManager.LocalPlayerInstance == null)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    Debug.Log("Instantiating Player 1");
                   GameObject go = PhotonNetwork.Instantiate(m_PlayerPrefabs, m_PlayerSpawnPostition[0].position, m_PlayerSpawnPostition[0].rotation);
                    m_Players.Add(go);
                    go.GetComponentInChildren<Text>().text = PhotonNetwork.LocalPlayer.NickName;
                }
                else
                {

                    for (int i = 1; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
                    {
                        GameObject go = PhotonNetwork.Instantiate(m_PlayerPrefabs, m_PlayerSpawnPostition[i].position, m_PlayerSpawnPostition[i].rotation);
                        m_Players.Add(go);
                        go.GetComponentInChildren<Text>().text = PhotonNetwork.LocalPlayer.NickName;
                    }
                }
            }



        }


        /// <summary>
        /// Check if client is connected else return to main Menu
        /// </summary>
        void returnMenu()
        {
            if (!PhotonNetwork.IsConnected)
            {
                SceneManager.LoadScene(0, LoadSceneMode.Single);
                return;
            }
        }

        void Update()
        {

        }
    }
}