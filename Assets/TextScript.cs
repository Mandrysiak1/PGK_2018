using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {

    public Text txt;
    public TextMeshProUGUI Beertxt;
    public TextMeshProUGUI Speedtxt;
    public TextMeshProUGUI Mariotxt;
    private void Update()
    {
        txt.text = UpgradeClass.Tip.ToString();
        Beertxt.text = "increase beer capacity\n" + (10 * UpgradeClass.BeerTimes).ToString() + " coins";
        Speedtxt.text = "increase movement speed\n" + (5 * UpgradeClass.SpeedTimes).ToString() + " coins";
        Mariotxt.text = "beer loss invulnerability\n" + 1000 + " coins";
        if(UpgradeClass.invulnerabilityPurchased == true)
        {
            Mariotxt.alpha = 100;
        }
    }

    public void Return()
    {
        SceneManager.UnloadSceneAsync("Shop");
        UpgradeClass.exited = true;
    }
    public void StarPurchase()
    {
        UpgradeClass.MarioStar();
    }
    public void BeerPurchase()
    {
        UpgradeClass.ChangeMaxBeer();
    }
    public void SpeedPurchase()
    {
        UpgradeClass.ChangeSpeedModif();
    }
}
