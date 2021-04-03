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

    List<Fish> caughtFishes;

    bool canMove;

    Tweener cameraTween;

    void Awake()
    {
        myCollider = GetComponent<Collider2D>();
        mainCamera = Camera.main;
        caughtFishes = new List<Fish>();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Fish" && fishCount != strenght)
        {
            fishCount++;
            Fish fish = other.gameObject.GetComponent<Fish>();
            fish.Hooked();
            caughtFishes.Add(fish);
            other.transform.SetParent(transform);
            other.transform.position = hookTransform.position;
            other.transform.rotation = hookTransform.rotation;
            other.transform.localScale = Vector3.one;

            other.transform.DOShakeRotation(5, Vector3.forward * 45, 10, 90, false).SetLoops(1, LoopType.Yoyo).OnComplete(delegate
            {
                other.transform.rotation = Quaternion.identity;
            });

            if (fishCount == strenght)
            {
                StopFishing();
            }
        }
    }
}
