using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class OPScript : MonoBehaviour
{
    [SerializeField] VideoPlayer videoOBJ = null;
    [SerializeField] GameObject warningObj = null;
    [SerializeField] CanvasGroup group = null;
    [SerializeField] GameObject skipObj = null;
    [SerializeField] GameObject backGround = null;
    [SerializeField] AudioClip pushSFX = null;
    [SerializeField] AudioSource source = null;
    void Start()
    {
        skipObj.SetActive(false);
        backGround.SetActive(true);
        videoOBJ.loopPointReached += VideoEnd;
        StartCoroutine(WaeninngObjFade());
    }

    // Update is called once per frame
    void Update()
    {
        if (videoOBJ.isPlaying)
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                videoOBJ.Pause();
                skipObj.SetActive(true);
            }
        }
    }
    void VideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("Title");
    }

    IEnumerator WaeninngObjFade()
    {
        while (true)
        {
            yield return null;
            if (group.alpha >= 1)
            {
                yield return new WaitForSeconds(4);
                while (true)
                {
                    yield return null;
                    if (group.alpha <= 0) break;
                    else group.alpha -= 0.001f;
                }
                break;
            }
            else group.alpha += 0.001f;
        }
        OPStart();
        yield return 0;
    }
    public void YesObj()
    {
        source.PlayOneShot(pushSFX);
        SceneManager.LoadScene("Title");
    }
    public void NoObj()
    {
        skipObj.SetActive(false);
        videoOBJ.Play();
    }

    private void OPStart()
    {
        videoOBJ.Play();
        backGround.SetActive(false);
    }
}
