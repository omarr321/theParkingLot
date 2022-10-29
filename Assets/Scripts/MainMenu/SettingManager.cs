using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/* This class has a instance of the playerSettings and is keep thought sceneLoading
 * @Author Omar Radwan
 * @Version 1.0.0
 */
public class SettingManager : MonoBehaviour
{
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

    // Default Values for everything
    private const KeyCode defaultForward = KeyCode.W;
    private const KeyCode defaultLeft = KeyCode.A;
    private const KeyCode defaultBackwards = KeyCode.S;
    private const KeyCode defaultRight = KeyCode.D;
    private const KeyCode defaultInvOpen = KeyCode.F;
    private const KeyCode defaultCraftingOpen = KeyCode.C;
    private const KeyCode defaultInteractKey = KeyCode.E;
    private const bool defaultInvertY = false;
    private const int defaultCamSense = 250;

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

    #region publicMethods
    public void defaultSetUp() {
            FileStream playerPref = File.Create(Path.Combine(Application.persistentDataPath, "playerPref.dat"));
            playerPref.Close();
            restoreDefaultVal();
    }

    public void restoreDefaultVal(){
            Dictionary<string, object> saveData = new Dictionary<string, object>();
            saveData.Add("forward", this.getDefaultForward().ToString().ToLower());
            saveData.Add("backward", this.getDefaultBackward().ToString().ToLower());
            saveData.Add("left", this.getDefaultLeft().ToString().ToLower());
            saveData.Add("right", this.getDefaultRight().ToString().ToLower());
            saveData.Add("invOpen", this.getDefaultInvOpen().ToString().ToLower());
            saveData.Add("craftingOpen", this.getDefaultCrafting().ToString().ToLower());
            saveData.Add("interactKey", this.getDefaultInteract().ToString().ToLower());
            saveData.Add("invertY", this.getDefaultInvertY().ToString().ToLower());
            saveData.Add("camSense", this.getDefaultCamSense().ToString().ToLower());

            StreamWriter writer = new StreamWriter(Path.Combine(Application.persistentDataPath, "playerPref.dat"));
            foreach(KeyValuePair<string, object> data in saveData){
                writer.WriteLine(data.Key + ":" + data.Value.ToString());
            }
            writer.Close();
            loadPlayerSetting();
    }

    public void loadPlayerSetting() {
        StreamReader reader = new StreamReader(Path.Combine(Application.persistentDataPath, "playerPref.dat"));
        while(!reader.EndOfStream) {
            string[] data = reader.ReadLine().Split(":");
            KeyCode temp = KeyCode.None;
            switch(data[0]) {
                case "forward":
                    chartoKeycode.TryGetValue(data[1].ToCharArray()[0], out temp);
                    this.setForward(temp);
                    break;
                case "backward":
                    chartoKeycode.TryGetValue(data[1].ToCharArray()[0], out temp);
                    this.setBackward(temp);
                    break;
                case "left":
                    chartoKeycode.TryGetValue(data[1].ToCharArray()[0], out temp);
                    this.setLeft(temp);
                    break;
                case "right":
                    chartoKeycode.TryGetValue(data[1].ToCharArray()[0], out temp);
                    this.setRight(temp);
                    break;
                case "invOpen":
                    chartoKeycode.TryGetValue(data[1].ToCharArray()[0], out temp);
                    this.setInvOpen(temp);
                    break;
                case "craftingOpen":
                    chartoKeycode.TryGetValue(data[1].ToCharArray()[0], out temp);
                    this.setCrafting(temp);
                    break;
                case "interactKey":
                    chartoKeycode.TryGetValue(data[1].ToCharArray()[0], out temp);
                    this.setInteract(temp);
                    break;
                case "invertY":
                    if (data[1] == "false"){
                        this.setInvertY(false);
                    } else if (data[1] == "true") {
                        this.setInvertY(true);
                    }
                    break;
                case "camSense":
                    this.setCamSense(int.Parse(data[1]));
                    break;
            }
        }
        reader.Close();
    }

    public void savePlayerSetting() {
        Dictionary<string, object> saveData = new Dictionary<string, object>();
        saveData.Add("forward", this.getForward().ToString().ToLower());
        saveData.Add("backward", this.getBackward().ToString().ToLower());
        saveData.Add("left", this.getLeft().ToString().ToLower());
        saveData.Add("right", this.getRight().ToString().ToLower());
        saveData.Add("invOpen", this.getInvOpen().ToString().ToLower());
        saveData.Add("craftingOpen", this.getCrafting().ToString().ToLower());
        saveData.Add("interactKey", this.getInteract().ToString().ToLower());
        saveData.Add("invertY", this.getInvertY().ToString().ToLower());
        saveData.Add("camSense", this.getCamSense().ToString().ToLower());

        StreamWriter writer = new StreamWriter(Path.Combine(Application.persistentDataPath, "playerPref.dat"));
        foreach(KeyValuePair<string, object> data in saveData){
            writer.WriteLine(data.Key + ":" + data.Value.ToString());
        }
        writer.Close();
    }
    #endregion

    public KeyCode getKeycode(char key)
    {
        KeyCode temp = KeyCode.None;
        chartoKeycode.TryGetValue(key, out temp);
        return temp;
    }
}
