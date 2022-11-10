using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public enum MenuId { StartScreen = 0, InfoPanel = 1, EndPanel = 2, VideoPanel = 3};
    public List<PanelId> Panels;
    SmoothLocomotion SmoothLoco;
    public UnityEngine.UI.Slider FeedBackSlider;
    public UnityEngine.UI.Button FeedBack;

    void Start()
    {
        FeedBack.onClick.AddListener(RecieveFeedBack);
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
        SmoothLoco = FindObjectOfType<SmoothLocomotion>();
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
    public void AdjustLocomotionSpeed(float value)
    {
        SmoothLoco.MovementSpeed = value;
    }
    void RecieveFeedBack()
    {
        System.IO.File.WriteAllText(System.IO.Path.Combine(Application.persistentDataPath, "FeedBack.txt"), FeedBackSlider.value.ToString());
    }
}
