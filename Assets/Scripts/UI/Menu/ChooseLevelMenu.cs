using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevelMenu : MonoBehaviour
{
    [SerializeField]
    private GameFlowController GameFlow;
    [SerializeField]
    private MenuContent Content;
    [SerializeField]
    private Button ButtonPrefab;

    private void Awake()
    {
        foreach (GameLevel level in GameFlow.Flow.AllLevels.Reverse())
        {
            GameObject obj = Instantiate(ButtonPrefab.gameObject, transform);
            Button button = obj.GetComponent<Button>();
            TextMeshProUGUI text = obj.GetComponentInChildren<TextMeshProUGUI>();

            text.text = level.Name.ToLower();
            button.onClick.AddListener(() => LoadLevel(level));
            obj.transform.SetAsFirstSibling();
            Content.FirstSelection = obj;
        }
    }

    private void LoadLevel(GameLevel level)
    {
        GameFlow.StartLevel(level);
    }
}
