using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSctipt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FadeAndSceneChange.Instance.FadeSystem(FadeAndSceneChange.FADE_STATUS.FADE_OUT);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            FadeAndSceneChange.Instance.FadeOutChangeSystem();
        }
    }
}
