using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerMenuUI : MonoBehaviour
{
    public GameObject lobbyItemPrefab;
    public Transform content;
    public GameObject createLobbyPopup;
    public GameObject joinLobbyWithPasswordPopup;
    public InputField lobbyNameInput;
    public InputField maxPlayersInput;
    public InputField lobbyPasswordInput;
    public InputField joinLobbyPasswordInput;
    public Button createLobbyButton;
    public Button joinLobbyButton;

    private string currentLobbyId;

    private void Start()
    {
        createLobbyButton.onClick.AddListener(ShowCreateLobbyPopup);
        joinLobbyButton.onClick.AddListener(ShowJoinLobbyWithPasswordPopup);
        LobbyManager.Instance.OnLobbyListUpdated += UpdateLobbyList;
    }

    private void OnDestroy()
    {
        LobbyManager.Instance.OnLobbyListUpdated -= UpdateLobbyList;
    }

    public void ShowCreateLobbyPopup()
    {
        createLobbyPopup.SetActive(true);
    }

    public void ShowJoinLobbyWithPasswordPopup()
    {
        joinLobbyWithPasswordPopup.SetActive(true);
    }

    public void ConfirmJoinLobbyWithPassword()
    {
        string password = joinLobbyPasswordInput.text;
        LobbyManager.Instance.JoinLobby(currentLobbyId, password);
        joinLobbyWithPasswordPopup.SetActive(false);
    }

    private void UpdateLobbyList(List<Lobby> lobbies)
    {
        ClearLobbies();
        foreach (var lobby in lobbies)
        {
            GameObject lobbyItem = Instantiate(lobbyItemPrefab, content);
            LobbyItem lobbyItemScript = lobbyItem.GetComponent<LobbyItem>();
            lobbyItemScript.SetLobbyInfo(lobby.Id, lobby.Name, lobby.Players.Count, lobby.MaxPlayers, lobby.IsPrivate);
            lobbyItemScript.OnJoinLobby += OnJoinLobby;
        }
    }

    private void ClearLobbies()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }

    private void OnJoinLobby(string lobbyId)
    {
        currentLobbyId = lobbyId;
        ShowJoinLobbyWithPasswordPopup();
    }
}
