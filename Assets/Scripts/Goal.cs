using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public int playersNeeded;
    private List<GameObject> playersInGoal;
    private string levelSelectSceneName = "Scene_LevelSelect";
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playersNeeded = 2;
        playersInGoal = new List<GameObject>();
    }

    void Update()
    {
        if (playersNeeded <= 0)
        {
            SceneManager.LoadScene(levelSelectSceneName);
        }
    }

    public void PlayerEnteringGoal(GameObject playerEntering)
    {
        if (!playersInGoal.Contains(playerEntering))
        {
            playersNeeded--;
            Debug.Log(playerEntering.transform.name + " entered goal");
            playersInGoal.Add(playerEntering);
        }
    }

    public void PlayerLeavingGoal(GameObject playerLeaving)
    {
        if (playersInGoal.Contains(playerLeaving))
        {
            playersNeeded++;
            Debug.Log(playerLeaving.transform.name + " left goal");
            playersInGoal.Remove(playerLeaving);
        }
    }
}
