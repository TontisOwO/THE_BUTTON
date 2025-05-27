using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] ScreenFade myFader;

    [SerializeField] string sceneName;
    [SerializeField] public int currentLevel = 1;

    public static SceneLoader Instance;

    void Awake()
    {
        if (Instance = null)
        {
            Instance = this;
        }
    }


    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(StartLoadScene(sceneName));
        }
    }

    public void ButtonPressed(string loadSceneName)
    {
        StartCoroutine(StartLoadScene(loadSceneName));
    }

    public void StartGame()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator StartLoadScene(string sceneToLoad)
    {
        //add reference to player script to play a little animation/move the player into the object before teleporting
        myFader.StartCoroutine(myFader.FadeOutCoroutine(2));
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnApplicationQuit( )
    {
        SaveData.SaveGameState();
    }
}