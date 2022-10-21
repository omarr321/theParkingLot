using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* This class updates the Loading Screen text
 * @Author Omar Radwan
 * @Version 1.0.0
 */
public class LoadingScreen : MonoBehaviour
{
    public TMPro.TextMeshProUGUI loadingText;
    public TMPro.TextMeshProUGUI stepCounterText;
    public Slider loadingBar;
    private int count = 1;
    private string dots = "";

    public int numOfSteps = 25;
    private int currSteps = 0;

    private string loadingString;
    //Set Deafult Values
    void Start()
    {
        stepCounterText.text = "0%";
        loadingBar.value = 0;
        loadingString = "";
        count = 1;
        currSteps = 0;
        loadingText.text = "LOADING";
        //Repeats the loadingLoop method every .5 seconds
        InvokeRepeating("loadingLoop", 0.5f, 0.5f);
    }
    
    // Updates the loading text and dots
    void loadingLoop(){
        dots = new string('.', count);
        loadingString = "LOADING" + dots;
        loadingText.text = loadingString;
        count++;
        count = count % 4;
        float currPercent = Mathf.Round((currSteps*1.0f/numOfSteps*1.0f) * 100.0f);
        loadingBar.value = currPercent;
        stepCounterText.text = currPercent.ToString() + "%";
    }

    // Incurments the step counter
    public void incurStep() {
        currSteps++;
        if (currSteps > numOfSteps) {
            currSteps = numOfSteps;
        }
    }
}
