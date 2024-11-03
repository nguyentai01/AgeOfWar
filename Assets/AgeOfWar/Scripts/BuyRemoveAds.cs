using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;

public class BuyRemoveAds : MonoBehaviour
{
    [SerializeField] string idProduct;
    [SerializeField] TextMeshProUGUI txtPrice;

    private void Start()
    {
        var product = CodelessIAPStoreListener.Instance.GetProduct(idProduct);
        txtPrice.text = product.metadata.localizedPriceString;
    }

    public void EventBuySuccess()
    {
        EventUi.Ins.EventCompleteBuyRemoveAds();
        EventLater();
    }

    public void EventBuyFail()
    {
        UiManger.Ins.EventFailPay();
    }

    public void EventLater()
    {
        Destroy(gameObject);
    }
}
