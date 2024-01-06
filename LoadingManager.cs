using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : GlobalReference<LoadingManager>
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image progressBar;
    private float target;
    public async void LoadScene(string sceneName)
    {
        target = 0;
        progressBar.fillAmount = 0;

        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loadingScreen.SetActive(true);

        do
        {
            await Task.Delay(100);
            target = scene.progress;
            
        } while (scene.progress < 1f);

        await Task.Delay(1000);
        scene.allowSceneActivation = true;
        loadingScreen.SetActive(false);
    }
    private void Update()
    {
        progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, target, Time.deltaTime * 3);
    }
}
