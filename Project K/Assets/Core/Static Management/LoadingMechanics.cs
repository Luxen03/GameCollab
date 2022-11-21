using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingMechanics : MonoBehaviour
{

    void Start()
    {
        Load();
    }

    public void Load()
    {
        Debug.Log("Loading scene...");
        StartCoroutine(LoadScene(1));
    }

    IEnumerator LoadScene(int _SceneIndex)
    {
        AsyncOperation Progress = SceneManager.LoadSceneAsync(_SceneIndex);

        while (!Progress.isDone){
            Debug.Log(Progress.progress);
            yield return null;
        }
    }

    internal void LoadPlayers()
    {

    }
}
