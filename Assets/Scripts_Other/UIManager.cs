using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject StartButton;

    bool isSettingPanel = false;
    void Start()
    {
        InactivateAll();
    }

    public void ShowSettingPanel()
    {
        if (!isSettingPanel)
        {
            settingPanel.SetActive(true);
            isSettingPanel = true;

            if (StartButton != null)
            {
                StartButton.SetActive(false);
            }
        }
        else
        {
            settingPanel.SetActive(false);
            isSettingPanel = false;

            if (StartButton != null)
            {
                StartButton.SetActive(true);
            }
        }
    }

    public void InactivateAll()
    {
        settingPanel.SetActive(false);
        isSettingPanel = false;

    }
}
