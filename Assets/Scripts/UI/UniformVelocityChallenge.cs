using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UniformVelocityChallenge : MonoBehaviour
{
    public string corretAnswer = "A";
    [Header("Referências às UI Buttons")]
    public Button buttonA;
    public Button buttonB;
    [Header("Referências de UI")]
    [SerializeField] private TextMeshProUGUI feedbackText;      // Texto para exibir “errou/acertou”

    [Header("Force Fild Object")]
    [SerializeField] private GameObject forceFild; // gameObject do campo de força do portão

    private bool isPanelActive, rightAnwserClicked = false;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        // adiciona listeners programaticamente
        buttonA.onClick.AddListener(() => HandleAnswer("A"));
        buttonB.onClick.AddListener(() => HandleAnswer("B"));
    }


    public void ShowPanel()
    {
        feedbackText.text = "";
        gameObject.SetActive(true);
        isPanelActive = true;
    }

    public void HidePanel()
    {
        StartCoroutine(HideAfter(1.5f));//espera 1,5 segundos antes de esconder o panel
    }
    private IEnumerator HideAfter(float delay)
    {
        yield return new WaitForSeconds(delay);
        isPanelActive = false;
        gameObject.SetActive(false);
    }


    private void HandleAnswer(string answer)
    {
        if (!isPanelActive) return;

        if (rightAnwserClicked) return;
        if (answer == corretAnswer)
        {
            rightAnwserClicked = true;

            feedbackText.text = "Parabéns, você acertou!";
            GameManager.Instance.InputManager.EnablePlayerInput();
            GameManager.Instance.UiManager.tipsUniformVelocity.HidePanelTips();
            GameManager.Instance.UiManager.smallTip.HidePanelTips();
            HidePanel();
            forceFild.SetActive(false);
        }
        else
        {
            // Errou - permite tentar novamente
            feedbackText.text = $"Resposta incorreta. Tente novamente.";
        }
    }
}
