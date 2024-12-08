using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public void GoStageScene()
    {
        SceneManager.LoadScene("StageScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
