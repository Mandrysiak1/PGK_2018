using UnityEngine;

[CreateAssetMenu(menuName="Beerfest/Save System")]
public class SaveSystem : ScriptableObject
{
    public bool Enabled = true;
    [SerializeField]
    private GameSettings Settings;

    public string KeyPrefix = "MainSave_";
    public bool Reset = false;

    private bool IsLoaded = false;

    public void Sync()
    {
        if (!Enabled)
            return;

        if (!IsLoaded)
        {
            Load();
            IsLoaded = true;
        }

        Save();
    }

    private void OnEnable()
    {
        IsLoaded = false;
        TryReset();
    }

    private void OnDisable()
    {
        IsLoaded = false;
        TryReset();
    }

    private void TryReset()
    {
        if (Reset)
        {
            Reset = false;
            PlayerPrefs.DeleteAll();
        }
    }

    private void Save()
    {
        PlayerPrefs.SetInt(KeyPrefix + "Tip", UpgradeClass.Tip);
        PlayerPrefs.SetFloat(KeyPrefix + "Speed", UpgradeClass.SpeedModif);
        PlayerPrefs.SetInt(KeyPrefix + "Beers", UpgradeClass.BeerModif);
        PlayerPrefs.SetInt(KeyPrefix + "BeerTimes", UpgradeClass.BeerTimes);
        PlayerPrefs.SetInt(KeyPrefix + "SpeedTimes", UpgradeClass.SpeedTimes);
        PlayerPrefs.SetInt(KeyPrefix + "Invulnerability", UpgradeClass.invulnerabilityPurchased ? 1 : 0);
    }

    private void Load()
    {
        UpgradeClass.LoadTips(PlayerPrefs.GetInt(KeyPrefix + "Tip", UpgradeClass.Tip)); // wywo�ywanie eventu powodowa�o �e, po restrcie wy�wietla�a si� animacja lec�cych monet np 20;
        //UpgradeClass.Tip = PlayerPrefs.GetInt(KeyPrefix + "Tip", UpgradeClass.Tip);
        UpgradeClass.SpeedModif = PlayerPrefs.GetFloat(KeyPrefix + "Speed", UpgradeClass.SpeedModif);
        UpgradeClass.BeerModif = PlayerPrefs.GetInt(KeyPrefix + "Beers", UpgradeClass.BeerModif);
        UpgradeClass.BeerTimes = PlayerPrefs.GetInt(KeyPrefix + "BeerTimes", UpgradeClass.BeerTimes);
        UpgradeClass.SpeedTimes = PlayerPrefs.GetInt(KeyPrefix + "SpeedTimes", UpgradeClass.SpeedTimes);
        UpgradeClass.invulnerabilityPurchased = PlayerPrefs.GetInt(KeyPrefix + "Invulnerability", UpgradeClass.invulnerabilityPurchased ? 1 : 0) == 1;
    }
}

