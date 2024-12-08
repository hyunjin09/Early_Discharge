using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public void StartStage(int stageNum)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage"+stageNum.ToString());
    }
}
