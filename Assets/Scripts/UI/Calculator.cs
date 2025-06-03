using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Calculator : MonoBehaviour
{
    public TextMeshProUGUI displayText;//É o componente TextMeshPro que exibirá a entrada atual do usuário e os resultados.
    private string currentInput = "";//Armazena a string que representa a expressão matemática que o usuário está digitando
    private double result = 0.0;
    private bool isShowing = false;//Uma flag booleana que indica se a calculadora está atualmente visível.

    private void Awake()
    {
        gameObject.SetActive(false);//Antes do primeiro frame da cena a calculadora é escondida
    }


    public void OnButtonClick(string buttonValue)
    {
        if (buttonValue == "=")// Chama o método para calcular o resultado.
        {
            CalculateResult();
        }
        else if (buttonValue == "C")//Chama o método que limpa a tela de resultado da calculadora
        {
            ClearInput();
        }
        else
        {// Acrescenta o valor pressionado à string de entrada e atualiza o display.
            currentInput += buttonValue;
            UpdateDisplay();
        }
    }



    public void CalculateResult()
    {
        try
        {
            /*
            Compute: Utiliza a classe DataTable para avaliar a expressão contida em currentInput. Esse método interpreta a string matemática e retorna o resultado.
            System.Convert.ToDouble: Converte o resultado da operação para um tipo double.
            */
            result = System.Convert.ToDouble(new System.Data.DataTable().Compute(currentInput, ""));

            currentInput = result.ToString();
            UpdateDisplay();
        }
        catch (System.Exception)
        {
            currentInput = "Error";
            UpdateDisplay();
        }

    }

    //Zera a entrada e o resultado, atualizando o display para refletir que a calculadora foi limpa.
    private void ClearInput()
    {
        currentInput = "";
        result = 0.0;
        UpdateDisplay();
    }

    //Define o texto do componente displayText para mostrar a string atualmente armazenada em currentInput.
    private void UpdateDisplay()
    {
        displayText.text = currentInput;
    }

    //Função que é responsável por mostrar e esconder a calculadora da cena
    public void ShowPanelCalculator()
    {
        if (!isShowing)
        {
            gameObject.transform.SetAsLastSibling();//Isso fará com que o Pnel em questão seja renderizado acima dos outros no mesmo Canvas.
            gameObject.SetActive(true);
            isShowing = true;

        }
        else
        {
            gameObject.SetActive(false);
            isShowing = false;

        }

    }
}
