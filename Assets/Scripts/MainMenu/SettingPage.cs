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
