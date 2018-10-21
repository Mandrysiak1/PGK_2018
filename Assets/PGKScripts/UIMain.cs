using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : MonoBehaviour {

    MainScript mainScript;
    public Text howManyBeers;
    public Slider bigBar;

    // Use this for initialization
    void Start () {
        mainScript = (MainScript)FindObjectOfType(typeof(MainScript));
        mainScript.PropertyChanged += MainScript_PropertyChanged;
    }

    private void MainScript_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName.Equals("BeerCount"))
            this.howManyBeers.text = ((MainScript)sender).BeerCount + " x";
        if (e.PropertyName.Equals("DissatisfactionValue"))
            this.bigBar.value = ((MainScript)sender).DissatisfactionValue;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
