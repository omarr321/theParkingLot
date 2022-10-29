using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseOpen : MonoBehaviour
{
   public GameObject pauseMenu;
   public PlayerControlLock playerLock;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if(playerLock.noOwner()) {
                if (pauseMenu.activeInHierarchy) {
                    playerLock.enableCam(this);
                    playerLock.enableMovement(this);
                    pauseMenu.SetActive(false);
                    playerLock.unlockPlayer(this);
                } else {
                    playerLock.lockPlayer(this);
                    playerLock.disableCam(this);
                    playerLock.disableMovement(this);
                    pauseMenu.SetActive(true);
                }
            }
        }
    }
}
