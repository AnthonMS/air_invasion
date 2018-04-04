
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {


    public void Awake()
    {
        
        
            GameObject.FindGameObjectWithTag("AudioManager").SendMessage("GameOverSound");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }


}
