using UnityEngine;
using System.Collections;

public class CShoot : MonoBehaviour
{

    public GameObject playerDir;
    public float _speed;
    public int myDir;
    public float _damage = 15;


    // Use this for initialization
    void Start()
    {

        playerDir = GameObject.Find("Player");

        bool mmdir = playerDir.GetComponent<CPlayer>()._isRightDir;

        if (mmdir == true)
        {
            myDir = 1;
        }
        else
        {
            myDir = -1;
        }

        if (mmdir != true)
        {
            // 방향전환.
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

        }

    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector2.right * myDir * _speed * Time.deltaTime);

        Die();

    }

    void Die()
    {
        Vector2 pos = transform.position;

        if (pos.x < -10f || pos.x > 10f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag == "ENEMY" || collider.tag == "SHOOTE")
        {
            Destroy(gameObject);
        }
    }
}
