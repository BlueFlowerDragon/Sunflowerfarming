using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int Page=0;
    public enum MenuStates { Main, Page1, Page2, Page3 };
    public MenuStates currentstate;
    // Start is called before the first frame update
    public GameObject HowPlay;
    public GameObject MusicOnClick;
    void Start()
    {
        if(PlayerPrefs.GetInt("Level")== 0){
            PlayerPrefs.SetInt("Level", 2);
        }
        Debug.Log("MainMenu專案啟動了");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Page = 0;
            Instantiate(MusicOnClick);
        }
            switch (Page)
        {
            case 0:
                HowPlay.SetActive(false);
                break;
            case 1:
                HowPlay.SetActive(true);
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }
    public void GameStart()
    {
        Debug.Log("開始遊戲啟動");
        SceneManager.LoadScene(2);
    }

    public void HowtoPlay()
    {
        Debug.Log("遊戲說明啟動");
        Page = 1;
        Instantiate(MusicOnClick);
        //SceneManager.LoadScene(1);
    }
    public void BacktoMenu()
    {
        Debug.Log("關閉教學");
        Page = 0;
        //currentstate = MenuStates.Main;
    }
    public void PageR()
    {
        Debug.Log("往前翻頁");
        if (Page < 3) {
            Page++;
        }
        //currentstate = MenuStates.Page2;
    }
    public void PageL()
    {
        Debug.Log("往後翻頁");
        if (Page > 1)
        {
            Page--;
        }
        //currentstate = MenuStates.Page2;
    }
    void Awake() {
        currentstate = MenuStates.Main;
        Page = 0;
    }
    
    public void ExitGame() {
        Application.Quit();
    }
}
