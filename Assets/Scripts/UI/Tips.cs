using TMPro;
using UnityEngine;

public class Tips : MonoBehaviour
{

    private void Awake()
    {
        HidePanelTips();//Antes do primeiro frame da cena as Dicas são escondidas

    }
    public void ShowPanelTips()
    {
        gameObject.transform.SetAsLastSibling();//Isso fará com que o Pnel em questão seja renderizado acima dos outros no mesmo Canvas.
        gameObject.SetActive(true);
    }

    public void HidePanelTips()
    {
        gameObject.SetActive(false);
    }

}
