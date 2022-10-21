using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class controls and lock the player so only one class can control the player at a time
 * @Author Omar Radwan
 * @Version 1.1.0
 */
public class PlayerControlLock : MonoBehaviour
{
    public FirstPersonCamera cam;
    public Movement move;
    public Rigidbody playerBody;
    private MonoBehaviour current;
    private bool lockStatus;
    
    // Start set default values
    void Start()
    {
        lockStatus = false;
    }

    // Locks the player so the gameObject provoded is the only one to control the player
    // @Parms MonoBehaviour obj : The controlling obj
    // @Return bool : True if it was successful, false if otherwise
    public bool lockPlayer(MonoBehaviour obj) {
        if (lockStatus) {
            return false;
        } else {
            lockStatus = true;
            current = obj;
            return true;
        }
    }

    // Releases the lock on the player
    // @Parms MonoBehaviour obj : The controlling obj
    // @Return bool : True if it was successful, false if otherwise
    public bool unlockPlayer(MonoBehaviour obj) {
        if (obj != current) {
            return false;
        }

        lockStatus = false;
        current = null;
        return true;
    }

    // Disables the player camera
    // @Parms MonoBehaviour obj : The controlling obj
    // @Return bool : True if it was successful, false if otherwise
    public bool disableCam(MonoBehaviour obj) {
        if (obj != current) {
            return false;
        }

        cam.enabled = false;
        return true;
    }

    // Enables the player camera
    // @Parms MonoBehaviour obj : The controlling obj
    // @Return bool : True if it was successful, false if otherwise
    public bool enableCam(MonoBehaviour obj) {
        if (obj != current) {
            return false;
        }

        cam.enabled = true;
        return true;
    }

    // Disables the player movement
    // @Parms MonoBehaviour obj : The controlling obj
    // @Return bool : True if it was successful, false if otherwise
    public bool disableMovement(MonoBehaviour obj) {
        if (obj != current) {
            return false;
        }

        playerBody.constraints =  RigidbodyConstraints.FreezeAll;
        move.enabled = false;
        return true;
    }

    // Enables the player movement
    // @Parms MonoBehaviour obj : The controlling obj
    // @Return bool : True if it was successful, false if otherwise
    public bool enableMovement(MonoBehaviour obj) {
        if (obj != current) {
            return false;
        }

        playerBody.constraints = RigidbodyConstraints.None;
        move.enabled = true;
        return true;
    }

    // Gets the lock status of the player
    // @Return bool : The Lock status
    public bool getLockStatus() {
        return lockStatus;
    }

    // checks to see if you are the owner or there is no owner
    // @Parms MonoBehaviour obj : The controlling obj
    // @Return bool : True if you are the owner or there is no owner, false if otherwise
    public bool checkOwner(MonoBehaviour obj) {
        return obj == current || noOwner();
    }

    // checks to see if there is an owner
    // @Return bool : True if there is no owner, false if otherwise
    public bool noOwner() {
        return current == null;
    }
}
