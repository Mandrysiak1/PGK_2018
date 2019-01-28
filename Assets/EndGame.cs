using Game.Initialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

    [SerializeField]
    private EventSystem EventSystem = null;
    [SerializeField]
    GameObject toSelect;
    public SceneReference MainMenu;
    void Start () {
        if (EventSystem == null)
            EventSystem = FindObjectOfType<EventSystem>();
        EventSystem.SetSelectedGameObject(toSelect);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Loadmenu()
    {
        SceneManager.LoadScene(MainMenu.SceneName);
    }

 

}

    
