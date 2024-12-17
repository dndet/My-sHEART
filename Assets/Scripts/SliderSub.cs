using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderSub : Slider
{
    [SerializeField] private TextMeshProUGUI _textShow;
    [SerializeField] private float _fillId = 100;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] string _subText;
    public float fillIndex
    {
        get
        {
            return _fillId;
        }
        set
        {
            if (_fillId != value)
            {
                _fillId = value;
            }
        }
    }

    public AudioSource audioPlay
    {
        get
        {
            return _audioSource;
        }
        set
        {
            this._audioSource = value;
        }
    }

    public string subText
    {
        get
        {
            return _subText;
        }
        set
        {
            if (_subText != value)
            {
                _subText = value;
            }
        }
    }

    public TextMeshProUGUI textShow
    {
        get
        {
            return _textShow;
        }
        set
        {
            if (_textShow != value)
            {
                this._textShow = value;
            }
        }
    }

    protected override void Start()
    {
        base.Start();
        if (_textShow != null) _textShow.text = ((int)(this.value * _fillId)).ToString() + " " + subText;
        onValueChanged.AddListener(delegate
        {
            if(_textShow != null) _textShow.text = ((int)(this.value * _fillId)).ToString() + " " + subText;
            if(_audioSource != null) _audioSource.Play();
        });
    }
}
