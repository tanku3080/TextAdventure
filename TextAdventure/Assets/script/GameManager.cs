using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector] public bool isGameOvar = false;
    [HideInInspector] public bool isGameClear = false;
    /// <summary>今のチャプター</summary>
    public int nowChapter = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>UIをアクティブ非アクティブ化する関数</summary>
    /// <param name="obj">指定のオブジェクト</param>
    /// <param name="isActive">trueならアクティブ、falseなら非アクティブ</param>
    public void UiActiveSystem(GameObject obj,bool isActive = true)
    {
        obj.SetActive(isActive);
    }
}
