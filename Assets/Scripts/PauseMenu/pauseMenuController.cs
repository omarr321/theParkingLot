using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenuController : MonoBehaviour
{
    public GameObject creditView;
    public GameObject settingView;
    public GameObject pauseMain;
    public GameObject saveView;

    public PlayerManager playerMan;
    public InvManager invManager;
    private WorldManager worldMan;

    // public SettingManager settingMan;
    // public WorldManager loadMan;

    public void openSave()
    {
        closeAllWindows();
        saveView.SetActive(true);
    }

    public void openCredit()
    {
        closeAllWindows();
        creditView.SetActive(true);
    }

    public void openSetting()
    {
        closeAllWindows();
        settingView.SetActive(true);
    }

    public void backToPauseMenu()
    {
        closeAllWindows();
        pauseMain.SetActive(true);
    }

    public void closeAllWindows()
    {
        creditView.SetActive(false);
        settingView.SetActive(false);
        pauseMain.SetActive(false);
        saveView.SetActive(false);
    }

    public void backToMainMenu()
    {
        worldMan = GameObject.Find("LoadSetting").GetComponent<WorldManager>();
        playerMan.savePlayer();
        invManager.saveInv();
        worldMan.savePlayerValToFile();
        SceneManager.LoadScene(0);
    }
}
