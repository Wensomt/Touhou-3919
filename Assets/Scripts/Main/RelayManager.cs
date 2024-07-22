using Unity.Services.Relay.Models;
using Unity.Services.Relay;
using Unity.Netcode;
using UnityEngine;
using Unity.Netcode.Transports.UTP;

public class RelayManager : MonoBehaviour
{
    public async void CreateRelay()
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(5);
        string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        Debug.Log("Relay created with join code: " + joinCode);

        UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.SetRelayServerData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
                                      allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData);
        NetworkManager.Singleton.StartHost();
    }

    public async void JoinRelay(string joinCode)
    {
        JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

        UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.SetRelayServerData(joinAllocation.RelayServer.IpV4, (ushort)joinAllocation.RelayServer.Port,
                                      joinAllocation.AllocationIdBytes, joinAllocation.Key, joinAllocation.ConnectionData,
                                      joinAllocation.HostConnectionData);
        NetworkManager.Singleton.StartClient();
    }
}
