using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSavingScript : MonoBehaviour
{
    public GameObject autoSaveTag;
    private WorldManager worldMan;
    public PlayerManager playerMan;
    public InvManager invMan;

    private int saveInterval;
    private bool saveStarted = false;

    void Start()
    {
        worldMan = GameObject.Find("LoadSetting").GetComponent<WorldManager>();
        autoSaveTag.SetActive(false);
        saveInterval = 0;
    }

    public void setSaveInterval(int interval) {
        this.saveInterval = interval;
    }

    public void startSaveLoop() {
        if (!saveStarted) {
            if (this.saveInterval != 0) {
                InvokeRepeating("saveLoop", this.saveInterval, this.saveInterval);
            }
            this.saveStarted = true;
        }
    }

    public void saveLoop() {
        StartCoroutine(saveGame());
    }

    private IEnumerator saveGame() {
        autoSaveTag.SetActive(true);
        yield return null;
        playerMan.savePlayer();
        yield return null;
        invMan.saveInv();
        yield return null;
        worldMan.savePlayerValToFile();
        //NOTE: This add artifical Delay in the return of the player saving because otherwise it is too fast and people will not trust it
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        autoSaveTag.SetActive(false);
    }
}
