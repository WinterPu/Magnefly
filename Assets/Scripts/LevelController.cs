using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : Singleton<LevelController>
{
    public Image mask;

    private void Awake()
    {
        ResetGlobal();
        StartCoroutine(FadeIn());
    }

    public void LoadLevel(int level)
    {
        StartCoroutine(FadeOutAndLoad(level));
    }

    IEnumerator FadeIn()
    {
        float progress = 0;
        var c = mask.color;
        c.a = progress;
        Color from = mask.color;
        Color to = c;
        for (; progress < 1; progress += 0.01f)
        {
            c = Color.Lerp(from, to, progress);
            mask.color = c;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator FadeOutAndLoad(int level)
    {
        float progress = 0;
        var c = mask.color;
        c.a = progress;
        Color from = c;
        c.a = 1;
        Color to = c;
        for (; progress < 1; progress += 0.01f)
        {
            c = Color.Lerp(from, to, progress);
            mask.color = c;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    void ResetGlobal()
    {
        Global.CountOnTheElevator = 0;
    }
}
