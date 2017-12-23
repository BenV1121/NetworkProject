using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class CustomNetworkManager : NetworkManager {

    //Gets the central
    public multyPLayerCamera cameraRef;

    //
    // Summary:
    //     ///
    //     The name of the current network scene.
    //     ///
    public static string c_networkSceneName;
    //
    // Summary:
    //     ///
    //     The NetworkManager singleton object.
    //     ///
    public static NetworkManager c_singleton;
    //
    // Summary:
    //     ///
    //     The name of the current match.
    //     ///
    [SerializeField]
    public string c_matchName;
    //
    // Summary:
    //     ///
    //     The maximum number of players in the current match.
    //     ///
    [SerializeField]
    public uint c_matchSize;
    //
    // Summary:
    //     ///
    //     True if the NetworkServer or NetworkClient isactive.
    //     ///
    public bool c_isNetworkActive;
    //
    // Summary:
    //     ///
    //     The current NetworkClient being used by the manager.
    //     ///
    public NetworkClient c_client;
    //
    // Summary:
    //     ///
    //     A MatchInfo instance that will be used when StartServer() or StartClient() are
    //     called.
    //     ///
    public MatchInfo c_matchInfo;
    //
    // Summary:
    //     ///
    //     The UMatch MatchMaker object.
    //     ///
    public NetworkMatch c_matchMaker;
    //
    // Summary:
    //     ///
    //     The list of matches that are available to join.
    //     ///
    public List<MatchInfoSnapshot> c_matches;


    public CustomNetworkManager() : base()
    {
        networkSceneName = c_networkSceneName;
        singleton = c_singleton;
        matchName = c_matchName;
        matchSize = c_matchSize;
        isNetworkActive = c_isNetworkActive;
        client = c_client;
        matchInfo = c_matchInfo;
        matchMaker = c_matchMaker;
        matches = c_matches;
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        //cameraRef.targets.Add(Network.connections[conn.connectionId].)

        base.OnClientConnect(conn);
    }

}
