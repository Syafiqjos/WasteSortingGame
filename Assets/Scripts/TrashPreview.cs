using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPreview : MonoBehaviour
{
    public float rotateSpeed;
    public float rotatePos;
    public float rotateLength;

    private float rotation = 0;

    [Range(0, 100)] public float minRotateSpeed;
    [Range(0, 100)] public float maxRotateSpeed;

    [Range(0, 180)] public float minRotatePos;
    [Range(0, 180)] public float maxRotatePos;

    [Range(0, 180)] public float minRotateLength;
    [Range(0, 180)] public float maxRotateLength;

    private void Awake()
    {
        RandomizeProperties();
    }

    private void Update()
    {
        rotatePos = (rotatePos + Time.deltaTime * rotateSpeed) % 360;

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(rotatePos / 180 * Mathf.PI) * rotateLength);
    }

    private void RandomizeProperties()
    {
        rotateSpeed = Random.Range(minRotateSpeed, maxRotateSpeed);
        rotatePos = Random.Range(minRotatePos, maxRotatePos);
        rotateLength = Random.Range(minRotateLength, maxRotateLength);

        rotation = rotatePos;
    }
}
