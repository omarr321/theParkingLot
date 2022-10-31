using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

/* This class has all the functionally of the menu
 * @Author Omar Radwan
 * @Version 1.0.0
 */
public class MenuViewController : MonoBehaviour
{
    public GameObject creditView;
    public GameObject settingView;
    public GameObject createWorld;
    public GameObject loadWorldView;

    public SettingManager settingMan;
    public WorldManager loadMan;

    private FileStream playerPref;

    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        try {
            FileStream playerPref = File.Open(Path.Combine(Application.persistentDataPath, "playerPref.dat"), FileMode.Open);
            playerPref.Close();
        } catch (FileNotFoundException) {
            settingMan.defaultSetUp();
        }
        settingMan.loadPlayerSetting();

        if(!System.IO.Directory.Exists(Path.Combine(Application.persistentDataPath, "saves"))) {
            System.IO.Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "saves"));
        }

        creditView.SetActive(false);
        settingView.SetActive(false);
        createWorld.SetActive(false);
        loadWorldView.SetActive(false);
    }

    // Opens the credit tab
    public void toggleCredit()
    {
        this.gameObject.SetActive(false);
        creditView.SetActive(!creditView.activeInHierarchy);
    }

    // Opens the Setting tab
    // NOTE: It hides the main menu
    public void toggleSetting()
    {
        settingView.SetActive(!settingView.activeInHierarchy);
        this.gameObject.SetActive(false);
    }

    public void clickedPlay()
    {
        createWorld.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void clickedLoad()
    {
        loadWorldView.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void clickedQuit()
    {
        Application.Quit();
    }
}