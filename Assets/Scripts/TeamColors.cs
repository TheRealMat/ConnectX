using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamColors : MonoBehaviour
{
    public struct AvailableColor
    {
        public Color color;
        public bool available;
    }

    public AvailableColor[] colorsList = new AvailableColor[]{

        new AvailableColor(){color = new Color(255, 0, 0), available = true}, // red
        new AvailableColor(){color = new Color(0, 255, 0), available = true}, // green
        new AvailableColor(){color = new Color(0, 0, 255), available = true}, // blue

    };

}
