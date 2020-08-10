using UnityEngine;
using UnityEngine.SceneManagement;

public static class GlobalVars
{
    [Range(0,1)]
    public static float musicVol = 0.8f;
    [Range(0, 1)]
    public static float effectVol = 0.5f;

}

public class SceneControl : MonoBehaviour
{
    public AudioSource bg_audio;

    private void Start()
    {
        bg_audio.volume = GlobalVars.musicVol;
    }
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
