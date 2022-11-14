using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPage : MonoBehaviour
{
    // User Set Values
    public TMPro.TMP_Dropdown forward;
    public TMPro.TMP_Dropdown left;
    public TMPro.TMP_Dropdown backwards;
    public TMPro.TMP_Dropdown right;
    public TMPro.TMP_Dropdown invOpen;
    public TMPro.TMP_Dropdown craftingOpen;
    public TMPro.TMP_Dropdown interactKey;
    public Toggle invertY;
    public Slider camSense;
    public TMPro.TMP_Dropdown autoSaveTime;
    public SettingManager settingMan;
    public MenuViewController menuView;

    void OnEnable ()
    {
        if (settingMan == null) {
            try {
                settingMan = GameObject.Find("SettingPersonal").GetComponent<SettingManager>();
            } catch(NullReferenceException) {
                throw new Exception("Error: The game must be started from Main Menu scene");
            }
        }
        loadValFromFile();
    }

    public void restoreDefault()
    {
        settingMan.restoreDefaultVal();
    }

    public void loadValFromFile()
    {
        int value = settingMan.getForward().ToString().ToCharArray()[0] - 'A';
        forward.value = value;
        
        value = settingMan.getBackward().ToString().ToCharArray()[0] - 'A';
        backwards.value = value;

        value = settingMan.getLeft().ToString().ToCharArray()[0] - 'A';
        left.value = value;

        value = settingMan.getRight().ToString().ToCharArray()[0] - 'A';
        right.value = value;

        value = settingMan.getInvOpen().ToString().ToCharArray()[0] - 'A';
        invOpen.value = value;

        value = settingMan.getCrafting().ToString().ToCharArray()[0] - 'A';
        craftingOpen.value = value;

        value = settingMan.getInteract().ToString().ToCharArray()[0] - 'A';
        interactKey.value = value;

        bool invertVal = settingMan.getInvertY();
        invertY.isOn = invertVal;

        value = settingMan.getCamSense();
        camSense.value = value;
        
        value = this.getAutoSaveIndex(settingMan.getAutoSave());
        this.autoSaveTime.value = value;
    }

    private int getAutoSaveIndex(int autoSavetime) {
        switch(autoSavetime) 
        {
            case 0:
                return 0;
            case 60:
                return 1;
            case 180:
                return 2;
            case 300:
                return 3;
            case 600:
                return 4;
            case 900:
                return 5;
            case 1800:
                return 6;
            case 2700:
                return 7;
            case 3600:
                return 8;
        }
        return -1;
    }

    private int getAutoSaveFromIndex(int index) {
        switch(index) 
        {
            case 0:
                return 0;
            case 1:
                return 60;
            case 2:
                return 180;
            case 3:
                return 300;
            case 4:
                return 600;
            case 5:
                return 900;
            case 6:
                return 1800;
            case 7:
                return 2700;
            case 8:
                return 3600;
        }
        return -1;
    }

    public void saveValToSettingMan()
    {
        settingMan.setForward(settingMan.getKeycode(((char)(forward.value + 'A')).ToString().ToLower().ToCharArray()[0]));
        settingMan.setBackward(settingMan.getKeycode(((char)(backwards.value + 'A')).ToString().ToLower().ToCharArray()[0]));
        settingMan.setLeft(settingMan.getKeycode(((char)(left.value + 'A')).ToString().ToLower().ToCharArray()[0]));
        settingMan.setRight(settingMan.getKeycode(((char)(right.value + 'A')).ToString().ToLower().ToCharArray()[0]));
        settingMan.setInvOpen(settingMan.getKeycode(((char)(invOpen.value + 'A')).ToString().ToLower().ToCharArray()[0]));
        settingMan.setCrafting(settingMan.getKeycode(((char)(craftingOpen.value + 'A')).ToString().ToLower().ToCharArray()[0]));
        settingMan.setInteract(settingMan.getKeycode(((char)(interactKey.value + 'A')).ToString().ToLower().ToCharArray()[0]));
        settingMan.setInvertY(invertY.isOn);
        settingMan.setCamSense((int)camSense.value);
        settingMan.setAutoSave(this.getAutoSaveFromIndex(this.autoSaveTime.value));
        settingMan.savePlayerSetting();
    }

    public void closeView()
    {
        if (menuView != null) {
            menuView.gameObject.SetActive(true);
        }
        this.gameObject.SetActive(false);
    }
}
