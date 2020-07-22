using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskClick : MonoBehaviour
{

    public GameManager gm;
    public int index;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

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
            //board.MaskClicked(index);
            gm.MaskClicked(index);
        }
    }

}
