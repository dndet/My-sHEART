using UnityEngine;

public class TowerScript : MonoBehaviour
{
    void OnMouseDown()
    {
        OnClick();
    }

    public void OnClick()
    {
        if (!PlayerControl.instance.isDie)
            PlayerControl.instance._targetPos = transform.GetChild(0);
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Collider2D collider = Physics2D.OverlapPoint(touchPosition);
                    if (collider != null && collider.gameObject == gameObject)
                    {
                        OnClick();
                    }
                    break;
            }
        }
    }
}
