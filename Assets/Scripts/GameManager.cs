using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Player> players;
    public Board board;

    Player currentPlayer;
    int currentTurnIndex = 0;



    public void MaskClicked()
    {
        if (board.PlaceDisk(currentPlayer))
        {
            NextTurn();
        }
    }

    private void Start()
    {
        StartGame();
        board.CreateGhost(currentPlayer);
    }

    private void StartGame()
    {
        UpdateCurrentPlayer();
    }

    private void NextTurn()
    {
        currentTurnIndex = (currentTurnIndex+1)%players.Count;
        UpdateCurrentPlayer();
        board.UpdateGhostColor(currentPlayer);
        MoveGhost();
    }

    private void UpdateCurrentPlayer()
    {
        currentPlayer = players[currentTurnIndex];
    }

    public void MoveGhost()
    {
        board.MoveGhost();
    }

    public void HideGhost()
    {
        board.HideGhost();
    }

    public void CurrentMouseOverColumnRow(int columnRow)
    {
        board.UpdateCurrentMouseOverColumnRow(columnRow);
    }
}
