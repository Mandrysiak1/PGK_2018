using Assets.PGKScripts.Enums;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private Toggle m_MenuToggle;
	private float m_TimeScaleRef = 1f;
    private float m_VolumeRef = 1f;
    private bool m_Paused;
    public GameObject standardSetObject;
    private MainScript mainScript;

    public void ResetFirstButton()
    {
        //if(mainScript.CurrentGameState == Assets.PGKScripts.Enums.GameState.Playing)
        //{
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(standardSetObject);
        //}
    }
    private void Start()
    {
        m_MenuToggle = GetComponent<Toggle>();
        this.mainScript = FindObjectOfType<MainScript>();
        mainScript.GameStatusChanged.AddListener(GameStateChanged);
    }
    void Awake()
    {
        //ResetFirstButton();
        //m_MenuToggle = GetComponent <Toggle> ();
	}
    private void GameStateChanged(GameState prevS, GameState newS)
    {
        if (newS == GameState.Paused)
        {
            ResetFirstButton();
        }
    }

    private void MenuOn ()
    {
        m_TimeScaleRef = Time.timeScale;
        Time.timeScale = 0f;

        m_VolumeRef = AudioListener.volume;
        AudioListener.volume = 0f;

        m_Paused = true;
    }


    public void MenuOff ()
    {
        Time.timeScale = m_TimeScaleRef;
        AudioListener.volume = m_VolumeRef;
        m_Paused = false;
    }


    public void OnMenuStatusChange ()
    {
        if (m_MenuToggle.isOn && !m_Paused)
        {
            MenuOn();
        }
        else if (!m_MenuToggle.isOn && m_Paused)
        {
            MenuOff();
        }
    }

    /*

#if !MOBILE_INPUT
	void Update()
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
		    m_MenuToggle.isOn = !m_MenuToggle.isOn;
            Cursor.visible = m_MenuToggle.isOn;//force the cursor visible if anythign had hidden it
		}
	}
#endif
*/

}
