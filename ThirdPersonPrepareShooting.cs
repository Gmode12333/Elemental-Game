
using StarterAssets;
using UnityEngine;

public class ThirdPersonPrepareShooting : MonoBehaviour
{
    public GameObject pfElement;

    private StarterAssetsInputs starterAssetsInputs;
    private void Start()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }
    private void Update()
    {
        if (starterAssetsInputs.aim)
        {
            pfElement.SetActive(true);
        }
        else
        {
            pfElement.SetActive(false);
        }
    }
}
