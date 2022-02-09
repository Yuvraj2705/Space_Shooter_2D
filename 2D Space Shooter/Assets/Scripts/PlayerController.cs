using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] float xPadding;
    [SerializeField] float speed;
 
    private float xMax, xMin;

    void Start()
    {
        SettingBounderies();
    }

    void Update()
    {
        PlayerMove();
    }

    void SettingBounderies()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + xPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - xPadding;
    }

    void PlayerMove()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX,xMin,xMax);

        transform.position = new Vector2(newXPos, transform.position.y);
    }
}
