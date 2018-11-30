using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField]
    private GameFlowController Flow;

    public GameObject DiffMenu;
    public TutorialScript Tutorial;
    public GameObject Level;

    public Button DummyButton;
    public GameObject standardSetObject;

    public void ResetFirstButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(standardSetObject);
    }
    public void Start()
    {
        ResetFirstButton();
    }

    public void Awake()
    {
        ResetFirstButton();
    }

    public void PlayGame()
    {
        StartFirstLevel();
    }

    private void StartFirstLevel()
    {
        Flow.StartFirstLevel();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeToDifficultyMenu()
    {
        gameObject.SetActive(false);
        DiffMenu.SetActive(true);
    }

    public void ChangeLevel()
    {
        gameObject.SetActive(false);
        Level.SetActive(true);
    }
}
