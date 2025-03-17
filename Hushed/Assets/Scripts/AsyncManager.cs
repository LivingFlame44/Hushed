using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncManager : MonoBehaviour
{
    public static AsyncManager instance;
    [Header("Menu Screens")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject[] menuPanels;

    public Sprite[] loadingImages; 

    public Slider slider;

    public void Awake()
    {
        

        
    }

    public void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
    }

    public List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public void UnloadLevelBtn(string levelToUnload)
    {
        scenesLoading.Clear();

        if (levelToUnload != null)
        {
            scenesLoading.Add(SceneManager.LoadSceneAsync(levelToUnload, LoadSceneMode.Additive));
        }

    }

    public void LoadLevelBtn(string levelToLoad)
    {
        loadingScreen.SetActive(true);

        if (levelToLoad != null)
        {
            scenesLoading.Add(SceneManager.LoadSceneAsync(levelToLoad, LoadSceneMode.Additive));
        }
        
        StartCoroutine(GetSceneLoadProgress());
    }

    IEnumerator LoadLevelAsync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

        while(!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            slider.value = progressValue;
            yield return null;
        }
    }

    float totalSceneProgress;
    IEnumerator GetSceneLoadProgress()
    {
        for(int i = 0; i<scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;
                foreach(AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }

                totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100f;

                slider.value = Mathf.RoundToInt(totalSceneProgress);
                yield return null;
            }
        }

        loadingScreen.SetActive(false);
    }

    //use this
    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        int bgNum = Random.Range(0,loadingImages.Length);
        scene.allowSceneActivation = false;

        loadingScreen.SetActive(true);
        loadingScreen.GetComponentInChildren<Image>().sprite = loadingImages[bgNum];
        do
        {
            await Task.Delay(100);
            slider.value = scene.progress;
        } while (scene.progress < 0.9f);

        await Task.Delay(1000);

        scene.allowSceneActivation = true;

        await Task.Delay(1);
        loadingScreen.SetActive(false);
    }
}
