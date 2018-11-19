using System.Collections.Generic;
using UnityEngine;

public abstract class MonoCatchQteStrategy : MonoBehaviour, ICatchQteStrategy
{
    public abstract IEnumerable<Transform> GetItems(Transform parent);

    public abstract bool TryGetOrigin(Transform obj, out Vector3 position);

    public abstract void Success(Transform item);

    public abstract void Fail(Transform item);

    public abstract string GetFinalText(int catched, int missed);
}
