using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private Button menu;
    [SerializeField] private Button voltar;

    private void Start()
    {
        gameObject.SetActive(false);
        menu.onClick.AddListener(BackToMenu);
        voltar.onClick.AddListener(Back);
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

    public void ShowPanelPause()
    {
        gameObject.transform.SetAsLastSibling();
        GameManager.Instance.Pausar();
        gameObject.SetActive(true);
    }

}
