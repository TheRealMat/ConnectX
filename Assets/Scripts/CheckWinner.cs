using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinner : MonoBehaviour
{
    public int connectX = 4;

    private int[,] evaluated;

    public Player EvaluateBoard(GameObject[,] discs)
    {
        for (int column = 0; column < discs.GetLength(0); column++)
        {
            for (int row = 0; row < discs.GetLength(1); row++)
            {
                GameObject disc = discs[column, row];

                // No disc here 😫😫😫😫😫
                if (disc == null)
                {
                    break;
                }
                // We have a disc
                // Get the owner 😎
                Player discOwner = disc.GetComponent<Disc>().player;
                
                // Check its neighbors
                // Vertical
                // Up

                // Horizontal
                // Right


                // Diagonal 
                //LeftUp (no need to check down because we only check up ( •_•)>⌐■-■ (⌐■_■))

                //RightUp



            }
        }
        

        return null;
    }

}
