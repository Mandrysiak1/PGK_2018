using System.Collections.Generic;
using UnityEngine;

public interface ICatchQteStrategy
{
    IEnumerable<Transform> GetItems(Transform parent);
    bool TryGetOrigin(Transform obj, out Vector3 position);
    void Success(Transform item);
    void Fail(Transform item);
    string GetFinalText(int catched, int missed);
}
