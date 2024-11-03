using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionMenu : MonoBehaviour
{
    public GameObject panelCollection;
    public GameObject ShopCollection;

    [SerializeField]
    Transform paentPanelCollection;
    [SerializeField]
    Transform parentShopCollection;
    private GameObject plane;
    private GameObject shopUi;

    public void OpenCollection()
    {
        plane = Instantiate(panelCollection, paentPanelCollection);

    }
    public void OpenShop()
    {
        shopUi = Instantiate(ShopCollection, parentShopCollection);

    }
    public void DestroyPlane()
    {
        if (plane)
        {
            Destroy(plane);
        }
    }
    public void DestroyShop()
    {
        if (shopUi)
        {
            Destroy(shopUi);
        }
    }
}
