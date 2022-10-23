using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

/* This class has all the functionally of the menu
 * @Author Omar Radwan
 * @Version 1.0.0
 */
public class MenuViewController : MonoBehaviour
{
    public GameObject creditView;
    public GameObject settingView;

    public SettingManager settingMan;
    public LoadManager loadMan;

    private FileStream playerPref;

    #region KeycodeDict
    Dictionary<char, KeyCode> chartoKeycode = new Dictionary<char, KeyCode>()
{
  //-------------------------LOGICAL mappings-------------------------
  
  //Lower Case Letters
  {'a', KeyCode.A},
  {'b', KeyCode.B},
  {'c', KeyCode.C},
  {'d', KeyCode.D},
  {'e', KeyCode.E},
  {'f', KeyCode.F},
  {'g', KeyCode.G},
  {'h', KeyCode.H},
  {'i', KeyCode.I},
  {'j', KeyCode.J},
  {'k', KeyCode.K},
  {'l', KeyCode.L},
  {'m', KeyCode.M},
  {'n', KeyCode.N},
  {'o', KeyCode.O},
  {'p', KeyCode.P},
  {'q', KeyCode.Q},
  {'r', KeyCode.R},
  {'s', KeyCode.S},
  {'t', KeyCode.T},
  {'u', KeyCode.U},
  {'v', KeyCode.V},
  {'w', KeyCode.W},
  {'x', KeyCode.X},
  {'y', KeyCode.Y},
  {'z', KeyCode.Z},
  
  //KeyPad Numbers
  {'1', KeyCode.Keypad1},
  {'2', KeyCode.Keypad2},
  {'3', KeyCode.Keypad3},
  {'4', KeyCode.Keypad4},
  {'5', KeyCode.Keypad5},
  {'6', KeyCode.Keypad6},
  {'7', KeyCode.Keypad7},
  {'8', KeyCode.Keypad8},
  {'9', KeyCode.Keypad9},
  {'0', KeyCode.Keypad0},
  
  //Other Symbols
  {'!', KeyCode.Exclaim}, //1
  {'"', KeyCode.DoubleQuote},
  {'#', KeyCode.Hash}, //3
  {'$', KeyCode.Dollar}, //4
  {'&', KeyCode.Ampersand}, //7
  {'\'', KeyCode.Quote}, //remember the special forward slash rule... this isnt wrong
  {'(', KeyCode.LeftParen}, //9
  {')', KeyCode.RightParen}, //0
  {'*', KeyCode.Asterisk}, //8
  {'+', KeyCode.Plus},
  {',', KeyCode.Comma},
  {'-', KeyCode.Minus},
  {'.', KeyCode.Period},
  {'/', KeyCode.Slash},
  {':', KeyCode.Colon},
  {';', KeyCode.Semicolon},
  {'<', KeyCode.Less},
  {'=', KeyCode.Equals},
  {'>', KeyCode.Greater},
  {'?', KeyCode.Question},
  {'@', KeyCode.At}, //2
  {'[', KeyCode.LeftBracket},
  {'\\', KeyCode.Backslash}, //remember the special forward slash rule... this isnt wrong
  {']', KeyCode.RightBracket},
  {'^', KeyCode.Caret}, //6
  {'_', KeyCode.Underscore},
  {'`', KeyCode.BackQuote},
  
  //-------------------------NON-LOGICAL mappings-------------------------
  
  //NOTE: all of these can easily be remapped to something that perhaps you find more useful
  
  //---Mappings where the logical keycode was taken up by its counter part in either (the regular keybaord) or the (keypad)
  
  //Alpha Numbers
  //NOTE: we are using the UPPER CASE LETTERS Q -> P because they are nearest to the Alpha Numbers
  {'Q', KeyCode.Alpha1},
  {'W', KeyCode.Alpha2},
  {'E', KeyCode.Alpha3},
  {'R', KeyCode.Alpha4},
  {'T', KeyCode.Alpha5},
  {'Y', KeyCode.Alpha6},
  {'U', KeyCode.Alpha7},
  {'I', KeyCode.Alpha8},
  {'O', KeyCode.Alpha9},
  {'P', KeyCode.Alpha0},
  
  //INACTIVE since I am using these characters else where
  {'A', KeyCode.KeypadPeriod},
  {'B', KeyCode.KeypadDivide},
  {'C', KeyCode.KeypadMultiply},
  {'D', KeyCode.KeypadMinus},
  {'F', KeyCode.KeypadPlus},
  {'G', KeyCode.KeypadEquals},
  
  //-------------------------CHARACTER KEYS with NO KEYCODE-------------------------
  
  //NOTE: you can map these to any of the OPEN KEYCODES below
  
  /*
  //Upper Case Letters (16)
  {'H', -},
  {'J', -},
  {'K', -},
  {'L', -},
  {'M', -},
  {'N', -},
  {'S', -},
  {'V', -},
  {'X', -},
  {'Z', -}
  */
  
  //-------------------------KEYCODES with NO CHARACER KEY-------------------------
  
  //-----KeyCodes without Logical Mappings
  //-Anything above "KeyCode.Space" in Unity's Documentation (9 KeyCodes)
  //-Anything between "KeyCode.UpArrow" and "KeyCode.F15" in Unity's Documentation (24 KeyCodes)
  //-Anything Below "KeyCode.Numlock" in Unity's Documentation [(28 KeyCodes) + (9 * 20 = 180 JoyStickCodes) = 208 KeyCodes]
  
  //-------------------------other-------------------------

  //-----KeyCodes that are inaccesible for some reason
  //{'~', KeyCode.tilde},
  //{'{', KeyCode.LeftCurlyBrace}, 
  //{'}', KeyCode.RightCurlyBrace}, 
  //{'|', KeyCode.Line},   
  //{'%', KeyCode.percent},
};
    #endregion

    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        try {
            FileStream playerPref = File.Open(Path.Combine(Application.persistentDataPath, "playerPref.dat"), FileMode.Open);
            playerPref.Close();
        } catch (FileNotFoundException) {
            FileStream playerPref = File.Create(Path.Combine(Application.persistentDataPath, "playerPref.dat"));
            playerPref.Close();
            Dictionary<string, object> saveData = new Dictionary<string, object>();
            saveData.Add("forward", settingMan.getDefaultForward().ToString().ToLower());
            saveData.Add("backward", settingMan.getDefaultBackward().ToString().ToLower());
            saveData.Add("left", settingMan.getDefaultLeft().ToString().ToLower());
            saveData.Add("right", settingMan.getDefaultRight().ToString().ToLower());
            saveData.Add("invOpen", settingMan.getDefaultInvOpen().ToString().ToLower());
            saveData.Add("craftingOpen", settingMan.getDefaultCrafting().ToString().ToLower());
            saveData.Add("interactKey", settingMan.getDefaultInteract().ToString().ToLower());
            saveData.Add("invertY", settingMan.getDefaultInvertY().ToString().ToLower());
            saveData.Add("camSense", settingMan.getDefaultCamSense().ToString().ToLower());

            StreamWriter writer = new StreamWriter(Path.Combine(Application.persistentDataPath, "playerPref.dat"));
            foreach(KeyValuePair<string, object> data in saveData){
                writer.WriteLine(data.Key + ":" + data.Value.ToString());
            }
            writer.Close();
        }

        StreamReader reader = new StreamReader(Path.Combine(Application.persistentDataPath, "playerPref.dat"));
        while(!reader.EndOfStream) {
            string[] data = reader.ReadLine().Split(":");
            KeyCode temp = KeyCode.None;
            switch(data[0]) {
                case "forward":
                    chartoKeycode.TryGetValue(data[1].ToCharArray()[0], out temp);
                    settingMan.setForward(temp);
                    break;
                case "backward":
                    chartoKeycode.TryGetValue(data[1].ToCharArray()[0], out temp);
                    settingMan.setBackward(temp);
                    break;
                case "left":
                    chartoKeycode.TryGetValue(data[1].ToCharArray()[0], out temp);
                    settingMan.setLeft(temp);
                    break;
                case "invOpen":
                    chartoKeycode.TryGetValue(data[1].ToCharArray()[0], out temp);
                    settingMan.setInvOpen(temp);
                    break;
                case "craftingOpen":
                    chartoKeycode.TryGetValue(data[1].ToCharArray()[0], out temp);
                    settingMan.setCrafting(temp);
                    break;
                case "interactKey":
                    chartoKeycode.TryGetValue(data[1].ToCharArray()[0], out temp);
                    settingMan.setInteract(temp);
                    break;
                case "invertY":
                    if (data[1] == "false"){
                        settingMan.setInvertY(false);
                    } else if (data[1] == "true") {
                        settingMan.setInvertY(true);
                    }
                    break;
                case "camSense":
                    settingMan.setCamSense(int.Parse(data[1]));
                    break;
            }
        }
        reader.Close();

        creditView.SetActive(false);
        settingView.SetActive(false);
    }

    public void toggleCredit()
    {
        creditView.SetActive(!creditView.activeInHierarchy);
    }

    public void toggleSetting()
    {
        settingView.SetActive(!settingView.activeInHierarchy);
    }

    public void clickedPlay()
    {
        //SceneManager.LoadScene(1);
    }

    public void clickedLoad()
    {

    }
}