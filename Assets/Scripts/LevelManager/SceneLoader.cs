using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class SceneLoader : MonoBehaviour
{
    public Slider loadingSlider;
    public int levelCount;
    public int lastLevel;
    [SerializeField] private GameObject updateReqPanel;
    private bool isUpdateRequired;

    private void Awake()
    {
     
    }
    public void OnUpdateRequired()
    {
        isUpdateRequired = true;
        updateReqPanel.SetActive(true);

    }
 
    void Start()
    {
        if (!isUpdateRequired)
        {
            lastLevel = PlayerPrefs.GetInt("lastLevel", 1);

            if (SceneController.instance.firstLogin)
            {
                StartCoroutine(AsyncSceneLoader((lastLevel % lastLevel) == 0 ? lastLevel : (lastLevel % lastLevel)));
                SceneController.instance.firstLogin = false;
            }
            else
                StartCoroutine(AsyncSceneLoader(SceneController.instance.levelIndex));
        }
    }

    IEnumerator AsyncSceneLoader(int BuildIndex)
    {
        AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(BuildIndex, LoadSceneMode.Single);

        while (!asyncLoadScene.isDone)
        {
            loadingSlider.value = asyncLoadScene.progress;
            yield return null;
        }
    }
}