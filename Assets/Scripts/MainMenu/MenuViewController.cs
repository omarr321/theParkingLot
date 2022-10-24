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

    public void toggleCredit()
    {
        creditView.SetActive(!creditView.activeInHierarchy);
    }

    public void toggleSetting()
    {
        settingView.SetActive(!settingView.activeInHierarchy);
    }

    public void clickedPlay()
    {
        //SceneManager.LoadScene(1);
    }

    public void clickedLoad()
    {

    }
}