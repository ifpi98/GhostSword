using UnityEngine;
using System.Collections;

public class COrderInLayer : MonoBehaviour {

    SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        OrderZIndex();
    }

    public void OrderZIndex()
    {
        float y = transform.position.y * 100f;

        y *= -1f;

        _spriteRenderer.sortingOrder = (int)y;
    }

}

