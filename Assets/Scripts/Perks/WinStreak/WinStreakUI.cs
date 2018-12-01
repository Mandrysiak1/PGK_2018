using Assets.PGKScripts.Interfaces;
using TMPro;
using UnityEngine;

public class WinStreakUI : MonoBehaviour {

	[SerializeField]
    private MonoWinStreakSource winStreakSource;
    public TextMeshProUGUI counter;
	// Use this for initialization
	void Start () {
        winStreakSource.WinStreakChanged.AddListener(textChanger);
	}

    private void textChanger(int arg0, int arg1)
    {
        this.counter.text = arg1.ToString();
    }

    // Update is called once per frame
    void Update () {

	}
}
