using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public enum MenuId { StartScreen = 0, InfoPanel = 1, EndPanel = 2, VideoPanel = 3};
    public List<PanelId> Panels;

    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<PanelId>() != null)
            {
                Panels.Add(child.GetComponent<PanelId>());
            }
        }
        foreach (PanelId Panel in Panels)
        {
            if (Panel.ID == MenuId.StartScreen)
            {
                Panel.gameObject.SetActive(true);
            }
            else
            {
                Panel.gameObject.SetActive(false);
            }
        }
    }
    public void setActivePanel(int ID)
    {
        foreach (PanelId Panel in Panels)
        {
            if (Panel.ID == (MenuId)ID)
            {
                Panel.gameObject.SetActive(true);
            }
            else
            {
                Panel.gameObject.SetActive(false);
            }
        }
    }
    public void hideAllPanels()
    {
        foreach(PanelId Panel in Panels)
        {
            Panel.gameObject.SetActive(false);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
