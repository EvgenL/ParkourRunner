using UnityEngine;
using AEngine;

public class AppManager : MonoSingleton<AppManager>
{
    [SerializeField] private InAppManager _purchaseManager;

    public InAppManager PurchaseManager { get; private set; }

    protected override void Init()
    {
        base.Init();
        this.PurchaseManager = _purchaseManager;
    }
}
