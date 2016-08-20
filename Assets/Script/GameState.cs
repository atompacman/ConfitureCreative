using UnityEngine;
using System.Collections;

public enum GameStateEnum
{
    Start, Flying, Gameover
}

public class GameState : MonoBehaviour
{

    public GameStateEnum currentState = GameStateEnum.Start;
    public GameObject titleScreen;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartGame()
    {
        // Start flying!
        currentState = GameStateEnum.Flying;
    }

    void OnGUI()
    {
        if (currentState == GameStateEnum.Start && Input.GetButton("Fire1"))
        {
            currentState = GameStateEnum.Flying;
            titleScreen.SetActive(false);
        }
    }
}
