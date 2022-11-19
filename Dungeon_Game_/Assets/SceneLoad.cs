using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    private Timer _timer;
    // called zero
    private void Awake()
    {
        _timer = GameObject.Find("/Timer").GetComponent<Timer>();
        // Debug.Log(_timer);
    }

    // called first
    private void OnEnable()
    {
        // Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _timer.DisplayTime(DataStorage._TimeLeft);
        // Debug.Log(DataStorage._TimeLeft);
    }

    // called when the game is terminated
//     private void OnDisable()
//     {
//         Debug.Log("OnDisable");
//         SceneManager.sceneLoaded -= OnSceneLoaded;
//     }
}