using UnityEngine;
using UnityEngine.InputSystem;

public class Block5Tracker : MonoBehaviour
{
    private Block block;
    private float initialTime, initialSpeed, finalTime, finalSpeed;

    void Awake()
    {
        block = gameObject.GetComponent<Block>();
    }

    void Start()
    {
        initialTime = Time.time;
        initialSpeed = block.GetSpeed();
    }

    private void OnDestroy()
    {
        finalTime = Time.time;
        finalSpeed = block.GetSpeed();

        float deltaTime = (int)(finalTime - initialTime);
        float deltaVelocity = (int)(finalSpeed - initialSpeed);

        float averageAcceleration = deltaVelocity / deltaTime;

        GameManager.Instance.UiManager.averageAcceleration.ShowPanel(initialSpeed, deltaTime, deltaVelocity, averageAcceleration);
        GameManager.Instance.UiManager.tipsAverageAcceleration.ShowPanelTips();
    }
}
