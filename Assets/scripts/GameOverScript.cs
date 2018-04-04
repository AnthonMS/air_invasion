
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {




    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }


}
