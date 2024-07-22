using UnityEngine;
using UnityEngine.UI;

public class LobbyItem : MonoBehaviour
{
    public Text lobbyNameText;
    public Text playerCountText;
    public Button joinButton;
    private string lobbyId;
    public delegate void JoinLobbyDelegate(string lobbyId);
    public event JoinLobbyDelegate OnJoinLobby;

    public void SetLobbyInfo(string id, string name, int currentPlayers, int maxPlayers, bool isPrivate)
    {
        lobbyId = id;
        lobbyNameText.text = name;
        playerCountText.text = $"{currentPlayers}/{maxPlayers}";
        joinButton.onClick.AddListener(() => OnJoinLobby?.Invoke(lobbyId));
    }
}
