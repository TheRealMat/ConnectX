﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Board : MonoBehaviour
{
    public GameObject connectorPrefab;
    public GameObject diskPrefab;
    public GameObject sidePrefab;
    public GameObject topPrefab;
    public GameObject columnMask;
    private CheckWinner checkWinner;
    public Material ghostMaterial;
    private GameObject ghost;

    [Range(1, 50)]
    public int boardColumns = 10;

    [Range(1, 50)]
    public int boardRows = 10;

    private GameObject[,] discs;

    public GameObject[] masks;

    private OrbitCamera orbitCamera;

    private int columnRow;

    public int streak;

    private void Start()
    {
        checkWinner = gameObject.GetComponent<CheckWinner>();
        orbitCamera = FindObjectOfType<OrbitCamera>();
        GenerateBoard();
    }
    public void GenerateBoard()
    {
        // this would be a good time to implement events
        // sets camera's orbit target to middle of board
        orbitCamera.target = new Vector3((boardColumns / 2f) -0.5f, (boardRows / 2f) -0.5f, 0);
        

        // there's probably a more effecient way to do this but it works
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        discs = new GameObject[boardColumns, boardRows];
        masks = new GameObject[boardColumns];
        DrawGrid();
        DrawDetails();
    }

    public void StartPlay()
    {
        GenerateMasks();
    }

    public void CreateGhost(Player player)
    {
        ghost = Instantiate(diskPrefab, Vector3.zero, diskPrefab.transform.rotation);
        ghost.GetComponent<Disc>().player = player;

        UpdateGhostColor(player);

        ghost.SetActive(false);
    }

    public void UpdateGhostColor(Player player)
    {
        Color tmp = player.color;
        tmp.a = 0.5f;

        Renderer rend = ghost.GetComponent<Renderer>();
        rend.material = ghostMaterial;
        rend.material.color = tmp;
    }

    public void MoveGhost()
    {
        Vector3 Position = Vector3.zero;
        if (!GetValidPosition(ref Position))
        {
            ghost.SetActive(false);
            return;
        }
        ghost.SetActive(true);
        ghost.transform.position = Position;

    }

    public void HideGhost()
    {
        ghost.SetActive(false);
    }

    public void UpdateCurrentMouseOverColumnRow(int columnRow)
    {
        this.columnRow = columnRow;
    }

    public bool PlaceDisk(Player player, ref Vector2 lastPos)
    {
        Vector3 Position = Vector3.zero;
        if (!GetValidPosition(ref Position)){
            return false;
        }

        GameObject disk = Instantiate(diskPrefab, Position, diskPrefab.transform.rotation);
        disk.GetComponent<Disc>().player = player;
        disk.GetComponent<Renderer>().material.color = player.color;

        discs[columnRow, (int)Position.y] = disk;
        lastPos = new Vector2(columnRow, (int)Position.y);
        return true;
    }

    private bool GetValidPosition(ref Vector3 vector)
    {
        // Height
        for (int y = 0; y < boardRows; y++)
        {
            // Width (columnRow) is already set
            if (discs[columnRow, y] == null)
            {
                vector = new Vector3(columnRow, y, 0);
                return true;
            }
        }
        return false;
    }

    private void GenerateMasks()
    {
        for (int column = 0; column < boardColumns; column++)
        {
            CreateMask(column);
        }
    }

    private void CreateMask(int index)
    {
        Vector3 position = new Vector3(index, -0.5f, 0);
        GameObject mask = Instantiate(columnMask, position, Quaternion.identity, this.transform);
        // sets mask to be as tall as the board
        mask.transform.localScale += new Vector3(0, boardRows -1, 0);

        MaskClick tmp = mask.gameObject.transform.GetChild(0).GetComponent<MaskClick>();

        tmp.index = index;

        masks[index] = mask;
    }


    private void DrawGrid()
    {
        // Width
        for (int column = 0; column < boardColumns; column++)
        {

            // Height
            for (int row = 0; row < boardRows; row++)
            {
                Vector3 position = new Vector3(column, row, 0);
                Instantiate(connectorPrefab, position, Quaternion.identity, this.transform);
            }
        }
    }

    private void DrawDetails()
    {
        int row;
        // left
        for (row = 0; row < boardRows; row++)
        {
            Vector3 position = new Vector3(-1, row, 0);
            Instantiate(sidePrefab, position, sidePrefab.transform.rotation, this.transform);
        }
        Instantiate(topPrefab, new Vector3(-1, boardRows + topPrefab.transform.position.y, 0), topPrefab.transform.rotation, this.transform);
        row = 0;

        // right
        for (row = 0; row < boardRows; row++)
        {
            Vector3 position = new Vector3(boardColumns, row, 0);
            Instantiate(sidePrefab, position, sidePrefab.transform.rotation, this.transform);
        }
        Instantiate(topPrefab, new Vector3(boardColumns, boardRows + topPrefab.transform.position.y, 0), Quaternion.Euler(90, 0, 0), this.transform);
    }

    public Player EvaluateBoard(Vector2 lastPas)
    {
        Vector2[] rules = {
            // Horizontal
            new Vector2(0, 1),
            new Vector2(0, -1),
            
            // Vertical
            new Vector2(1, 0),
            new Vector2(-1, 0),

            // Diagonal
            new Vector2(1, 1),
            new Vector2(-1, -1),

            new Vector2(1, -1),
            new Vector2(-1, 1)
        };
        
        for (int i = 0; i < rules.Length; i += 2)
        {
            streak = checkWinner.CheckForWinner(discs, lastPas, rules[i]);
            streak += checkWinner.CheckForWinner(discs, lastPas, rules[i + 1]);

            // + 1 because current disk has to be counted too
            if (streak + 1 >= checkWinner.connectX)
            {
                // add win code
                Debug.Log("a player has won");
            }
        }

        return null;
    }
}
