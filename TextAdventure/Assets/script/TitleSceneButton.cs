using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneButton : MonoBehaviour
{
    [SerializeField] GameObject stageSelectButtonObjs = null;
    [SerializeField] GameObject Ok_NoButton = null;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.UiActiveSystem(Ok_NoButton,false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //以下はstageselectのイベントに対応したメソッド
    public void StageButton1()
    {
        GameManager.Instance.nowChapter = 1;
        StageSelectSequence();
    }
    public void StageButton2()
    {
        GameManager.Instance.nowChapter = 2;
        StageSelectSequence();
    }
    public void StageButton3()
    {
        GameManager.Instance.nowChapter = 3;
        StageSelectSequence();
    }
    public void StageButton4()
    {
        GameManager.Instance.nowChapter = 4;
        StageSelectSequence();
    }
    private void StageSelectSequence()
    {
        GameManager.Instance.UiActiveSystem(stageSelectButtonObjs,false);
        GameManager.Instance.UiActiveSystem(Ok_NoButton);
    }

    public void OkButton()
    {
        FadeAndSceneChange.Instance.FadeOutChangeSystem(0.005f);
    }
    public void NoButton()
    {
        GameManager.Instance.UiActiveSystem(Ok_NoButton,false);
        GameManager.Instance.UiActiveSystem(stageSelectButtonObjs);
    }
}
