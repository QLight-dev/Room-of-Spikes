using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField]
    private GameObject LoadingScreen;

    void Start() { }

    void Update() { }

    public void StartGame()
    {
        transform.parent.gameObject.SetActive(false);
        LoadingScreen.SetActive(true);
        SceneManager.LoadScene("Game");
    }
}
