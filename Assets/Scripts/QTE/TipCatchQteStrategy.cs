using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipCatchQteStrategy : MonoCatchQteStrategy
{
    public int Amount = 4;
    [SerializeField]
    private GameObject Prefab;

    [SerializeField]
    private RectTransform Origin;

    public override IEnumerable<Transform> GetItems(Transform parent)
    {
        for (int i = 0; i < Amount; i++)
        {
            GameObject obj = Instantiate(Prefab, parent);

            yield return obj.transform;
        }
    }

    public override bool TryGetOrigin(Transform obj, out Vector3 position)
    {
        position = Origin.position;
        return true;
    }

    public override void Success(Transform item)
    {
        Destroy(item.gameObject);
    }

    public override void Fail(Transform item)
    {
        Destroy(item.gameObject);
    }

    public override string GetFinalText(int catched, int missed)
    {
        if (catched == 0)
            return "NO TIP FOR YOU CROCK!";
        else if (missed == 0)
            return "YOU GOT THEM ALL!";
        else
            return "TAKE IT AND GO AWAY!";
    }
}
