using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public VideoPlayer videoPlayer;

    public GameObject[] eventPanels;
    public bool[] eventsProgress;
    public int currentEvent = 0;
    
    public string nextSceneName;
    void Start()
    {
        videoPlayer.loopPointReached += EndCutScene;
    }

    // Update is called once per frame
    void Update()
    {
        VideoEvent(31, eventPanels[0], 0);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeSceneManager.instance.ChangeScene(nextSceneName);
        }
    }

    public void VideoEvent(int time, GameObject go, int eventIndex)
    {
        if(videoPlayer.time > time && eventsProgress[eventIndex] == false)
        {
            eventsProgress[eventIndex] = true;
            videoPlayer.Pause();
            go.SetActive(true);
        }
    }

    public void EndCutScene(VideoPlayer vp)
    {
        //if (videoPlayer.frame > videoPlayer.frameCount)
        //{
        //    //Video has finshed playing!

        //}
        ChangeSceneManager.instance.ChangeScene(nextSceneName);
        
    }

    public void ResumeVideo()
    {
        currentEvent++;
        videoPlayer.Play();

    }

    public void ShowText(GameObject go)
    {
        go.SetActive(true);
        StartCoroutine(TextDisappear(3, go));
    }

    public IEnumerator TextDisappear(int time, GameObject go)
    {
        yield return new WaitForSeconds(time);
        go.SetActive(false);
    }
}
