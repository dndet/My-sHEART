using UnityEngine;

public abstract class CharacterHealth : MonoBehaviour
{
    protected int hp;
    protected virtual int maxHP { get; set; }
    [SerializeField] protected Transform heathBar, shielBar;
    protected int shiel, maxShiel;

    protected bool isImmortality;

    public abstract void Dead();

    public void takeDamage(int n)
    {
        if (!isImmortality)
        {
            if (shiel >= (int)Mathf.Ceil(n / 2.0f)) shiel -= (int) Mathf.Ceil(n/2.0f);
            else
            {
                n -= shiel * 2;
                shiel = 0;
                hp = Mathf.Clamp(hp - n, 0, maxHP);
                if (heathBar != null) heathBar.localScale = new Vector3((hp / (float)maxHP), 1, 1);
            }
            if(shielBar != null) shielBar.localScale = new Vector3((shiel / (float)maxShiel), 1, 1);
            if (hp <= 0)
            {
                Dead();
            }
        }
    }
    protected void doStart(int hpl, int shl = 0)
    {
        maxShiel = shl == 0 ? 1 : shl;
        shiel = shl;
        maxHP = hpl;
        hp = maxHP;
        if (heathBar != null) heathBar.localScale = new Vector3((hp / (float) maxHP), 1, 1);
        if (shielBar != null) shielBar.localScale = new Vector3((shiel / (float)maxShiel), 1, 1);
    }
}
