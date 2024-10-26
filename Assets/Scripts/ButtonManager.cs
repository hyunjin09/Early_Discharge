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
        Debug.Log("Quit Game");
    }
    public void StartStage(int stageNum)
    {
        SceneManager.LoadScene("Stage"+stageNum.ToString());
    }
}
