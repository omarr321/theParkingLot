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

    public void closeView()
    {
        menuView.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
