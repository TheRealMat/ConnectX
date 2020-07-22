﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Board : MonoBehaviour
{
    public GameObject connectorPrefab;
    public GameObject diskPrefab;
    public GameObject columnMask;
    private CheckWinner checkWinner;

    public Material ghostMaterial;
    private GameObject ghost;

    [Range(1, 50)]
    public int boardColumns = 10;

    [Range(1, 50)]
    public int boardRows = 10;

    private GameObject[,] disks;

    public GameObject[] masks;


    private int columnRow;


    private void Start()
    {
        checkWinner = gameObject.GetComponent<CheckWinner>();
        disks = new GameObject[boardColumns, boardRows];
        masks = new GameObject[boardColumns];
        
        DrawGrid();
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

    public bool PlaceDisk(Player player)
    {
        Vector3 Position = Vector3.zero;
        if (!GetValidPosition(ref Position)){
            return false;
        }

        GameObject disk = Instantiate(diskPrefab, Position, diskPrefab.transform.rotation);
        disk.GetComponent<Disc>().player = player;
        disk.GetComponent<Renderer>().material.color = player.color;

        disks[columnRow, (int)Position.y] = disk;
        return true;
    }

    private bool GetValidPosition(ref Vector3 vector)
    {
        // Height
        for (int y = 0; y < boardRows; y++)
        {
            // Width (columnRow) is already set
            if (disks[columnRow, y] == null)
            {
                vector = new Vector3(columnRow, y, 0);
                return true;
            }
        }
        return false;
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
            CreateMask(column);

            // Height
            for (int row = 0; row < boardRows; row++)
            {
                Vector3 position = new Vector3(column, row, 0);
                Instantiate(connectorPrefab, position, Quaternion.identity, this.transform);
            }
        }
    }

    public void EvaluateBoard()
    {
        checkWinner.EvaluateBoard(disks);
    }
}