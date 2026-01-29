using UnityEngine;
using Fusion;
using System.Collections.Generic;
using TMPro;
using System.Linq;

public class NetworkedGameManager : NetworkBehaviour
{
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = null;
    private NetworkSessionManager networkSessionManager;
    [SerializeField] private NetworkPrefabRef playerPrefab;

    private int maxPlayers = 2;
    private int timerBeforeStart = 3;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI playerCountText;

    private void Awake()
    {
        networkSessionManager = GetComponent<NetworkSessionManager>();
    }

    public override void Spawned()
    {
        base.Spawned();
        NetworkSessionManager.Instance.OnPlayerJoinedEvent += OnPlayerJoined;
        NetworkSessionManager.Instance.OnPlayerLeftEvent += OnPlayerLeft;
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        NetworkSessionManager.Instance.OnPlayerJoinedEvent -= OnPlayerJoined;
        NetworkSessionManager.Instance.OnPlayerLeftEvent -= OnPlayerLeft;
    }

    public override void FixedUpdateNetwork()
    {
        
        playerCountText.text = $"Players: {Object.Runner.ActivePlayers.Count()}/{maxPlayers}";
    }

    public override void Render()
    {
        base.Render();
    }

    private void OnPlayerJoined(PlayerRef player)
    {
        if (!HasStateAuthority) return;
        if (networkSessionManager.JoinedPlayers.Count >= maxPlayers)
        {
            // Game Logic
            OnGameStarted();
        }
        Debug.Log($"Player{player.PlayerId} joined");
        
    }
    private void OnPlayerLeft(PlayerRef player)
    {
        if (!HasStateAuthority) return;
        if (!_spawnedCharacters.TryGetValue(player, out var playerObject)) return;
        Object.Runner.Despawn(playerObject);
        _spawnedCharacters.Remove(player);
    }

    private void OnGameStarted()
    {
        Debug.Log("Game Started");
        foreach(var player in networkSessionManager.JoinedPlayers)
        {
            var networkObj = Object.Runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, player);
            _spawnedCharacters.Add(player, networkObj);
        }
    }
}



