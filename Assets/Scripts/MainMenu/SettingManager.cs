using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class has a instance of the playerSettings and is keep thought sceneLoading
 * @Author Omar Radwan
 * @Version 1.0.0
 */
public class SettingManager : MonoBehaviour
{
    // Default Values for everything
    private const KeyCode defaultForward = KeyCode.W;
    private const KeyCode defaultLeft = KeyCode.A;
    private const KeyCode defaultBackwards = KeyCode.S;
    private const KeyCode defaultRight = KeyCode.D;
    private const KeyCode defaultInvOpen = KeyCode.F;
    private const KeyCode defaultCraftingOpen = KeyCode.C;
    private const KeyCode defaultInteractKey = KeyCode.E;
    private const bool defaultInvertY = false;
    private const int defaultCamSense = 25;

    // User Set Values
    private KeyCode forward;
    private KeyCode left;
    private KeyCode backwards;
    private KeyCode right;
    private KeyCode invOpen;
    private KeyCode craftingOpen;
    private KeyCode interactKey;
    private bool invertY;
    private int camSense;


    public static SettingManager Instance;

    // Set this gameObject to not be destory on scene load
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #region DefaultGetters
    public KeyCode getDefaultForward()
    {
        return defaultForward;
    }

    public KeyCode getDefaultBackward()
    {
        return defaultBackwards;
    }

    public KeyCode getDefaultLeft()
    {
        return defaultLeft;
    }

    public KeyCode getDefaultRight()
    {
        return defaultRight;
    }

    public KeyCode getDefaultInvOpen()
    {
        return defaultInvOpen;
    }

    public KeyCode getDefaultCrafting()
    {
        return defaultCraftingOpen;
    }

    public KeyCode getDefaultInteract()
    {
        return defaultInteractKey;
    }

    public bool getDefaultInvertY()
    {
        return defaultInvertY;
    }

    public int getDefaultCamSense()
    {
        return defaultCamSense;
    }
    #endregion

    #region PlayerGetters
    public KeyCode getForward()
    {
        return forward;
    }

    public KeyCode getBackward()
    {
        return backwards;
    }

    public KeyCode getLeft()
    {
        return left;
    }

    public KeyCode getRight()
    {
        return right;
    }

    public KeyCode getInvOpen()
    {
        return invOpen;
    }

    public KeyCode getCrafting()
    {
        return craftingOpen;
    }

    public KeyCode getInteract()
    {
        return interactKey;
    }

    public bool getInvertY()
    {
        return invertY;
    }

    public int getCamSense()
    {
        return camSense;
    }
    #endregion

    #region  PlayerSetters
        public void setForward(KeyCode key)
    {
        forward = key;
    }

    public void setBackward(KeyCode key)
    {
        backwards = key;
    }

    public void setLeft(KeyCode key)
    {
        left = key;
    }

    public void setRight(KeyCode key)
    {
        right = key;
    }

    public void setInvOpen(KeyCode key)
    {
        invOpen = key;
    }

    public void setCrafting(KeyCode key)
    {
        craftingOpen = key;
    }

    public void setInteract(KeyCode key)
    {
        interactKey = key;
    }

    public void setInvertY(bool val)
    {
        invertY = val;
    }

    public void setCamSense(int val)
    {
        camSense = val;
    }
    #endregion
}
