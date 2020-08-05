using System.Collections;
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
        SetColor(colorIndex);
        teamColors.colorsList[colorIndex].available = false;
    }

    private void SetColor(int index)
    {
        ColorBlock cb = button.colors;

        cb.pressedColor = teamColors.colorsList[colorIndex].color;
        cb.normalColor = teamColors.colorsList[colorIndex].color;
        cb.selectedColor = teamColors.colorsList[colorIndex].color;
        cb.selectedColor = teamColors.colorsList[colorIndex].color;
        cb.highlightedColor = teamColors.colorsList[colorIndex].color;
        button.colors = cb;
    }

    public void ButtonPressed()
    {

        Debug.Log("button was pressed");

        

        for (int i = colorIndex + 1; i < teamColors.colorsList.Length + colorIndex + 1; i++)
        {
            Debug.Log(i);

            // go to back to start of array
            if ( i >= teamColors.colorsList.Length)
            {
                i = 0;

                Debug.Log("went back to start of array");
            }

            // found available color
            if (teamColors.colorsList[i].available == true)
            {

                Debug.Log("color available");

                // set old color to be available
                teamColors.colorsList[colorIndex].available = true;
                Debug.Log($"{colorIndex} set to available");
                // set new color to be taken
                teamColors.colorsList[i].available = false;
                Debug.Log($"{i} set to NOT available");

                colorIndex = i;
                SetColor(i);
                return;
                
            }
            // no available color
            else if (i == colorIndex)
            {
                Debug.Log("color NOT available");
                return;
            }
        }
    }
}
