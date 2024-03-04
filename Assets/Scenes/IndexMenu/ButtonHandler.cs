using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField]
    public string nextScene;

    [SerializeField]
    public string previousScene;

    [SerializeField]
    public string scene1;
    public void ExitGame()
    {
        Debug.Log("exit");
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
    public void changeEsceneToNext()
    {
        Debug.Log("cargando escena posterior");
        SceneManager.LoadScene(nextScene);
    }
    public void changeEsceneToPrevious()
    {
        Debug.Log("cargando escena previa");
        SceneManager.LoadScene(previousScene);
    }
    public void changeEsceneToScene1()
    {
        Debug.Log("cargando escena previa");
        SceneManager.LoadScene(scene1);
    }
}
