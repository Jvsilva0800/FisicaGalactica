using UnityEngine;

public class Phase2ProgressionManager : MonoBehaviour
{
    [SerializeField] private GameObjectCreator gameObjectCreator;//so preciso da posição de um gameobject pois todos se sobrepoe
    [SerializeField] private GameObject[] rampas;//Referencia as rampas
    [SerializeField] private int maxStructures = 3; // Número máximo de estruturas
    [SerializeField] private PortalNextLevel portal;

    private int currentStructureCount = 0;

    public void OnChallengeSucceeded()
    {
        currentStructureCount++;
        Debug.Log("Rampa completa: " + currentStructureCount);

        if (currentStructureCount < maxStructures)
        {
            rampas[currentStructureCount - 1].SetActive(false);//desativa a rampa anterior
            rampas[currentStructureCount].SetActive(true);
            gameObjectCreator.BuildGameObject();//Cria novamente o bloco no início da esteira
        }
        else
        {
            Debug.Log("Fase completa!");
            // Aqui você pode implementar a lógica de finalização da fase, exibir mensagem de vitória, ou transitar para o próximo nível.
            GameManager.Instance.InputManager.EnablePlayerInput();//Vai habilitar a movimentação do player assim que o mesmo responda as três perguntas corretamente.
            portal.ActivatePortal();
        }
    }
}
