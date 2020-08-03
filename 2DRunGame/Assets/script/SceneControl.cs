using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    /// <summary>
    /// 切換場景
    /// </summary>
    public void ChangeScene()
    {
        Invoke("DelayedChangeScene", 0.5f);
    }
    void DelayedChangeScene()
    {
        SceneManager.LoadScene("遊戲場景");
    }
    /// <summary>
    /// 離開遊戲
    /// </summary>
    public void Quit()
    {
        Invoke("DelayedQuit", 0.5f);
    }
    void DelayedQuit()
    {
        Application.Quit();
    }
}
