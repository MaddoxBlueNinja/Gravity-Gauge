using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target;

    public float offsetZ;
    public float rotateOffset;
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 thisPos = this.transform.position;
        Vector3 targetPos = target.transform.position;

        thisPos.z = targetPos.z + offsetZ;

        this.transform.position = thisPos;

        Vector3 aimPos = new Vector3(
            thisPos.x,
            targetPos.y - (rotateOffset * (thisPos.y - targetPos.y)),
            targetPos.z);

        transform.LookAt(aimPos);
    }
}
