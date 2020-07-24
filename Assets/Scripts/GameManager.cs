﻿using System.Collections;
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
        Vector2 lastPos = Vector2.zero;
        if (board.PlaceDisk(currentPlayer, ref lastPos))
        {
            Player winner = board.EvaluateBoard(lastPos);
            if (winner != null)
            {
                Debug.Log($"{winner.playerName} won! Very cool");
            }
            NextTurn();
        }
    }

    private void Start()
    {
        StartGame();
        board.CreateGhost(currentPlayer);
    }

    public void StartGame()
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
