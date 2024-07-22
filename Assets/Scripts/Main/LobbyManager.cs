using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using System.Collections.Generic;
using Unity.Services.Authentication;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance { get; private set; }
    public delegate void LobbyListUpdatedDelegate(List<Lobby> lobbies);
    public event LobbyListUpdatedDelegate OnLobbyListUpdated;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AuthenticationService.Instance.SignedIn += OnSignedIn;
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    private void OnSignedIn()
    {
        GetOpenLobbies();
    }

    public async void GetOpenLobbies()
    {
        QueryLobbiesOptions options = new QueryLobbiesOptions
        {
            Filters = new List<QueryFilter>
            {
                new QueryFilter(QueryFilter.FieldOptions.AvailableSlots, "0", QueryFilter.OpOptions.GT)
            }
        };

        QueryResponse response = await LobbyService.Instance.QueryLobbiesAsync(options);
        OnLobbyListUpdated?.Invoke(response.Results);
    }

    public async void CreateLobby(string lobbyName, int maxPlayers, string password)
    {
        CreateLobbyOptions options = new CreateLobbyOptions
        {
            IsPrivate = !string.IsNullOrEmpty(password),
            Data = new Dictionary<string, DataObject>
            {
                { "password", new DataObject(DataObject.VisibilityOptions.Member, password, DataObject.IndexOptions.S1) }
            }
        };

        Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, options);
        Debug.Log("Lobby created with ID: " + lobby.Id);
        GetOpenLobbies();
    }

    public async void JoinLobby(string lobbyId, string password = "")
    {
        JoinLobbyByIdOptions options = new JoinLobbyByIdOptions();

        if (!string.IsNullOrEmpty(password))
        {
            var playerData = new Dictionary<string, PlayerDataObject>
            {
                { "password", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, password) }
            };
            options.Player = new Player { Data = playerData };
        }

        try
        {
            await LobbyService.Instance.JoinLobbyByIdAsync(lobbyId, options);
            Debug.Log("Joined lobby with ID: " + lobbyId);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError("Failed to join lobby: " + e.Message);
            // Handle incorrect password scenario
        }
    }
}
