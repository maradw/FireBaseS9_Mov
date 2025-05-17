﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class SceneGlobalManager : MonoBehaviour
{
    public static SceneGlobalManager Instance;

    //[SerializeField] private Image loadingBarFill;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;    
        DontDestroyOnLoad(gameObject);
    }

    public void AssignLoadingBar(Image bar)
    {
       //loadingBarFill = bar;
    }
    public void LoadScene(string sceneName)
    {

        SceneManager.LoadScene(sceneName);
    }
    public void LoadSceneAsync(string sceneName)
    {

        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
    
    public void UnloadSceneAsync(string sceneToUnload)
    {
        SceneManager.UnloadSceneAsync(sceneToUnload);
    }
    private IEnumerator LoadWithLoadingScreen(string loadScene, string targetScene)
    {
   
        AsyncOperation loadLoading = SceneManager.LoadSceneAsync(loadScene, LoadSceneMode.Additive);
        yield return loadLoading;

        yield return null;

    
        AsyncOperation loadTarget = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Single);
        loadTarget.allowSceneActivation = false;

        float progress = 0f;
        while (progress < 0.9f)
        {
            progress = Mathf.Clamp01(loadTarget.progress / 0.9f);

            yield return null;
        }

        yield return new WaitForSeconds(0.5f); 


        loadTarget.allowSceneActivation = true;

       
        yield return new WaitForSeconds(1f); 
        SceneManager.UnloadSceneAsync(loadScene);
    }
    public void HideScene(string sceneToHide)
    {

        Scene gameScene = SceneManager.GetSceneByName(sceneToHide);
        if (gameScene.IsValid())
        {
            GameObject[] rootObjects = gameScene.GetRootGameObjects();
            for (int i = 0; i < rootObjects.Length; i++)
            {
                rootObjects[i].SetActive(false);
            }
        }
        
    }
    private IEnumerator AdditiveScenes(string previousScene, string nextScene)
    {
        AsyncOperation gameLoad = SceneManager.LoadSceneAsync(previousScene, LoadSceneMode.Additive);
        yield return gameLoad;

        Scene resultsScene = SceneManager.GetSceneByName(nextScene);
        if (resultsScene.IsValid())
        {
            GameObject[] rootObjects = resultsScene.GetRootGameObjects();
            for (int i = 0; i < rootObjects.Length; i++)
            {
                rootObjects[i].SetActive(false);
            }
        }

        Scene gameScene = SceneManager.GetSceneByName(previousScene);
        if (gameScene.IsValid())
        {
            GameObject[] rootObjects = gameScene.GetRootGameObjects();
            for (int i = 0; i < rootObjects.Length; i++)
            {
                rootObjects[i].SetActive(true);
            }
        }
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
                                        Application.Quit();
#endif
    }


}