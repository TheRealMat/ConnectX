using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskClick : MonoBehaviour
{

    public Board board;
    public int index;


    private void OnMouseEnter()
    {
        // show ghost
    }
    private void OnMouseExit()
    {
        // stop showing ghost
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            board.MaskClicked(index);
        }
    }

}
