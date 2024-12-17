using UnityEngine;

public class fireBalScript : MonoBehaviour
{
    private int _damage = 1;
    private Vector3 direction;
    private float _speed = 2.5f;

    private void Start()
    {
        _damage += userPointManager.UserPoint.fireBallLV;
        _speed += userPointManager.UserPoint.fireBallLV * 0.015f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.tag == "Enermy")
            {
                skeletonsScript sck = collision.gameObject.GetComponent<skeletonsScript>();
                if(sck != null)
                {
                    sck.takeDamage(_damage);
                }
            }
        }
    }

    void Update()
    {
        if (PlayerControl.instance._fireBall != null && PlayerControl.instance._targetPos != null)
        {
            if (Vector3.Distance(PlayerControl.instance._fireBall.transform.position, PlayerControl.instance._targetPos.position) > 0.05f)
            {
                direction = PlayerControl.instance._targetPos.position - PlayerControl.instance._fireBall.transform.position;
                direction.Normalize();
                Vector3 subdirection = PlayerControl.instance._fireBall.transform.position - PlayerControl.instance._targetPos.position;
                float angle = Vector3.Angle(Vector3.up, subdirection);
                angle *= direction.x > 0 ? 1 : -1;
                PlayerControl.instance._fireBall.transform.position += direction * _speed * Time.deltaTime;
                transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3( 0, 0, angle));
            }
            else
            {
                Quaternion rotation = Quaternion.LookRotation(Vector3.back);
                transform.GetChild(0).transform.rotation = rotation;
            }
        }
    }
}
