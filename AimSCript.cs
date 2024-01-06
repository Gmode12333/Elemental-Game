using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class AimSCript : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera animCamera;
    [SerializeField] LayerMask aimColliderMask = new LayerMask();
    public Vector3 shootDir;
    private ThirdPersonController controller;
    private void Start()
    {
        controller = GetComponent<ThirdPersonController>();
    }
    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if(Physics.Raycast(ray, out RaycastHit raycasthit, 999f , aimColliderMask))
        {
            mouseWorldPosition = raycasthit.point;
        }
        if (InGameManager.Instance.isPlayerAttacking)
        {
            controller.SetRotateOnMove(false);
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDir = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * 20f);
        }
        else
        {
            controller.SetRotateOnMove(true);
        }
        shootDir = mouseWorldPosition;
    }
}
