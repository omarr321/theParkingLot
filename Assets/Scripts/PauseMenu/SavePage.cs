using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePage : MonoBehaviour
{
    public TMPro.TextMeshProUGUI savingText;
    public Button backButt;
    public PlayerManager playerMan;
    public InvManager invManager;
    private WorldManager worldMan;

    private Color grey = new Color(0.6f, 0.6f, 0.6f, 1.0f);
    private Color white = new Color(1f, 1f, 1f, 1.0f);

    // Start is called before the first frame update
    void Start()
    {
        worldMan = GameObject.Find("LoadSetting").GetComponent<WorldManager>();
        playerMan.savePlayer();
        invManager.saveInv();
        worldMan.savePlayerValToFile();
        backButt.image.color = white;
        backButt.enabled = true;
    }
}