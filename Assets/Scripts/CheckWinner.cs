using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinner : MonoBehaviour
{
    public int connectX = 4;

    //private int[,] evaluated;

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


                // check if it's possible to win from current position
                if (!(row + connectX > discs.GetLength(1)))
                {
                    int count = 0;
                    // iterate upwards
                    for (int y = row; y < discs.GetLength(1); y++)
                    {
                        GameObject disc_target = discs[column, y];
                        if (disc_target == null)
                        {
                            // Nothing above us
                            break;
                        }
                        Player disc_target_owner = disc_target.GetComponent<Disc>().player;
                        if (discOwner == disc_target_owner)
                        {
                            count++;
                            if (count >= connectX)
                            {
                                Debug.Log($"{discOwner.name} won!");
                                return discOwner;
                            }
                        }
                        else 
                        {
                            count = 0;
                        }
                    }
                }

                // Horizontal
                // Right


                // Diagonal 
                //LeftUp (no need to check down because we only check up ( •_■))

                //RightUp



            }
        }
        

        return null;
    }

}
