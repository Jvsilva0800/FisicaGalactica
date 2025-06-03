using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel;
    [Header("Menu UI properties")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    private void OnEnable()//Esse método é chamado automaticamente quando o objeto se torna ativo na cena
    {
        optionsPanel.SetActive(false);

        startButton.onClick.AddListener(GoToGameplayScene);
        optionsButton.onClick.AddListener(OpenOptionsMenu);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void GoToGameplayScene()
    {//Troca para a cena chamada "Gameplay". É necessário mudar o nome da scene na Unity para o escolhido abaixo
        GameManager.Instance.LevelManager.LoadScene("Tutorial", "CrossFade");
    }
    private void OpenOptionsMenu()
    {
        optionsPanel.SetActive(true);
    }

    private void ExitGame()
    {
        /*
        Se estiver rodando no Editor da Unity, ele para a simulação (isPlaying = false).
        Se for um build do jogo, ele fecha o aplicativo (Application.Quit()).
        */
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
