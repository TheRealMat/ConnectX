﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{

    public int colorIndex = 0;
    Button button;
    public TeamColors teamColors;

    void Start()
    {
        button = gameObject.GetComponent<Button>();
        CreateColors(colorIndex);
        teamColors.colorsList[colorIndex].available = false;
    }

    private void CreateColors(int index)
    {
        ColorBlock cb = button.colors;

        cb.pressedColor = teamColors.colorsList[colorIndex].color;
        cb.normalColor = teamColors.colorsList[colorIndex].color;
        cb.selectedColor = teamColors.colorsList[colorIndex].color;
        cb.selectedColor = teamColors.colorsList[colorIndex].color;
        cb.highlightedColor = teamColors.colorsList[colorIndex].color;
        button.colors = cb;
    }

    // used for disabled buttons
    public void SetAvailable() 
    {
        teamColors.colorsList[colorIndex].available = true;
    }
    // used to check if color is no longer available when button is reenabled
    public void ResetButton()
    {
        if (teamColors.colorsList[colorIndex].available == true)
        {
            teamColors.colorsList[colorIndex].available = false;
        }
        else
        {
            // this breaks if the color of the button was taken. i need to make it so i can set the color without setting last color to available
            NextColor(false);
        }
        
    }

    // changes color and sets old color to be available
    private void SwitchColor(int i)
    {
        //Debug.Log("colors switching");
        // set old color to be available
        teamColors.colorsList[colorIndex].available = true;
        //Debug.Log($"{colorIndex} set to available");
        // set new color to be taken
        teamColors.colorsList[i].available = false;
        //Debug.Log($"{i} set to NOT available");

        colorIndex = i;
        CreateColors(i);
        return;
    }

    // changes color and does not make any changes to old color (used for getting new color for reenabled button)
    private void SetColor(int i)
    {
        //Debug.Log("colors being set");
        // set new color to be taken
        teamColors.colorsList[i].available = false;
        //Debug.Log($"{i} set to NOT available");

        colorIndex = i;
        CreateColors(i);
        return;
    }
    public void NextColor(bool ChangeOld)
    {

        for (int i = colorIndex + 1; i < teamColors.colorsList.Length + colorIndex + 1; i++)
        {
            //Debug.Log(i);

            // go to back to start of array
            if (i >= teamColors.colorsList.Length)
            {
                i = 0;

                //Debug.Log("went back to start of array");
            }

            // found available color
            if (teamColors.colorsList[i].available == true)
            {
                //Debug.Log("color available");

                if (ChangeOld == true)
                {
                    SwitchColor(i);
                    return;
                }
                else
                {
                    SetColor(i);
                    return;
                }

            }
            // no available color
            else if (i == colorIndex)
            {
                //Debug.Log("color NOT available");
                return;
            }
        }
    }

    public void ButtonPressed()
    {
        //Debug.Log("button was pressed");
        NextColor(true);
    }
}
