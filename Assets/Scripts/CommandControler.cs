using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class controlls the command line and process the command
 * @Author Omar Radwam
 * @Version 1.0.0
 */
public class CommandControler : MonoBehaviour
{
    public TMPro.TextMeshProUGUI inputBox;
    public KeyCode cmdKey;
    public GameObject cmdView;
    public PlayerControlLock playerLock;
    public InvManager invMan;
    public PlayerManager playerMan;
    
    // Disable the cmdView
    void Start()
    {
        cmdView.SetActive(false);
    }

    // Check for the keybind input
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(cmdKey)) {
            if(playerLock.noOwner()) {
                cmdView.SetActive(true);
                playerLock.lockPlayer(this);
                playerLock.disableCam(this);
                playerLock.disableMovement(this);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }
    }

    // This closes the cmdView
    public void closeView() {
        inputBox.SetText("");
        cmdView.SetActive(false);
        playerLock.enableCam(this);
        playerLock.enableMovement(this);
        playerLock.unlockPlayer(this);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Process the command from the text box
    // @Parms TMPro.TMP_InputField input : The input to analyze
    public void processCmd(TMPro.TMP_InputField input) {
        string text = input.text;
        string[] textP = text.Split(".");
        // Checks first piece
        switch (textP[0])
        {
            case "player":
                Debug.Log("Player");
                // Checks second piece
                switch (textP[1]) {
                    case "health":
                        Debug.Log("Health");
                        // Checks third piece
                        switch (textP[2]) {
                            case "add":
                                Debug.Log("Add");
                                playerMan.addHealth(float.Parse(textP[3]));
                                break;
                            case "sub":
                                Debug.Log("Sub");
                                playerMan.subHealth(float.Parse(textP[3]));
                                break;
                            case "set":
                                Debug.Log("Set");
                                playerMan.setHealth(float.Parse(textP[3]));
                                break;
                            case "enabled":
                                Debug.Log("Enabled");
                                playerMan.setEnabledHealth(bool.Parse(textP[3]));
                                break;
                            default:
                                Debug.Log("Error");
                                break;
                        }
                        break;
                    case "saturation":
                        Debug.Log("Saturation");
                        // Checks third piece
                        switch (textP[2]) {
                            case "add":
                                Debug.Log("Add");
                                playerMan.addSaturation(float.Parse(textP[3]));
                                break;
                            case "sub":
                                Debug.Log("Sub");
                                playerMan.subSaturation(float.Parse(textP[3]));
                                break;
                            case "set":
                                Debug.Log("Set");
                                playerMan.setSaturation(float.Parse(textP[3]));
                                break;
                            case "enabled":
                                Debug.Log("Enabled");
                                playerMan.setEnabledSat(bool.Parse(textP[3]));
                                break;
                            default:
                                Debug.Log("Error");
                                break;
                        }
                        break;
                    case "hydration":
                        Debug.Log("Hydration");
                        // Checks third piece
                        switch (textP[2]) {
                            case "add":
                                Debug.Log("Add");
                                playerMan.addHydration(float.Parse(textP[3]));
                                break;
                            case "sub":
                                Debug.Log("Sub");
                                playerMan.subHydration(float.Parse(textP[3]));
                                break;
                            case "set":
                                Debug.Log("Set");
                                playerMan.setHydration(float.Parse(textP[3]));
                                break;
                            case "enabled":
                                Debug.Log("Enabled");
                                playerMan.setEnabledHydro(bool.Parse(textP[3]));
                                break;
                            default:
                                Debug.Log("Error");
                                break;
                        }
                        break;
                    default:
                        Debug.Log("Error");
                        break;
                }
                break;
            case "inv":
                // Checks second piece
                switch(textP[1]){
                    case "add":
                        invMan.addItem(Database.getItem(textP[2]));
                        break;
                    case "remove":
                        invMan.removeItem(int.Parse(textP[2]));
                        break;
                    case "removeLast":
                        invMan.removeLastItem();
                        break;
                    default:
                        Debug.Log("Error");
                        break;
                }
                break;
            default:
                Debug.Log("Error");
                break;
        }
    }
}