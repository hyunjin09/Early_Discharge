using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void GoStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void GoStageScene()
    {
        SceneManager.LoadScene("StageScene");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    public void StartStage(int stageNum)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage"+stageNum.ToString());
    }
}
