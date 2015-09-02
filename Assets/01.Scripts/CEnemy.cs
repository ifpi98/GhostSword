using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CEnemy : MonoBehaviour
{



    public float _speed;
    public int _attackRate;
    public float _expValue = 4;
    public float _mpValue = 8;
    bool dirRight = true;
    int dir = 1;
    bool boolAttackIng = false;
    public float _hp;
    public bool _enemyDieCheck = false;
    public GameObject[] _dropItem;



    public CUIDissable _comboImage;
    public GameObject _enemyHit;
    public GameObject _player;
    public GameObject _shoot;

    public float _minAttack;
    public float _maxAttack;
    private bool _skilldie = false;

    Animator _animator;
    SpriteRenderer _spriteRender; // 스프라이트 컴포넌트.
    COrderInLayer _orderInLayer; // z 정렬 컴포넌트.
    Rigidbody2D _rigidbody2d; // 강체 컴포넌트.


    // Use this for initialization

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _orderInLayer = GetComponent<COrderInLayer>();
        _rigidbody2d = GetComponent<Rigidbody2D>();

    }

    void Start()
    {
        Vector2 pos = transform.position;
        if (pos.x < 10f)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        _player = GameObject.Find("Player");


        StartCoroutine(MoveHor());
        StartCoroutine(Attack());


    }

    // Update is called once per frame
    void Update()
    {

        if (_enemyDieCheck != true)
        {
            if (CPlayer._skillOn != true)
            {
                Move();
                Dir();
                Die();
            }
        }

    }


    IEnumerator Attack()
    {
        while (true)
        {
            float _attTime = Random.Range(_minAttack, _maxAttack);
            yield return new WaitForSeconds(_attTime);
            AttackAction();
        }
    }



    void AttackAction()
    {
        if (_enemyDieCheck != true)
        {
            if (CPlayer._skillOn != true)
            {
                boolAttackIng = true;                
                _animator.SetTrigger("EnemyAttack");
                _animator.SetBool("EnemyWalk", false);
            }
        }
    }



    void ShootMake()
    {
        if (_enemyDieCheck != true)
        {
            if (CPlayer._skillOn != true)
            {
                _shoot.SetActive(false);
                _shoot.SetActive(true);
                _shoot.GetComponentInChildren<CShootE>()._crashEff.SetActive(false);
            }
        }
    }

    void AttackEnd()
    {
        if (_enemyDieCheck != true)
        {
            boolAttackIng = false;
            _shoot.SetActive(false);
        }
    }

    void Move()
    {
        if (!boolAttackIng)
        {

            _animator.SetBool("EnemyWalk", true);
            Vector2 pos = transform.position;
            int way = Random.Range(1, 100);
            int past = Random.Range(1, 10);
            

            if (way < 80)
            {

                if (past < 3)
                {
                    transform.Translate(Vector2.right * dir * ((_speed * 3f) * CGameManager._balance) * Time.deltaTime);
                    
                }
                else
                {
                    transform.Translate(Vector2.right * dir * _speed * CGameManager._balance * Time.deltaTime);
                    
                }

                if (pos.x > 7.5f)
                {
                    pos.x = 7.5f;
                    transform.position = pos;
                    dir = -1;
                    
                }
                if (pos.x < -7.2f)
                {
                    pos.x = -7.2f;
                    transform.position = pos;
                    dir = 1;
                    
                }
            }
            else
            {
                if (past < 3)
                {
                    transform.Translate(Vector2.up * dir * (_speed * 3f) * Time.deltaTime);
                    
                }
                else
                {
                    transform.Translate(Vector2.up * dir * _speed * Time.deltaTime);
                    
                }

                if (pos.y > -2.5f)
                {
                    pos.y = -2.5f;
                    transform.position = pos;
                    dir = -1;
                    

                }
                if (pos.y < -4.8f)
                {
                    pos.y = -4.8f;
                    transform.position = pos;
                    dir = 1;
                    
                }
            }
        }
    }

    IEnumerator MoveHor()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            
            if (transform.position.x < _player.transform.position.x)
            {

                dir = 1;
            }
            else
            {
                dir = -1;
            }
        }

    }

    void Dir()
    {
        //_shoot.SetActive(false);
        if (transform.position.x < _player.transform.position.x && dirRight == true)
        {
            // 방향전환.
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            dirRight = false;
        }
        else if (transform.position.x > _player.transform.position.x && dirRight == false)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            dirRight = true;
        }

    }



    void Die()
    {
        if (_hp <= 0 && _enemyDieCheck == false)
        {
            _enemyDieCheck = true;
            _shoot.SetActive(false);
            Destroy(_shoot.gameObject);
            _player.GetComponent<CPlayer>().LvUp(_expValue * CGameManager._balance);
            gameObject.GetComponent<Collider2D>().enabled = false;
            ItemDrop();

            // 중력부여.
            _rigidbody2d.gravityScale = 2.5f;

            // Rigidbody2d.AddForce(방향 * 힘)
            _rigidbody2d.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            _animator.SetTrigger("EnemyDie");
        }
    }

    void DelMe()
    {
        CGameManager._enemyCount -= 1;
        CGameManager._killCount += 1;
        if (_skilldie != true)
        {
            CPlayer._mp += _mpValue;
        }
        Destroy(gameObject);
    }

    void ItemDrop()
    {
        int itemSelect = Random.Range(1, 100);
        if (itemSelect < 5)
        {
            Instantiate(_dropItem[0], transform.position, Quaternion.identity);
        }
        else if (itemSelect >= 5 && itemSelect < 20)
        {
            Instantiate(_dropItem[1], transform.position, Quaternion.identity);
        }
        else if (itemSelect >= 20 && itemSelect < 35)
        {
            Instantiate(_dropItem[2], transform.position, Quaternion.identity);
        }
        else
        {
            return;
        }
    }

    void DamagedEnd()
    {
        _animator.SetBool("EnemyDamaged", false);
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (CPlayer._skillOn != true)
        {
            if (collider.tag == "SHOOT")
            {
                _shoot.SetActive(false);
                _skilldie = false;
                _enemyHit.SetActive(true);
                CShoot hitDamage = collider.GetComponent<CShoot>();
                _hp += (10 * CGameManager._balance); // 게임매니저 난이도 밸런스 적용.
                _hp -= (hitDamage._damage * CPlayer._playerBalance); // 플레이어 레벨업 밸런스 적용.
                CGameManager._comboCount += 1;
                CGameManager._comboSucess = true;
                _comboImage = GameObject.Find("Combo").transform.FindChild("ComboImage").GetComponent<CUIDissable>();
                _comboImage.SetActiveMe();
                CGameManager._ComboResetCount = 3.5f;
                _animator.SetBool("EnemyDamaged", true);

                if (collider.transform.position.x > gameObject.transform.position.x)
                {
                    transform.Translate(-Vector2.right * 0.4f);
                }
                else
                {
                    transform.Translate(Vector2.right * 0.4f);
                }
            }

            if (collider.tag == "SKILLFIRE")
            {
                _shoot.SetActive(false);
                _skilldie = true;
                _enemyHit.SetActive(true);
                CSkillFire hitSkillDamage = collider.GetComponent<CSkillFire>();
                _hp *= CGameManager._balance; // 게임매니저 난이도 밸런스 적용.
                _hp -= (hitSkillDamage._damage * CPlayer._playerBalance); // 플레이어 레벨업 밸런스 적용.

                if (collider.transform.position.x > gameObject.transform.position.x)
                {
                    transform.Translate(-Vector2.right * 1.2f);
                }
                else
                {
                    transform.Translate(Vector2.right * 1.2f);
                }
            }
        }
    }
}







