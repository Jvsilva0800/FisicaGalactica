using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;

    private float timer;
    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)//Tem de verificar pois caso o player não exista mais na scena não dará erro
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < 12)
            {
                timer += Time.deltaTime;
                if (timer > 2)
                {
                    timer = 0;
                    Shoot();
                }

            }
        }

    }

    void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        SoundManager.Instance.PlaySound3D("Enemy Shoot", transform.position);
    }
}
