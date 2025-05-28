using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] ScreenFade myFader;

    [SerializeField] string sceneName;
    [SerializeField] bool isWinObject;
    [SerializeField] bool loadingScene;
    public string currentLevel = "Level 1";

    [SerializeField] GameObject confirmMenu;

    //new game stuff
    TextMeshPro text;
    [SerializeField] bool pressedOnce;


    void Awake()
    {

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            Instantiate(confirmMenu, GameObject.Find("MenuSpawnPos").transform);
            ConfirmMenu menuType = confirmMenu.GetComponent<ConfirmMenu>();

            menuType.whatMenu = "ReturnToMenu";
        }
        else
        {
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && isWinObject && !loadingScene)
        {
            BeatLevel("Level " + (SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    public void YouFuckingDiedYouLoser()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ButtonPressed(string loadSceneName)
    {
        if (!loadingScene)
        {
            StartCoroutine(StartLoadScene(loadSceneName));
        }
    }

    public void ReturnToMainMenu()
    {
        StartCoroutine(StartLoadScene("Main Menu"));
    }

    public void NewGame()
    {
        if (!pressedOnce)
        {
            Instantiate(confirmMenu, GameObject.Find("MenuSpawnPos").transform);
            ConfirmMenu menuType = confirmMenu.GetComponent<ConfirmMenu>();

            menuType.whatMenu = "NewGame";
            pressedOnce = true;
        }
        else
        {
            SaveScript saveData = GameObject.Find("MenuManager").GetComponent<SaveScript>();
            saveData.ResetData();
            StartGame();
        }
    }

    public void BeatLevel(string nextLevel)
    {
        StartCoroutine(StartLoadScene(nextLevel));
        currentLevel = nextLevel;
        SaveScript saveData = GetComponent<SaveScript>();
        saveData.SaveGame(nextLevel);
    }

    public void StartGame()
    {
        if (!loadingScene)
        {
            StartCoroutine(StartLoadScene(File.ReadAllText(Application.dataPath + "save.txt")));
        }
    }

    public void QuitGame()
    {
        Instantiate(confirmMenu, GameObject.Find("MenuSpawnPos").transform);
        ConfirmMenu menuType = confirmMenu.GetComponent<ConfirmMenu>();

        menuType.whatMenu = "Quit";
    }

    IEnumerator StartLoadScene(string sceneToLoad)
    {
        loadingScene = true;
        //add reference to player script to play a little animation/move the player into the object before teleporting
        myFader.StartCoroutine(myFader.FadeOutCoroutine(2));
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneToLoad);
        loadingScene = false;
    }
}