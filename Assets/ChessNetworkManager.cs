using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessNetworkManager : NetworkManager
{
    public TerrainManager board;
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        print("Onclientconnect");

    }
    public override void OnStartServer()
    {
        base.OnStartServer();
        print("OnStartServer");
    }
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // add player at correct spawn position
        GameObject player = Instantiate(playerPrefab, new Vector3(),Quaternion.identity);
        InputManager.Turn color= InputManager.Turn.White;
        if (numPlayers == 0)
        {
            color = InputManager.Turn.White;
        }
        else if (numPlayers == 1)
        {
            color = InputManager.Turn.Black;
        }
        else
        {
            print("error");
        }
        player.GetComponent<InputManager>().color = color;

        NetworkServer.AddPlayerForConnection(conn, player);
        foreach (Piece piece in board.Board.Values)
        {

            if ((int)piece.color == (int)color)
                piece.GetComponent<NetworkIdentity>().AssignClientAuthority(conn);
        }
        if (color == InputManager.Turn.Black)
        {
            board.CreatePieces(player, false);
        }
        else
        {
            board.CreatePieces(player, true);
        }
        
        // start the game
        if (numPlayers == 2)
        {
            board.asStarted=true;
        }
    }
}
