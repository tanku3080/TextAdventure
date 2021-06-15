using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector] public bool isGameOvar = false;
    [HideInInspector] public bool isGameClear = false;
    private int nowChapter = 1;
    /// <summary>今のチャプター</summary>
    public int Chapter { get { return nowChapter; } set { Chapter = nowChapter; } }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StageButton1()
    {
        Debug.Log("押された");
        nowChapter = 1;
        FadeAndSceneChange.Instance.FadeSystem(FadeAndSceneChange.FADE_STATUS.AUTO);
        FadeAndSceneChange.Instance.SceneChange(FadeAndSceneChange.SCENE_STATUS.AUTO);
    }
    public void StageButton2()
    {
        nowChapter = 2;
        FadeAndSceneChange.Instance.FadeSystem(FadeAndSceneChange.FADE_STATUS.AUTO);
        FadeAndSceneChange.Instance.SceneChange(FadeAndSceneChange.SCENE_STATUS.AUTO);
    }
    public void StageButton3()
    {
        nowChapter = 3;
    }
    public void StageButton4()
    {
        nowChapter = 4;
    }
}
