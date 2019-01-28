using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public string WinFormat = "Good job!\nScore: {0}";
    public string LoseFormat = "You wimp!\nScore: {0}";

    [SerializeField]
    private GameFlowController Flow;

    [SerializeField]
    private TextMeshProUGUI Text;

    [SerializeField]
    private GameObject Continue;

    [SerializeField]
    private GameObject Restart;

    [SerializeField]
    private GameObject Quit;

    [SerializeField]
    private EventSystem EventSystem = null;

    public void DoContinue()
    {
        if(UpgradeClass.Tip > 0)
            Flow.LoadShop();
        else
            Flow.StartNextLevel();
    }

    public void DoRestart()
    {
        Flow.RestartCurrentLevel();
    }

    public void DoQuit()
    {
        Flow.LoadMainMenu();
    }

    private void Awake()
    {
    }

    public void Show(bool isVictory, int score)
    {
        if (EventSystem == null)
            EventSystem = FindObjectOfType<EventSystem>();

        string format = isVictory ? WinFormat : LoseFormat;
        Text.text = string.Format(format, score);
        gameObject.SetActive(true);

        if(Continue != null)
            Continue.SetActive(isVictory);
        if (isVictory)
        {
            GameObject toSelect = Continue != null ? Continue : Quit;
            EventSystem.SetSelectedGameObject(toSelect);
        }
        else
        {
            EventSystem.SetSelectedGameObject(Restart);
        }
    }
}
