using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public Text textbox;

    private Slider theSlider;
    private void Start()
    {
        theSlider = this.GetComponent<Slider>();
    }

    public void UpdateText()
    {
        textbox.text = theSlider.value.ToString();
    }
}
