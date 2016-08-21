using UnityEngine;
using System.Collections;

public class HudController : MonoBehaviour {

    public GameObject titleScreen;
    public GameObject gameOverScreen;

    // Use this for initialization
    void Start()
    {
        Hide();
        ShowTitle();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowTitle()
    {
        titleScreen.SetActive(true);
    }

    public void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void ShowBravo()
    {
        //bravoScreen.SetActive(true);
    }

    public void Hide()
    {
        titleScreen.SetActive(false);
        gameOverScreen.SetActive(false);
    }

}
