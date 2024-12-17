using UnityEngine;

public class fireZoneScript : MonoBehaviour
{

    [SerializeField] int _damage = 2;
    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Enermy")
            {
                skeletonsScript sck = collision.gameObject.GetComponent<skeletonsScript>();
                if (sck != null)
                {
                    sck.takeDamage(_damage);
                }
            }
        }
    }

    public void OnCollider()
    {
        if(_collider != null) _collider.enabled = true;
    }

    public void OffCollider()
    {
        if (_collider != null) _collider.enabled = false;
    }

    public void inActive()
    {
        gameObject.SetActive(false);
    }
}
