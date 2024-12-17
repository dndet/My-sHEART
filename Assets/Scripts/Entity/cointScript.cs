using UnityEngine;

public class cointScript : MonoBehaviour
{
    [SerializeField] private int coint = 2;
    private float _timeDelay = 0.5f;

    void Update()
    {
        if(_timeDelay > 0)
        {
            _timeDelay -= Time.deltaTime;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, PlayerControl.instance._heartTrans.position, 3.5f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.tag == "Heart")
            {
                PlayerControl.instance.takeCoint(coint);
                Destroy(gameObject);
            }
        }
    }
}
