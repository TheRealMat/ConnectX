using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Board : MonoBehaviour
{
    public GameObject connectorPrefab;
    public GameObject diskPrefab;
    public GameObject columnMask;

    [Range(1, 50)]
    public int boardColumns = 10;

    [Range(1, 50)]
    public int boardRows = 10;

    private GameObject[,] disks;

    public GameObject[] masks;

    private void Start()
    {
        disks = new GameObject[boardColumns, boardRows];
        masks = new GameObject[boardColumns];
        DrawGrid();
    }

    public bool MaskClicked(int columnRow, Player player)
    {
        return PlaceDisk(columnRow, player);
    }

    public bool PlaceDisk(int columnRow, Player player)
    {
        int position = CheckPlacement(columnRow);
        if (position == -1){
            return false;
        }

        GameObject disk = Instantiate(diskPrefab, new Vector3(columnRow, position, 0), diskPrefab.transform.rotation);
        disk.GetComponent<Disc>().player = player;
        disk.GetComponent<Renderer>().material.color = player.color;

        disks[position, columnRow] = disk;
        return true;
    }

    private int CheckPlacement(int columnRow)
    {
        for (int i = 0; i < boardRows; i++)
        {
            if (disks[i, columnRow] == null)
            {
                return i;
            }
        }
        return -1;
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
        for (int column = 0; column < boardColumns; column++)
        {
            CreateMask(column);

            for (int row = 0; row < boardRows; row++)
            {
                Vector3 position = new Vector3(row, column, 0);
                Instantiate(connectorPrefab, position, Quaternion.identity, this.transform);
            }
        }

    }
}
