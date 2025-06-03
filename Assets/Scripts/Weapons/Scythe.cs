using UnityEngine;

public class Scythe : MonoBehaviour
{
    private void Awake()
    {
        DisableMeleeWeapon();
    }
    public void DisableMeleeWeapon()//Será desabilitada novemente no último frame da animação da foice
    {
        gameObject.SetActive(false);
    }

    public void EnableMeleeWeapon()
    {
        gameObject.SetActive(true);
    }
}
