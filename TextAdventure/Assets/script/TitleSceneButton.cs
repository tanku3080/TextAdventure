using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneButton : MonoBehaviour
{
    /// <summary>スタートUIの後に表示されるボタン群の親オブジェクト</summary>
    [SerializeField] GameObject startAfterButton = null;
    [SerializeField] GameObject stageSelectButtonObjs = null;
    [SerializeField] GameObject Ok_NoButton = null;
    [SerializeField] GameObject iamgeObj = null;
    [SerializeField] CanvasGroup starButtonObject = null;
    [SerializeField] CanvasGroup titleImageGroup = null;
    [SerializeField] AudioClip titleMusic = null;
    [SerializeField] AudioClip clickSfx = null;
    [SerializeField] AudioSource source = null;
    bool musicPlayFalg = false;
    bool secondFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.UiActiveSystem(Ok_NoButton,false);
        GameManager.Instance.UiActiveSystem(stageSelectButtonObjs, false);
        GameManager.Instance.UiActiveSystem(startAfterButton,false);
        starButtonObject.alpha = 0;
        FadeAndSceneChange.Instance.FadeSystem(FadeAndSceneChange.FADE_STATUS.FADE_OUT,0.001f);
        titleImageGroup.alpha = 0f;
        source.clip = titleMusic;
        source.loop = true;
        secondFlag = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (FadeAndSceneChange.Instance.FadeStop && musicPlayFalg != true)
        {
            musicPlayFalg = true;
            source.Play();
            FadeAndSceneChange.Instance.FadeSystem(FadeAndSceneChange.FADE_STATUS.FADE_IN,0.001f,titleImageGroup);
            secondFlag = true;
        }
        if (titleImageGroup.alpha == 1 && secondFlag)
        {
            secondFlag = false;
            FadeAndSceneChange.Instance.FadeSystem(FadeAndSceneChange.FADE_STATUS.FADE_IN,0.02f,starButtonObject);
        }

        if (starButtonObject.alpha == 1)
        {
            Debug.Log("スタートボタンがアクティブ");
            if (Input.GetKeyUp(KeyCode.Return) && startAfterButton.activeSelf == false)
            {
                source.PlayOneShot(clickSfx);
                GameManager.Instance.UiActiveSystem(starButtonObject.gameObject,false);
                GameManager.Instance.UiActiveSystem(startAfterButton);
            }
        }
    }
    /// <summary>新しくゲームを始める時</summary>
    public void NewGameButton()
    {
        source.PlayOneShot(clickSfx);
        //今後、既にデータがある場合に確認文が出るようにする
        FadeAndSceneChange.Instance.FadeOutChangeSystem(0.001f);
    }
    /// <summary>途中からゲームを始める</summary>
    public void ContinueButton()
    {
        source.PlayOneShot(clickSfx);
    }
    /// <summary>stage選択するとき</summary>
    public void StageSelectButtonParent()
    {
        source.PlayOneShot(clickSfx);
        GameManager.Instance.UiActiveSystem(startAfterButton,false);
        GameManager.Instance.UiActiveSystem(stageSelectButtonObjs);
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
        FadeAndSceneChange.Instance.FadeOutChangeSystem(0.005f,FadeAndSceneChange.SCENE_STATUS.GAME);
    }
    public void NoButton()
    {
        GameManager.Instance.UiActiveSystem(Ok_NoButton,false);
        GameManager.Instance.UiActiveSystem(stageSelectButtonObjs);
    }
}
