using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{

    public void QuitGame()
    {
      Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
      SceneManager.LoadScene(sceneName);
    }
    

    
}
