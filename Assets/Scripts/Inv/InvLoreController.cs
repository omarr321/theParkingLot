using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class controls the Lore screen of the inv
 * @Author Omar Radwan
 * @Version 1.0.0
 */
public class InvLoreController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI loreTitle;
    public TMPro.TextMeshProUGUI loreBody;

    public UnityEngine.UI.Button prevPageButt;
    public UnityEngine.UI.Button nextPageButt;
    public TMPro.TextMeshProUGUI pageCount;

    private int currPage = 1;
    private int maxPage;

    // Sets the Text of the panel
    // @Parms string title : The Item name
    // @Parms string lore : The Item lore
    public IEnumerator setText(string title, string lore)
    {
        loreTitle.text = title;
        loreBody.text = lore;
        prevPageButt.gameObject.SetActive(false);
        nextPageButt.gameObject.SetActive(false);
        this.pageCount.text = "";
        yield return null;
        this.maxPage = loreBody.textInfo.pageCount;
        this.pageCount.text = "Page " + currPage + " of " + maxPage;
        updatePageButtons();
    }

    // Display page button based on what page you are on
    private void updatePageButtons()
    {
        prevPageButt.gameObject.SetActive(true);
        nextPageButt.gameObject.SetActive(true);
        if (currPage == 1) {
            prevPageButt.gameObject.SetActive(false);
        }
        if (currPage == maxPage) {
            nextPageButt.gameObject.SetActive(false);
        }
    }

    // Closes the Window
    public void closeWindow()
    {
        this.currPage = 1;
        loreBody.pageToDisplay = currPage;
        this.gameObject.SetActive(false);
    }

    // Goes to the next page
    public void nextPage()
    {
        currPage++;
        this.pageCount.text = "Page " + currPage + " of " + maxPage;
        loreBody.pageToDisplay = currPage;
        updatePageButtons();
    }

    // Goes to the prev page
    public void prevPage()
    {
        currPage--;
        this.pageCount.text = "Page " + currPage + " of " + maxPage;
        loreBody.pageToDisplay = currPage;
        updatePageButtons();
    }
}
