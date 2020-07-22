using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Player> players;
    public Board board;

    Player currentPlayer;
    int currentTurnIndex = 0;



    public void MaskClicked(int index)
    {
        if (board.MaskClicked(index, currentPlayer))
        {
            NextTurn();
        }
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        UpdatePlayerTurn();
    }

    private void NextTurn()
    {
        currentTurnIndex = (currentTurnIndex+1)%players.Count;
        UpdatePlayerTurn();
    }

    private void UpdatePlayerTurn()
    {
        currentPlayer = players[currentTurnIndex];
    }
}
