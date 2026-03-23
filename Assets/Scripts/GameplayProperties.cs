using UnityEngine;

public class GameplayProperties : MonoBehaviour
{
    public KeyCode key_QuitGame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(key_QuitGame))
        {
            Application.Quit();
        }
    }
}
