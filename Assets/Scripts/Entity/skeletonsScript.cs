using TMPro;
using UnityEngine;

public class skeletonsScript : CharacterHealth
{
    private AudioSource _audioSource;
    private TextMeshPro _textLevel;
    private Animator _ani;
    private float _speed = 0.75f;
    [HideInInspector] public float speedRate;
    private Vector3 _target;
    private bool _isDead;
    public bool isDie
    {
        get
        {
            return _isDead;
        }
    }
    private int _dmg = 1;
    private float timeStop, timeCount;

    public override void Dead()
    {
        PlayerControl.instance.enermySpawning = Mathf.Clamp(PlayerControl.instance.enermySpawning -1, 0, PlayerControl.instance.enermySpawning);
        userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].timeSpawn += 1;
        _isDead = true;
        transform.GetChild(0).GetComponent<AudioSource>().Play();
        Invoke("inActive", 2.0f);
    }

    void Start()
    {
        speedRate = 1;
        _speed = 0.75f + userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].enermyLevel * 0.02f;
        _speed *= speedRate;
        _ani = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _target = PlayerControl.instance._heartTrans.transform.position + Vector3.down;
        _isDead = false;

        doStart(1 + (userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].enermyLevel));
        timeCount = timeStop;
        try
        {
            _textLevel = GetComponentInChildren<TextMeshPro>();
            if(_textLevel != null) _textLevel.text = (userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].enermyLevel).ToString();
        }
        catch { }
    }

    void Update()
    {
        if(_isDead)
        {
            _ani.SetTrigger("Dead");
        }
        else if(timeCount > 0)
        {
            timeCount -= Time.deltaTime;
            _ani.SetTrigger("Pause");
        }
        else if(_target != null)
        {
            Vector3 dir = _target - transform.position;
            dir.Normalize();
            _ani.SetFloat("Horizontal", dir.x);
            _ani.SetFloat("Vertical", dir.y);
            if ( Vector3.Distance(transform.position, _target) > 1f)
            {
                transform.position += dir * (_speed * Time.deltaTime);// Vector3.Lerp(transform.position, _target, _speed * Time.deltaTime);
                _ani.SetTrigger("Run");
            }
            else
            {
                _ani.SetTrigger("Attack");
            }
        }
    }

    public void beStop(float t, int damage)
    {
        timeStop = t / userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].enermyLevel;
        timeCount = timeStop;
        if (userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].enermyLevel < 10)
        {
            this.takeDamage(damage);
        }
        else
        {
        }
    }

    public void resetSate()
    {
        doStart(1 + (userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].enermyLevel));
        _speed = 0.75f + userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].enermyLevel * 0.02f;
        _speed *= speedRate;
        hp = maxHP;
        _isDead = false;
        if (heathBar != null) heathBar.localScale = new Vector3((hp / (float) maxHP), 1, 1);
        if (_textLevel != null) _textLevel.text = (userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].enermyLevel).ToString();
        gameObject.SetActive(true);
    }

    private void inActive()
    {
        Instantiate(PlayerControl.instance.cointPrefab, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    public void attack()
    {
        if (!_isDead)
        {
            PlayerControl.instance._heart.takeDamage(_dmg);
            if(_audioSource != null) _audioSource.Play();
        }
    }
}
