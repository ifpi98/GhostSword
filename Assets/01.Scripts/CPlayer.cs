using UnityEngine;
using System.Collections;
using UnityEngine.UI;



//GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);  색변경

public class CPlayer : MonoBehaviour
{

    public float _speed; // 속도.
    public bool _isRightDir = true; // 시선.

    public Image hpBar;
    public Image _hit;
    public Image comboBar;
    public Image skillBar;

    public GameObject shoot;
    public Transform shootPos;
    public GameObject _talkMessage;
    public Text _talkMessageText;

    public GameObject _skillOnText;
    public GameObject _skillEvent;
    public GameObject _skillFire;
    public GameObject _DieEffect;

    public static int _playerLv = 1;
    public static float _nowExp = 0f;
    public static float _nowMaxExp = 10;
    public static float _maxHp = 100;
    public static float _hp = 100;
    public static float _maxMp = 100;
    public static float _mp = 0;
    public static float _pointCount = 0;
    public static bool _skillOn = false;
    public static float _playerBalance = 1;

    Animator _animator; // 애니메이터 컴포넌트.
    Rigidbody2D _rigidbody2d; // 강체 컴포넌트.
    SpriteRenderer _spriteRender; // 스프라이트 컴포넌트.
    COrderInLayer _orderInLayer; // z 정렬 컴포넌트.

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _spriteRender = GetComponent<SpriteRenderer>();
        _orderInLayer = GetComponent<COrderInLayer>();

    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(ComboReset());

    }

    IEnumerator ComboReset()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (CGameManager._comboCount >= 1)
            {
                CGameManager._ComboResetCount -= 0.2f;
            }

            if (CGameManager._ComboResetCount <= 0)
            {
                CGameManager._comboCount = 0;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (_skillOn != true)
        {
            Move();
            Attack();
            Die();

            comboBar.fillAmount = CGameManager._ComboResetCount / 3.5f;
            skillBar.fillAmount = ((_mp / _maxMp));
            hpBar.fillAmount = _hp / _maxHp;
        }
    }

    public void TalkMessage(int what)
    {
        _talkMessage.SetActive(true);
        if (what == 1)
        {
            _talkMessageText.text = "Level Up!!";
        }
        if (what == 2)
        {
            _talkMessageText.text = "HP Up!!";
        }
        if (what == 3)
        {
            _talkMessageText.text = "MP Up!!";
        }
        if (what == 4)
        {
            _talkMessageText.text = "Point Up!!";
        }
    }

    public void LvUp(float exp)
    {
        _nowExp += exp;

        if (_playerLv == 1)
        {
            if (_nowExp >= _nowMaxExp)
            {
                _playerLv += 1;
                _nowMaxExp = 20;
                TalkMessage(1);
                _playerBalance += 0.5f;
            }
        }
        if (_playerLv == 2)
        {
            if (_nowExp >= _nowMaxExp)
            {
                _playerLv += 1;
                _nowMaxExp = 40;
                _hp = _maxHp;
                TalkMessage(1);
                _playerBalance += 0.5f;
            }
        }
        if (_playerLv == 3)
        {
            if (_nowExp >= _nowMaxExp)
            {
                _playerLv += 1;
                _nowMaxExp = 80;
                _hp = _maxHp;
                TalkMessage(1);
                _playerBalance += 0.5f;
            }
        }
        if (_playerLv == 4)
        {
            if (_nowExp >= _nowMaxExp)
            {
                _playerLv += 1;
                _nowMaxExp = 160;
                _hp = _maxHp;
                TalkMessage(1);
                _playerBalance += 0.5f;
            }
        }
        if (_playerLv == 5)
        {
            if (_nowExp >= _nowMaxExp)
            {
                _playerLv += 1;
                _nowMaxExp = 320;
                _hp = _maxHp;
                TalkMessage(1);
                _playerBalance += 0.5f;
            }
        }
        if (_playerLv == 6)
        {
            if (_nowExp >= _nowMaxExp)
            {
                _playerLv += 1;
                _nowMaxExp = 640;
                _hp = _maxHp;
                TalkMessage(1);
                _playerBalance += 0.5f;
            }
        }
        if (_playerLv == 7)
        {
            if (_nowExp >= _nowMaxExp)
            {
                _playerLv += 1;
                _nowMaxExp = 1280;
                _hp = _maxHp;
                TalkMessage(1);
                _playerBalance += 0.5f;
            }
        }
        if (_playerLv >= 8)
        {
            if (_nowExp >= _nowMaxExp)
            {
                return;
            }
        }
    }

    // 수정 필요 연출을 위해 Del을 별도 설정할 것
    void Die()
    {
        if (_hp <= 0)
        {

            _DieEffect.SetActive(true);
            
        }
    }



    void Move()
    {
        // 수평, 수직 키 입력 받기.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 pos = transform.position;

        if (h == 0 && v == 0)
        {
            // 정지 애니메이션 재생.
            _animator.SetBool("Walk", false);
        }
        else
        {
            // 이동 애니메이션 재생.
            _animator.SetBool("Walk", true);

            // 수평 이동 체크
            if (h != 0)
            {
                transform.Translate(Vector2.right * h * _speed * Time.deltaTime);

                if (pos.x > 7.7f)
                {
                    pos.x = 7.7f;
                    transform.position = pos;
                }
                if (pos.x < -7.5f)
                {
                    pos.x = -7.5f;
                    transform.position = pos;
                }
            }
            if ((h > 0 && !_isRightDir) || (h < 0 && _isRightDir))
            {
                // 방향전환.
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
                // 시선 전환
                _isRightDir = !_isRightDir;
            }

            if (v != 0)
            {
                transform.Translate(Vector2.up * v * (_speed / 1.5f) * Time.deltaTime);

                if (pos.y > -2.5f)
                {
                    pos.y = -2.5f;
                    transform.position = pos;

                }
                if (pos.y < -4.8f)
                {
                    pos.y = -4.8f;
                    transform.position = pos;

                }
            }
        }
    }


    void Attack()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            _animator.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Skill();

        }
    }

    void MakeMissile()
    {
        Instantiate(shoot, shootPos.position, Quaternion.identity);
    }

    public void Skill()
    {
        if (_mp >= _maxMp)
        {
            _skillOnText.SetActive(true);
            _animator.SetBool("Skill",true);
            _animator.SetBool("Walk", false);
            
            
            _skillOn = true;
            _mp = 0;
        }
    }

    public void SkillEnd()
    {
        _animator.SetBool("Skill", false);
        _skillOn = false;

    }

    void SkillEventOn()
    {
        _skillEvent.SetActive(true);
    }

    void DamagedEnd()
    {
        _animator.SetBool("Damaged", false);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (_skillOn != true)
        {
            if (collider.tag == "SHOOTE")
            {
                CShootE hitDamage = collider.GetComponent<CShootE>();
                iTween.ShakePosition(Camera.main.gameObject, iTween.Hash("x", 0.2f, "y", 0.2f, "time", 1f));

                _hp -= (hitDamage._damage * CGameManager._balance);
                hpBar.fillAmount = _hp / _maxHp;
                _hit.gameObject.SetActive(true);
                _animator.SetBool("Damaged", true);

                if (collider.GetComponentInParent<Transform>().position.x > gameObject.transform.position.x)
                {
                    transform.Translate(-Vector2.right * 1f);
                }
                else
                {
                    transform.Translate(Vector2.right * 1f);
                }
            }

            if (collider.tag == "HPITEM")
            {
                TalkMessage(2);
                _hp += (collider.GetComponent<CItem>()._hpValue * _playerBalance);
            }

            if (collider.tag == "MPITEM")
            {
                TalkMessage(3);
                _mp += collider.GetComponent<CItem>()._mpValue;
            }

            if (collider.tag == "POINTITEM")
            {
                TalkMessage(4);
                _pointCount += collider.GetComponent<CItem>()._pointValue;
            }
        }
    }

}
