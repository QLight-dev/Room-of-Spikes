using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingScreen;

    [SerializeField]
    private GameObject startScreen;

    void Start() { }

    void Update() { }

    public void LoadGame()
    {
        startScreen.SetActive(false);
        loadingScreen.SetActive(true);
        SceneManager.LoadScene("Game");
    }

    public void LoadMenu()
    {
        startScreen.SetActive(false);
        loadingScreen.SetActive(true);
        SceneManager.LoadScene("Start Menu");
    }
}
