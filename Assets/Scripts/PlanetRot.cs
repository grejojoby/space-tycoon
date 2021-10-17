using UnityEngine;
using System.Collections;

public class PlanetRot : MonoBehaviour
{

    public float degreesPerSec = 30f;

    void Update()
    {
        float rotAmount = degreesPerSec * Time.deltaTime;
        float curRot = transform.localRotation.eulerAngles.z;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot + rotAmount));
    }

}