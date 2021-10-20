using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public int trashID;
    public TrashType trashType;
    public float fallSpeed;
    public float extraFallSpeed;
    public float diagonalSpeed;
    public float rotateSpeed;

    [Range(0, 10)] public float minFallSpeed;
    [Range(0, 10)] public float maxFallSpeed;

    [Range(0, 180)] public float minRotateSpeed;
    [Range(0, 180)] public float maxRotateSpeed;

    private Rigidbody2D rb2;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite[] sprites;

    private void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        RandomizeProperties();
    }

    private void RandomizeProperties()
    {
        fallSpeed = Random.Range(minFallSpeed, maxFallSpeed) + extraFallSpeed;

        int leftRightMult = Random.value >= 0.5 ? 1 : -1;
        rotateSpeed = leftRightMult * Random.Range(minRotateSpeed, maxRotateSpeed);

        trashID = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[trashID];
    }

    private void Update()
    {
        FallController();
        RotateController();
    }

    private void FallController()
    {
        if (rb2)
        {
            rb2.velocity = new Vector2(diagonalSpeed, -fallSpeed);
        }
    }

    private void RotateController()
    {
        if (rb2)
        {
            rb2.angularVelocity = rotateSpeed;
        }
    }
}
