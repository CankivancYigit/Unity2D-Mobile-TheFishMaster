using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fish : MonoBehaviour
{
    FishType type;
    CircleCollider2D myCollider;
    SpriteRenderer mySpriteRenderer;
    float xBoundry;
    Tweener tweener;

    public FishType Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
            myCollider.radius = type.colliderRadius;
            mySpriteRenderer.sprite = type.sprite;
        }
    }

    void Awake()
    {
        myCollider = GetComponent<CircleCollider2D>();
        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        xBoundry = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
    }

    
    public void PlaceFish()
    {
        if (tweener != null)
        {
            tweener.Kill(false);
        }

        float depth = Random.Range(type.minDepth, type.maxDepth);
        myCollider.enabled = true;

        Vector3 position = transform.position;
        position.y = depth;
        position.x = xBoundry;
        transform.position = position;

        float yPosWhileMoving = Random.Range(depth - 1, depth + 1);
        Vector2 movingValue = new Vector2(-position.x, yPosWhileMoving);

        float movingDuration = 3f;
        float delay = Random.Range(0, 2 * movingDuration);
        tweener = transform.DOMove(movingValue, movingDuration, false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).SetDelay(delay).OnStepComplete(delegate
         {
             Vector3 localScale = transform.localScale;
             localScale.x = -localScale.x;
             transform.localScale = localScale;
         });

    }

    public void Hooked()
    {
        myCollider.enabled = false;
        tweener.Kill(false);
    }

    [System.Serializable]
    public class FishType
    {
        public int price;
        public int fishAmount;
        public float minDepth;
        public float maxDepth;
        public Sprite sprite;
        public float colliderRadius;
    }
}
