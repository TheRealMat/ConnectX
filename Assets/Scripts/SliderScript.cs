using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public Text textbox;
    private Board board;
    private CheckWinner winScript;
    private Slider theSlider;
    private void Start()
    {
        theSlider = this.GetComponent<Slider>();
        board = FindObjectOfType<Board>();
        winScript = FindObjectOfType<CheckWinner>();
    }
    
    private void UpdateBoard()
    {
        board.GenerateBoard();
    }

    public void UpdateText()
    {
        textbox.text = theSlider.value.ToString();
    }

    public void UpdateRows()
    {
        board.boardRows = (int)theSlider.value;
        UpdateBoard();
    }

    public void UpdateColumns()
    {
        board.boardColumns = (int)theSlider.value;
        UpdateBoard();
    }

    public void UpdateConnect()
    {
        winScript.connectX = (int)theSlider.value;
    }
}
