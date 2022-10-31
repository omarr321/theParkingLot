using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditPage : MonoBehaviour
{
    public GameObject mainMenu;
    
    public void backToMenu()
    {
        this.mainMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
