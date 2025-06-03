using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlockSeven : Block
{
    private float initialSpeed = 15f;
    private float deceleration = 5f;
    private float BlockDirection, initialTime;
    private bool locker = true;//trava a entrada da função quando o bloco ja estiver parado

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Speed = initialSpeed;
        initialTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //movimentação do bloco.
        BlockDirection = 1 * Time.deltaTime * Speed;
        transform.Translate(BlockDirection, 0, 0);
        // 1) desacelera continuamente: Δv = a * Δt
        if (Speed > 0f)
            Speed = Mathf.Max(0f, Speed - deceleration * Time.deltaTime);
        if (Speed == 0 && locker)//Quando a velocidade do bloco é zero e o locker permite a entrada, é chamada a função OnStop que exibe os peineis de tips e challenge
        {
            OnStop();
            locker = false;
        }

    }

    private void OnStop()
    {
        float finalTime = Time.time;
        float answer = Mathf.Floor((Speed - initialSpeed) / (finalTime - initialTime));
        GameManager.Instance.UiManager.averageAcceleration2.ShowPanel(initialSpeed, (int)(finalTime - initialTime), Speed, answer);
        GameManager.Instance.UiManager.tipsAverageAcceleration2.ShowPanelTips();
        GameManager.Instance.UiManager.smallTipAverageAcceleration2.ShowPanelTips();
    }
}
