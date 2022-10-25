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

    public SettingManager settingMan;
    public LoadManager loadMan;

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

        creditView.SetActive(false);
        settingView.SetActive(false);
    }

    // Opens the credit tab
    public void toggleCredit()
    {
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
        //TODO: Pick a world name and get it loaded into the file system
    }

    public void clickedLoad()
    {
        //TODO: Pick a world to load and get it loaded into the file system
    }
}