using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeAndSceneChange : Singleton<FadeAndSceneChange>
{
    public enum FADE_STATUS
    {
        FADE_IN,FADE_OUT,AUTO,NONE
    }
    public enum SCENE_STATUS
    {
        TITLE,GAME,GAME_OVAR,GAME_CLEAR,AUTO,NONE
    }
    [SerializeField] Image backImg = null;
    /// <summary>フェード処理が終わったかどうかを返す</summary>
    [HideInInspector] public bool FadeStop { get { return fadeStopFlag; }set { FadeStop = fadeStopFlag; } }
    private bool fadeStopFlag = false;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>フェードのみを行う</summary>
    /// <param name="_STATUS">どんなFadeを行うか？</param>
    /// <param name="fadeSpeed">フェード速度。初期値0.02f</param>
    public void FadeSystem(FADE_STATUS _STATUS = FADE_STATUS.NONE, float fadeSpeed = 0.02f)
    {
        StartCoroutine(StartFadeSystem(_STATUS,fadeSpeed));
    }
    private IEnumerator StartFadeSystem(FADE_STATUS _STATUS = FADE_STATUS.NONE,float fadeSpeed = 0.02f)
    {
        CanvasGroup group = GetComponent<CanvasGroup>();
        fadeStopFlag = false;
        switch (_STATUS)
        {
            case FADE_STATUS.FADE_IN:
                while (true)
                {
                    yield return null;
                    if (group.alpha >= 1)
                    {
                        fadeStopFlag = true;
                        break;
                    }
                    else group.alpha += fadeSpeed;
                }
                break;
            case FADE_STATUS.FADE_OUT:
                while (true)
                {
                    yield return null;
                    if (group.alpha <= 0)
                    {
                        fadeStopFlag = true;
                        break;
                    }
                    else group.alpha -= fadeSpeed;
                }
                break;
            case FADE_STATUS.AUTO:
                if (group.alpha == 1)
                {
                    FadeSystem(FADE_STATUS.FADE_OUT);
                }
                else
                {
                    FadeSystem(FADE_STATUS.FADE_IN);
                }
                break;
            case FADE_STATUS.NONE:
                break;
        }
        yield return 0;
    }

    /// <summary>シーン切り替えのみを行う</summary>
    /// <param name="CHANGE">どのシーンに行きたいか？</param>
    public void SceneChange(SCENE_STATUS CHANGE = SCENE_STATUS.NONE)
    {
        string changeName = null;
        var nowSceneName = SceneManager.GetActiveScene().name;
        switch (CHANGE)
        {
            case SCENE_STATUS.TITLE:
                changeName = "Title";
                break;
            case SCENE_STATUS.GAME:
                changeName = "Game";
                break;
            case SCENE_STATUS.GAME_OVAR:
                changeName = "GameOvar";
                break;
            case SCENE_STATUS.GAME_CLEAR:
                changeName = "GameClear";
                break;
            case SCENE_STATUS.AUTO:
                if (nowSceneName == "Title") changeName = "Game";
                else if (nowSceneName == "Game" && GameManager.Instance.isGameClear) changeName = "GameClear";
                else if (nowSceneName == "Game" && GameManager.Instance.isGameOvar) changeName = "GameOvar";
                else changeName = "Title";
                break;
            case SCENE_STATUS.NONE:
                break;
        }
        SceneManager.LoadScene(changeName);
    }

    /// <summary>ソーンが切り替わった時に呼ばれる</summary>
    /// <param name="from">ここから</param>
    /// <param name="to">ここに</param>
    private void SceneChangeEvent(Scene from,Scene to)
    {
        Debug.Log("呼ばれた");
    }

    public void FadeOutChangeSystem(float fadeSpeed = 0.02f)
    {
        StartCoroutine(FadeOutSceneChangeStart(fadeSpeed));
    }
    private IEnumerator FadeOutSceneChangeStart(float fadeSpeed = 0.02f)
    {
        CanvasGroup group = GetComponent<CanvasGroup>();
        fadeStopFlag = false;
        if (fadeStopFlag != true)
        {
            while (true)
            {
                yield return null;
                if (group.alpha >= 1)
                {
                    Debug.Log("ここまで来た");
                    SceneManager.activeSceneChanged += SceneChangeEvent;
                    SceneChange(SCENE_STATUS.AUTO);
                    while (true)
                    {
                        yield return null;
                        if (group.alpha <= 0)
                        {
                            Debug.Log("終わりの到達点");
                            fadeStopFlag = true;
                            break;
                        }
                        else group.alpha -= fadeSpeed;
                    }
                    break;
                }
                else group.alpha += fadeSpeed;
            }
        }
        yield return 0;
    }
}
