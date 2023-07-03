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

    public void SetActiveToggle(GameObject gameObject)
    {

      if(gameObject.activeInHierarchy)
      {
        gameObject.SetActive(false);
      }
      else if(!gameObject.activeInHierarchy)
      {
        gameObject.SetActive(true);
      }
    }

}
