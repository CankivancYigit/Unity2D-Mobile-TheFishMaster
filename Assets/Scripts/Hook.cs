using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hook : MonoBehaviour
{
    [SerializeField] Transform hookTransform;

    Collider2D myCollider;
    Camera mainCamera;

    int lenght;
    int strenght;
    int fishCount;

    bool canMove;

    Tweener cameraTween;

    void Awake()
    {
        myCollider = GetComponent<Collider2D>();
        mainCamera = Camera.main;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (canMove && Input.GetMouseButton(0))
        {
            Vector3 vector = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 position = transform.position;
            position.x = vector.x;
            transform.position = position;
        }
        
    }

    public void StartFishing()
    {
        lenght = -50;
        strenght = -3;
        fishCount = 0;
        float time = (-lenght) * 0.1f;

        cameraTween = mainCamera.transform.DOMoveY(lenght, 1 + time * 0.25f, false).OnUpdate(delegate
        {
            if (mainCamera.transform.position.y <= -7)
            {
                transform.SetParent(mainCamera.transform);
            }
        }).OnComplete(delegate
        {
            myCollider.enabled = true;
            cameraTween = mainCamera.transform.DOMoveY(0, time * 4, false).OnUpdate(delegate
            {
                if (mainCamera.transform.position.y >= -20)
                {
                    StopFishing();
                }
            });
        });

        myCollider.enabled = false;
        canMove = true;
    }

    void StopFishing() 
    {
        canMove = false;
        cameraTween.Kill(false);
        cameraTween = mainCamera.transform.DOMoveY(0 , 2, false).OnUpdate(delegate
        {
            if (mainCamera.transform.position.y >= -7)
            {
                transform.SetParent(null);
                transform.position = new Vector2(transform.position.x, -4.1f);
            }
        }).OnComplete(delegate
        {
            transform.position = Vector2.down * 4.1f;
            myCollider.enabled = true;
        });
    }
}
