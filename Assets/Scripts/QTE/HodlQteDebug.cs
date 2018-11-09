using UnityEngine;

namespace QTE
{
    public class HodlQteDebug : MonoBehaviour
    {
        public HodlQte Qte;

        private void Start()
        {
            Qte.Run(_ => { });
        }
    }
}