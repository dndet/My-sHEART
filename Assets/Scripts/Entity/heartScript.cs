using System.Threading.Tasks;
using UnityEngine;

public class heartScript : CharacterHealth
{
    //private int maxHP = 12;
    private float timeImmortality, timeCount;
    [SerializeField] private SpriteRenderer shieldLV;
    [SerializeField] private ParticleSystem _heartParticle;
    private Animator _ani;

    public override async void Dead()
    {
        PlayerControl.instance.isDie = true;
        if (UIPlayManager.instance.mainCameraAnimation != null)
        {
            UIPlayManager.instance.mainCameraAnimation.SetTrigger("isEnd");
        }
        await Task.Delay(1000);
        if(_ani != null) _ani.SetTrigger("broken");
        //PlayerControl.instance.clearZone.SetActive(true);
        await Task.Delay(1000);
        Time.timeScale = 0.5f;
        await Task.Delay(500);
        Time.timeScale = 0.0f;
        UIPlayManager.instance.DeadCtr();
        UIPlayManager.instance.uiDead.SetActive(true);
    }

    void Start()
    {
        doStart(32 + userPointManager.UserPoint.heartLV * 3, userPointManager.UserPoint.shieldLV * 2);
        isImmortality = false;
        _ani = GetComponent<Animator>();
    }

    private void Update()
    {
        if (timeCount > 0)
        {
            timeCount -= Time.deltaTime;
            if(shieldLV != null)
            {
                shieldLV.color = new Color32(255, 255, 255, (byte) ((timeCount / timeImmortality) * 255));
            }
        }
        else
        {
            isImmortality = false;
            if (shieldLV != null)
            {
                shieldLV.color = new Color32(255, 255, 255, 0);
            }
        }

        int nMaxHP = 32 + userPointManager.UserPoint.heartLV * 3;
        if (nMaxHP != maxHP && nMaxHP != 0) maxHP = nMaxHP;
        int nMaxSP = userPointManager.UserPoint.shieldLV * 2;
        if (nMaxSP != maxShiel && nMaxSP != 0) maxShiel = nMaxSP;
    }

    public void Recover(int hp)
    {
        if(hp > 0)
        {
            hp = Mathf.Clamp(this.hp + hp, 0, maxHP);
            if (_heartParticle != null) _heartParticle.Play();
            if (heathBar != null) heathBar.localScale = new Vector3((hp / (float)maxHP), 1, 1);
        }
    }

    public void Recover(float pc)
    {
        if (hp > 0)
        {
            hp = Mathf.Clamp(this.hp + (int) (maxHP * pc), 0, maxHP);
            if (_heartParticle != null) _heartParticle.Play();
            if (heathBar != null) heathBar.localScale = new Vector3((hp / (float)maxHP), 1, 1);
        }
    }

    public void upShiel(int n)
    {
        if (n > 0)
        {
            shiel = Mathf.Clamp(this.shiel + n, 0, maxShiel);
            if (shielBar != null) shielBar.localScale = new Vector3((shiel / (float)maxShiel), 1, 1);
        }
    }

    public void setImmortality()
    {
        isImmortality = true;
        timeImmortality = userPointManager.UserPoint.skillLevel[1] * 0.52f + 1;
        timeCount = timeImmortality;
    }
}
