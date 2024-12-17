using UnityEngine;

public class meleScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _healBar;
    private SpriteRenderer _sprite;
    private BoxCollider2D _boxCollider;
    private skeletonsScript _skeleton;
    private float _timeHiden, _timeCount;
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        StartHiden();
    }

    void StartHiden()
    {
        _skeleton = GetComponent<skeletonsScript>();
        _timeHiden = 5.5f + userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].enermyLevel * 0.2f;
        _timeCount = 0;
    }

    private void Update()
    {
        if (_timeCount < _timeHiden)
        {
            _timeCount += Time.deltaTime;
            _boxCollider.enabled = false;
            _sprite.color = new Color32(255, 255, 255, (byte) ((_timeCount / _timeHiden) * 255));
            _healBar.color = new Color32(255, 255, 255, (byte) ((_timeCount / _timeHiden) * 255));
            _skeleton.speedRate = 2 - (_timeCount / _timeHiden);

        }
        else
        {
            _boxCollider.enabled = true;
            _sprite.color = new Color32(255, 255, 255, 255);
            _healBar.color = new Color32(255, 255, 255, 255);
            _skeleton.speedRate = 1;
        }
    }
}
