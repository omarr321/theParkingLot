using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadPage : MonoBehaviour
{
    public TMPro.TMP_Dropdown worldList;
    public TMPro.TextMeshProUGUI titleName;
    public TMPro.TextMeshProUGUI lastPlayed;
    public WorldManager worldMan;
    public MenuViewController menuView;
    public Button loadButt;

    private Color grey = new Color(0.6f, 0.6f, 0.6f, 1.0f);
    private Color white = new Color(1f, 1f, 1f, 1.0f);
    private string folderPath;
    private string[] worldFolders;
    
    void Start()
    {
        loadButt.enabled = false;
        loadButt.image.color = this.grey;
        titleName.text = "";
        lastPlayed.text = "";
        worldList.onValueChanged.AddListener(onWorldChanged);
        worldList.ClearOptions();
        worldList.AddOptions(new List<string>(){"----"});
        this.folderPath = Path.Combine(Application.persistentDataPath, "saves");
        worldFolders = Directory.GetDirectories(this.folderPath);
        
        for(int i = 0; i < worldFolders.Length; i++) {
            string[] temp = worldFolders[i].Split("\\");
            worldFolders[i] = temp[temp.Length-1];
        }
        
        foreach (string world in worldFolders) {
            worldList.AddOptions(new List<string>(){world});
        }
    }

    public void closeView()
    {
        menuView.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void loadWorld()
    {
        SceneManager.LoadScene(1);
    }

    private void onWorldChanged(int arg0)
    {
        if (worldList.value == 0) {
            this.titleName.text = "";
            this.lastPlayed.text = "";
            loadButt.enabled = false;
            loadButt.image.color = this.grey;
        } else {
            loadButt.image.color = this.white;
            loadButt.enabled = true;
            worldMan.setWorldName(worldFolders[worldList.value-1]);
            worldMan.initWorld();
            worldMan.loadPlayerValFromFile();
            this.titleName.text = worldMan.getWorldName();
            this.lastPlayed.text = worldMan.getLastPlayed();
        }
    }
}