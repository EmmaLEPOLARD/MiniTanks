using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace Networking.Menus
{
    public class PlayerNameInput : MonoBehaviour
    {
        [SerializeField] InputField m_NameInputField = null;
        [SerializeField] Button m_ContinueButton = null;

        private const string m_PlayerPrefNameKey = "PlayerName";

        private void Start() => SetUpInputField();

        private void SetUpInputField()
        {
            if (!PlayerPrefs.HasKey(m_PlayerPrefNameKey)) { return; }

            string defaultName = PlayerPrefs.GetString(m_PlayerPrefNameKey);

            m_NameInputField.text = defaultName;

            SetPlayerName(defaultName);
        }

        public void SetPlayerName(string _name)
        {
            m_ContinueButton.interactable = !string.IsNullOrEmpty(_name);
        }

        public void SavePlayuerName()
        {
            string playerName = m_NameInputField.text;

            PhotonNetwork.NickName = playerName;
            PlayerPrefs.SetString(m_PlayerPrefNameKey, playerName);
        }
    }
}