using QTE;
using UnityEngine;

public class QteTest : MonoBehaviour
{
    [SerializeField]
    private CatchBeerQTE CatchBeerQte;

    [SerializeField]
    private TipCatchQteStrategy _qteStrategy;

    public KeyCode RunKey;

    private void Update()
    {
        if (Input.GetKeyDown(RunKey))
        {
            RunCatchQte();
        }
    }

    public void RunCatchQte()
    {
        GameObject obj = Instantiate(CatchBeerQte.gameObject);
        CatchBeerQTE qte = obj.GetComponent<CatchBeerQTE>();
        obj.transform.SetAsLastSibling();
        qte.Run(_qteStrategy, OnQteEnd);
    }

    private void OnQteEnd()
    {

    }
}
