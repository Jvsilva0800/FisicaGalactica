using UnityEngine;

public class PortalNextLevel : MonoBehaviour
{

    [SerializeField] private string nextLevelName;
    private void Awake()
    {
        gameObject.SetActive(false);//Ao inicio da cena o objeto é desativado
    }
    public void ActivatePortal()
    {
        GameManager.Instance.InputManager.EnablePlayerInput();
        gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))//So é ativado se o collider for o do Player
        {
            GameManager.Instance.LevelManager.LoadScene(nextLevelName, "CrossFade");
        }

    }
}
