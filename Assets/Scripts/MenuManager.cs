using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string selectedGameName;

    public void onGameButtonClick(string name)
    {
        selectedGameName = name;
        // TODO: move Tofu
    }

    public void onPlayButtonClick()
    {
        SceneManager.LoadScene(selectedGameName);
    }

}
