using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Button backButton;
    private void Start()
    {
        //O botao de voltar irá chamar a função "ClosePanel" quando clicado
        backButton.onClick.AddListener(ClosePanel);

    }

    private void ClosePanel()
    {
        this.gameObject.SetActive(false);
    }
}
