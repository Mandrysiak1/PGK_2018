using Game.Initialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

    public SceneReference MainMenu;
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Loadmenu()
    {
        SceneManager.LoadScene(MainMenu.SceneName);
    }

 

}

    
