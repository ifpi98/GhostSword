using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CGameManager : MonoBehaviour
{

    public GameObject _enemyType;
    public float _makerTimeMin;
    public float _makerTimeMax;
    public static int _enemyCount = 0;
    public static int _killCount = 0;
    public static int _comboCount = 0;
    public static int _maxComboCount = 0;
    public static float _ComboResetCount = 3.5f;
    public static bool _comboSucess = false;
    public static float _balance = 1f;

    private string _hitGood;

    public Text _comboText;
    public Text _hitLevelText;
    public Text _hitLevelText2;
    public Text _maxComboText;
    public Text _killCountText;
    public Text _playerLvText;
    public Text _playerExpText;
    public Text _playerHpText;
    public Text _pointText;
    public Text _playerMpText;


    // Use this for initialization
    void Start()
    {

        StartCoroutine(EnemyMaker());
        StartCoroutine(Balance());


    }

    // Update is called once per frame
    void Update()
    {

        _comboText.text = CGameManager._comboCount.ToString();
        HitGood();
        _hitLevelText.text = _hitGood;
        _hitLevelText2.text = _hitGood;
        _killCountText.text = CGameManager._killCount.ToString();
        _playerLvText.text = CPlayer._playerLv.ToString();
        _playerHpText.text = ((CPlayer._hp / CPlayer._maxHp) * 100).ToString("###.#");
        _pointText.text = CPlayer._pointCount.ToString();
        //float mptext = ((CPlayer._mp / CPlayer._maxMp) * 100);
        //_playerMpText.text = mptext.ToString("###.#");



        float _viewExp;
        if (CPlayer._nowExp != 0)
        {
            _viewExp = ((CPlayer._nowExp / CPlayer._nowMaxExp) * 100);
            _playerExpText.text = _viewExp.ToString("##");
        }
        else
        {
            _viewExp = 0;
            _playerExpText.text = _viewExp.ToString();
        }

        float _viewMp;
        if (CPlayer._mp != 0)
        {
            _viewMp = ((CPlayer._mp / CPlayer._maxMp) * 100);
            _playerMpText.text = _viewMp.ToString("##");
        }
        else
        {
            _viewMp = 0;
            _playerMpText.text = _viewMp.ToString();
        }

        if(CPlayer._mp > CPlayer._maxMp)
        {
            CPlayer._mp = CPlayer._maxMp;
        }

        if (CPlayer._hp > CPlayer._maxHp)
        {
            CPlayer._hp = CPlayer._maxHp;
        }
        else if (CPlayer._hp <= 0)
        {
            CPlayer._hp = 0;
        }

        if (_comboCount > _maxComboCount)
        {
            _maxComboCount = _comboCount;
            _maxComboText.text = CGameManager._maxComboCount.ToString();
        }


    }

    void HitGood()
    {
        if (_comboCount >= 0)
        {
            _hitGood = "Combo";
        }
        if (_comboCount > 10)
        {
            _hitGood = "Good~";
        }
        if (_comboCount > 30)
        {
            _hitGood = "Very Good~";
        }
        if (_comboCount > 60)
        {
            _hitGood = "Excellent!";
        }
        if (_comboCount > 120)
        {
            _hitGood = "Great~!";
        }
        if (_comboCount > 240)
        {
            _hitGood = "Amazing~!!";
        }
        if (_comboCount > 480)
        {
            _hitGood = "Fantastic~!!!";
        }
    }


    IEnumerator EnemyMaker()
    {

        while (true)
        {

            float _makeTime = Random.Range(_makerTimeMin, _makerTimeMax);
            yield return new WaitForSeconds(_makeTime);


            if (_enemyCount <= 6)
            {
                Instantiate(_enemyType, transform.position, Quaternion.identity);
                _enemyCount += 1;

            }
        }

    }

    IEnumerator Balance()
    {
        int i = 20;

        while (true)
        {
            yield return new WaitForSeconds(30f);

            if (i >= 1)
            {
                _balance += 0.2f;
                i -= 1;
            }
            else
            {
                _balance = 5f;
            }

        }

    }

}
