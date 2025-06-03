using UnityEngine;

public class ReferencialManager : MonoBehaviour
{
    private Transform referencialPoint; //Ponto de teletrasnporte do player localizado em cima do bloco
    public Transform finishPoint; //Ponto de teletrasnporte do player localizado no chão
    public Transform panelReferencialChallenge;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void PlayerToBlock()//Teletransporta o player para o bloco
    {
        referencialPoint = GameObject.FindGameObjectWithTag("ReferencialPoint").transform;
        panelReferencialChallenge.SetParent(player.transform);//Faz com que o panel acompanhe o player para quando a câmera volte a acompanhar o player também.
        player.transform.position = referencialPoint.position;
    }

    public void PlayerToGround()//Teletransporta o player para o chão
    {
        panelReferencialChallenge.SetParent(null);//Faz com que pare de seguit o player
        player.transform.position = finishPoint.position;
    }

}
