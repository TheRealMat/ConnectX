﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinner : MonoBehaviour
{
    public int connectX = 4;


    public int CheckForWinner(GameObject[,] discs, Vector2 position, Vector2 steps)
    {

        int streak = 0;
   
        // The newly placed disc
        GameObject disc = discs[(int)position.x, (int)position.y];
        Player player = disc.GetComponent<Disc>().player;
        
        // Skip the first index because it's the object itself
        for (int i = 1; i < connectX; i++)
        {
            position.x += steps.x;
            position.y += steps.y;

            // Out of bounds check
            if (position.x < 0 || position.y < 0 || position.x >= discs.GetLength(0) || position.y >= discs.GetLength(1))
            {
                return streak;
            }

            GameObject cursor = discs[(int)position.x, (int)position.y];
            // Nothing here 😫😫😫😫😫
            if (cursor == null)
            {
                return streak;
            }

            Player cursorOwner = cursor.GetComponent<Disc>().player;

            if (cursorOwner != player)
            {
                return streak;
            }

            streak++;
        }

        return streak;
    }



}
