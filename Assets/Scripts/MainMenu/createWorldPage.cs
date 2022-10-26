using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class createWorldPage : MonoBehaviour
{
    public GameObject mainMenu;
    public WorldManager worldMan;
    public TMPro.TMP_InputField input;

    public void closeView() {
        mainMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void genWorld() {
        if (!checkWorldName()) {
            return;
        }

        worldMan.setWorldName(this.input.text);
        int seed = Random.Range(100000000,1000000000);
        worldMan.setSeed(seed);
        worldMan.initWorld();
        Debug.Log("Seed: " + seed.ToString());
        worldMan.loadPlayerValFromFile();
        
        SceneManager.LoadScene(1);
    }

    private bool checkWorldName() {
        Regex regex = new Regex("^[A-Za-z0-9-]*$");
        if (this.input.text == "" || !(regex.IsMatch(this.input.text))) {
            return false;
        }
        return true;
    }
}
