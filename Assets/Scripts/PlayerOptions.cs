using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOptions : MonoBehaviour
{
    public Player player;
    private GameManager gm;
    public GameObject addPlayerButton;
    public GameObject removePlayerButton;
    public GameObject playerName;
    public GameObject colorButton;
    private ColorButton colorButtonScript;


    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        colorButtonScript = colorButton.GetComponent<ColorButton>();
    }
    public void AddPlayerPressed()
    {
        gm.players.Add(player);
        addPlayerButton.SetActive(false);
        removePlayerButton.SetActive(true);
        playerName.SetActive(true);
        colorButton.SetActive(true);
        colorButtonScript.ResetButton();
    }

    public void RemovePlayerPressed()
    {
        colorButtonScript.SetAvailable();
        gm.players.Remove(player);
        addPlayerButton.SetActive(true);
        removePlayerButton.SetActive(false);
        playerName.SetActive(false);
        colorButton.SetActive(false);
    }
}
