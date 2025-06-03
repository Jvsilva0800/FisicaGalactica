using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    [SerializeField] private Text resultsText;//Referencia ao text dentro do Painel

    void Awake()
    {
        DisableDisplay();//Antes do primeiro frame o gameObject é escondido
    }

    ///PRIMEIRA FASE
    public void DisplayResults(float elapsedTime, float distanceTraveled)//Irá mostrar no text a Variação de distancia e tempo para o cálculo da velocidade média
    {
        gameObject.SetActive(true);//gameObject é mostrado
        // Formata a mensagem com os valores, por exemplo, com duas casas decimais
        resultsText.text = $"Variação de Tempo: {elapsedTime:F2} s\nVariação de Espaço: {distanceTraveled:F2} m";
    }

    public void DisplayTimeVariation(float initialTime, float finalTime)
    {
        gameObject.SetActive(true);//gameObject é mostrado
        resultsText.text = $"Tempo Inicial: {initialTime:F2} s\nTempo Final: {finalTime:F2} s";
    }


    ///TERCEIRA FASE
    public void DisplayAverageAcceleration(float elapsedTime, float finalVelocity)//Irá mostrar no text a variação de tempo e a Velocidade final do objeto para cálculo da aceleração média.
    {
        gameObject.SetActive(true);
        resultsText.text = $"Tempo decorrido(Δt): {elapsedTime:F2} s\nVelocidade(Δv): {finalVelocity:F2} m";
    }

    public void DisplayElapsedTime(float averageAcceleration, float finalVelocity)//Irá mostrar no text a aceleração média e a velocidade final para que o player calcule o tempo que o objeto demorou para percorer o trecho.
    {
        gameObject.SetActive(true);
        resultsText.text = $"Aceleração média: {averageAcceleration:F2} m/s² \nVelocidade final: {finalVelocity:F2} m";
    }

    public void DisplayFinalVelocity(float averageAcceleration, float elapsedTime)//Irá mostrar no text a acelração média e a variação de tempo para o cálculo da velocidade final
    {
        gameObject.SetActive(true);
        resultsText.text = $"Aceleração média: {averageAcceleration:F2} m/s² \nTempo final: {elapsedTime:F2} s";
    }


    ///DESABILITA O DISPLAY
    public void DisableDisplay()
    {
        gameObject.SetActive(false);
    }
}
