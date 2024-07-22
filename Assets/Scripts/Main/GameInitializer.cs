using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    async void Start()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        Debug.Log("Initialized and authenticated.");
    }
}
