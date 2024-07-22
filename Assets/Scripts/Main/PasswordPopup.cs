using UnityEngine;
using UnityEngine.UI;

public class PasswordPopup : MonoBehaviour
{
    public InputField passwordInput;
    public Button confirmButton;
    private string lobbyId;
    private bool isJoin;

    private void Start()
    {
        confirmButton.onClick.AddListener(OnConfirmPassword);
    }

    public void Show(string id, bool join)
    {
        lobbyId = id;
        isJoin = join;
        gameObject.SetActive(true);
    }

    public void OnConfirmPassword()
    {
        string password = passwordInput.text;
        if (isJoin)
        {
            FindObjectOfType<LobbyManager>().JoinLobby(lobbyId, password);
        }
        else
        {
            FindObjectOfType<LobbyManager>().CreateLobby(); // Adjusted to call the correct method
        }
        gameObject.SetActive(false);
    }
}
