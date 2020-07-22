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
        gm.CurrentMouseOverColumnRow(index);
        gm.MoveGhost();
    }
    private void OnMouseExit()
    {
        gm.HideGhost();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gm.MaskClicked();
        }
    }

}
