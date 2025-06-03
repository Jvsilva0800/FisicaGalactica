using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Block3Tracker : MonoBehaviour
{
    // Armazena a posição inicial do bloco (quando instanciado) e a posição em tempo real atualizada pelo update
    private Vector3 initialPosition, realTimePosition;
    // Armazena o tempo (em segundos) de quando o bloco foi instanciado e o tempo em tempo real atualizado pelo update
    private float initialTime, inRealTime;
    //Armazena os calculos de tempo e distancias percorridas ate o momento
    private float distanceTravelled, timeTravelled;
    // Se necessário, um fator de conversão (por padrão, 1 unidade = 1 metro)
    private float unitToMeter = 1.0f;
    [SerializeField] private TextMeshPro meter;//Texto que exibe os metros percorridos do bloco em tempo real
    [SerializeField] private TextMeshPro time;//Texto que exibe o tempo do bloco desde a sua criação


    private void Start()
    {
        // Registra a posição e o tempo no momento da criação
        initialPosition = transform.position;
        initialTime = Time.time;
    }
    private void Update()
    {
        realTimePosition = transform.position;
        inRealTime = Time.time;
        distanceTravelled = (int)Vector3.Distance(initialPosition, realTimePosition) * unitToMeter;//Calculado a distancia percorrida desde o ponto inicial ate o ponto em que o objeto se encontra, com conversão para metros 
        timeTravelled = (int)Math.Ceiling(inRealTime - initialTime);//arredonda pra cima
        meter.text = $"{distanceTravelled} m";//Atualização do texto com a distancia percorrida
        time.text = $"{timeTravelled} s";//atualização do texto com o tempo de vida do bloco
    }

    private void OnDestroy()
    {
        GameManager.Instance.UiManager.tipsUniformVelocity.ShowPanelTips();
        GameManager.Instance.UiManager.uniformVelocityChallenge.ShowPanel();
        GameManager.Instance.UiManager.smallTip.ShowPanelTips();//Dica sobre o display da esteira

    }

    public float GetDistanceTravelled() => distanceTravelled;
    public float GetTimeTravelled() => timeTravelled;

}
