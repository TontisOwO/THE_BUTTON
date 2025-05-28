using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmMenu : MonoBehaviour
{
    [SerializeField] float fadeDuration;

    public string whatMenu;
    [SerializeField] GameObject confirmScreen;
    [SerializeField] ScreenFade screenFader;
    [SerializeField] AudioSource source;

    [SerializeField] List<AudioClip> audioList;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        confirmScreen = this.gameObject;
        screenFader = GameObject.Find("ScreenFade").GetComponent<ScreenFade>();
    }

    void Update()
    {

    }

    public void WhatButton(string buttonName)
    {
        switch (whatMenu)
        {
            case "Quit":
                {
                    switch (buttonName)
                    {
                        case "Yes":
                            {
                                StartCoroutine(QuitGame());
                                break;
                            }

                        case "No":
                            {
                                Destroy(confirmScreen);
                                break;
                            }
                    }
                    break;
                }

            case "NewGame":
                {
                    switch (buttonName)
                    {
                        case "Yes":
                            {
                                SceneLoader loader = GameObject.Find("MenuManager").GetComponent<SceneLoader>();
                                loader.NewGame();
                                break;
                            }

                        case "No":
                            {
                                Destroy(confirmScreen);
                                break;
                            }
                    }
                    break;
                }

            case "ReturnToMenu":
                {
                    switch (buttonName)
                    {
                        case "Yes":
                            {
                                SceneLoader loader = GameObject.Find("GameManager").GetComponent<SceneLoader>();
                                loader.ReturnToMainMenu();
                                break;
                            }

                        case "No":
                            {
                                Destroy(confirmScreen);
                                break;
                            }
                    }
                    break;
                }


        }
    }

    IEnumerator QuitGame()
    {
        screenFader.StartCoroutine(screenFader.FadeOutCoroutine(fadeDuration));
        yield return new WaitForSeconds(fadeDuration);

        Application.Quit();
    }
}
