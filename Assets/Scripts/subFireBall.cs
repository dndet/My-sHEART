using UnityEngine;

public class subFireBall : MonoBehaviour
{
    [HideInInspector] public float _speed;
    [HideInInspector] public int _damge;
    [HideInInspector] public Transform _target;
    [HideInInspector] public ParticleSystem _particle;

    void Start()
    {
        _particle = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            if (_target.GetComponent<skeletonsScript>().isDie)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Vector3 dir = (_target.position - transform.position).normalized;
                transform.position += dir * (_speed * Time.deltaTime); //Vector3.Lerp(transform.position, _target.position, _speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, _target.position) < 0.1f && _target.gameObject.tag == "Enermy")
                {
                    _target.GetComponent<skeletonsScript>().takeDamage(_damge);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
