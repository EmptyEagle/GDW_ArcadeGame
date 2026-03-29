using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayProperties : MonoBehaviour
{
    public KeyCode key_QuitGame;
    public KeyCode key_RestartLevel;
    public bool inLevel;
    public string levelSelectSceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(key_QuitGame))
        {
            // Quits to level select menu if in a level
            if (inLevel)
            {
                SceneManager.LoadScene(levelSelectSceneName);
            }
            // If in a menu, quits the game
            else
            {
                Application.Quit();
            }
        }

        // Restart level if in a level and presses the restart button
        if (Input.GetKeyUp(key_RestartLevel) && inLevel)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
