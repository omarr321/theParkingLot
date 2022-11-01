using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseOpen : MonoBehaviour
{
    public pauseMenuController pauseMan;
    public PlayerManager playerMan;
    public PlayerControlLock playerLock;

    private bool flag;

    void Start() {
        pauseMan.closeAllWindows();
        flag = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if(playerLock.noOwner() || playerLock.checkOwner(this)) {
                if (flag) {
                    flag = false;
                    playerLock.enableCam(this);
                    playerLock.enableMovement(this);
                    pauseMan.closeAllWindows();
                    playerMan.setEnabledHealth(true);
                    playerMan.setEnabledHydro(true);
                    playerMan.setEnabledSat(true);
                    playerLock.unlockPlayer(this);
                } else {
                    flag = true;
                    playerLock.lockPlayer(this);
                    playerLock.disableCam(this);
                    playerLock.disableMovement(this);
                    pauseMan.backToPauseMenu();
                    playerMan.setEnabledHealth(false);
                    playerMan.setEnabledHydro(false);
                    playerMan.setEnabledSat(false);
                }
            }
        }
    }
}
