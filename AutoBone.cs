
using Unity.VisualScripting;
using UnityChan;
using UnityEngine;


public class AutoBone : MonoBehaviour
{
    private void Reset()
    {
        GetSpringBone(transform);
        var springManager = GetComponentInParent<SpringManager>();
        if(springManager != null )
        {
            springManager.springBones = springManager.GetComponentsInChildren<SpringBone>();
        }
        DestroyImmediate(this);
    }

    private static void GetSpringBone(Transform root)
    {
        if (root.childCount == 0)
            return;
            
        var child = root.GetChild(0);
        var springBone = root.GetOrAddComponent<SpringBone>();
        springBone.child = child;
        GetSpringBone(child);
    }
}
