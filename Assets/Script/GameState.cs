using UnityEngine;
using System.Collections;

public enum GameStateEnum
{
    Start, Flying, Gameover
}

public class GameState : MonoBehaviour
{
    public GameStateEnum currentState = GameStateEnum.Start;
    public HudController hudController;
    public PlayerController playerController;

    private bool buttonPressed = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Restart()
    {
        currentState = GameStateEnum.Start;
        playerController.Reset();
        hudController.Hide();
        hudController.ShowTitle();
        Debug.Log("Back to title!");
    }

    public void StartGame()
    {
        // Start flying!
        currentState = GameStateEnum.Flying;
        hudController.Hide();
        Debug.Log("Started!");
    }

    public void GameOver()
    {
        currentState = GameStateEnum.Gameover;
        hudController.ShowGameOver();
        Debug.Log("Game over!");
    }

    void OnGUI()
    {
        if (Input.GetButton("Fire1"))
        {
            if(!buttonPressed)
            {
                if (currentState == GameStateEnum.Start)
                {
                    StartGame();
                }
                else if (currentState == GameStateEnum.Gameover)
                {
                    Restart();
                }
            }
            buttonPressed = true;
        }
        else
        {
            buttonPressed = false;
        }
    }
}
