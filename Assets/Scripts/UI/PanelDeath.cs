using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelDeath : MonoBehaviour
{

    [SerializeField] private Button recomecar;
    [SerializeField] private Button menu;

    void Awake()
    {
        gameObject.SetActive(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        recomecar.onClick.AddListener(ReloadScene);
        menu.onClick.AddListener(BackToMenu);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ReloadScene()
    {
        GameManager.Instance.Despausar();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);//Recarrega a scena completamente
    }

    private void BackToMenu()
    {
        GameManager.Instance.Despausar();
        GameManager.Instance.LevelManager.LoadScene("Menu", "CrossFade");
    }

    public void ShowPanel()
    {
        gameObject.SetActive(true);
    }
}
