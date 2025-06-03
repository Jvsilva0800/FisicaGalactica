using UnityEngine;

public class TilePonte : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void ActivateBridge()
    {
        gameObject.SetActive(true);
    }
}
