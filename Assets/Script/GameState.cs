using UnityEngine;
using System.Collections;

public enum GameStateEnum
{
    Start, Flying, Gameover, Win
}

public class GameState : MonoBehaviour
{
    public GameStateEnum currentState = GameStateEnum.Start;
    public HudController hudController;
    public PlayerController playerController;
    public AnimationScript anim;

    private bool buttonPressed = false;

    public static GameState instance;

    GameState getInstance()
    {
        return instance;
    }

    // Use this for initialization
    void Awake()
    {
        // First we check if there are any other instances conflicting
        if (instance != null && instance != this)
        {
            // If that is the case, we destroy other instances
            Destroy(gameObject);
        }
        // Here we save our singleton instance
        instance = this;

        // Furthermore we make sure that we don't destroy between scenes (this is optional)
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Restart()
    {
        GameObject.Find("PaperPlane").GetComponent<Rigidbody>().useGravity = false;
        currentState = GameStateEnum.Start;
        playerController.Reset();
        hudController.Hide();
        hudController.ShowTitle();
        anim.gameObject.SetActive(true);
        anim.Reset(); // Reset la petite animation cute
        Debug.Log("Back to title!");
    }

    public void StartGame()
    {
        // Start flying!
        currentState = GameStateEnum.Flying;
        hudController.Hide();
        anim.gameObject.SetActive(false);
        Debug.Log("Started!");
    }

    public void GameOver()
    {
        GameObject.Find("PaperPlane").GetComponent<Rigidbody>().useGravity = true;
        currentState = GameStateEnum.Gameover;
        hudController.ShowGameOver();
        Debug.Log("Game over!");
    }

    public void GameWin()
    {
        GameObject.Find("PaperPlane").GetComponent<Rigidbody>().useGravity = true;
        currentState = GameStateEnum.Win;
        hudController.ShowGameWin();
        Debug.Log("Bravo!");
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
                else if (currentState == GameStateEnum.Gameover || currentState == GameStateEnum.Win)
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
