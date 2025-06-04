using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private Button menu;
    [SerializeField] private Button voltar;
    [SerializeField] private Button opcao;

    public GameObject opcaoPausePanel;



    private void Start()
    {
        gameObject.SetActive(false);
        menu.onClick.AddListener(BackToMenu);
        voltar.onClick.AddListener(Back);
        opcao.onClick.AddListener(Opcao);
    }

    private void BackToMenu()
    {
        GameManager.Instance.Despausar();
        GameManager.Instance.LevelManager.LoadScene("Menu", "CrossFade");
    }

    private void Back()
    {
        GameManager.Instance.Despausar();
        gameObject.SetActive(false);
    }

    private void Opcao()
    {
        opcaoPausePanel.gameObject.SetActive(true);
    }

    public void ShowPanelPause()
    {
        gameObject.transform.SetAsLastSibling();
        GameManager.Instance.Pausar();
        gameObject.SetActive(true);
    }

}
