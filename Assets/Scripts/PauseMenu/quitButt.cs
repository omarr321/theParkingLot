using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitButt : MonoBehaviour
{
    public PlayerManager playerMan;
    public InvManager invManager;

    private WorldManager worldMan;
    
    public void saveAndQuit()
    {
        worldMan = GameObject.Find("LoadSetting").GetComponent<WorldManager>();
        playerMan.savePlayer();
        invManager.saveInv();
        worldMan.savePlayerValToFile();
        Application.Quit();
    }
}
