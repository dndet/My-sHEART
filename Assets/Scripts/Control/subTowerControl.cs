using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class subTowerControl : MonoBehaviour
{
    [SerializeField, Range(0,3)] private int mainIndex = 0;
    [SerializeField] private GameObject subFire;
    [SerializeField] private TextMeshPro showLevel;
    [SerializeField] private ParticleSystem _particle;

    private Color _mainColor;

    private List<skeletonsScript> _enermySelect = new List<skeletonsScript>();
    private List<GameObject> listSubFire = new List<GameObject>();

    private float timeDelay, timeCOunt;
    private uint lvSubTow;


    void Start()
    {
        for(int i=0;i< 10; i++)
        {
            listSubFire.Add(Instantiate(subFire, _particle.transform.position, Quaternion.identity));
            listSubFire[i].SetActive(false);
        }

        lvSubTow = userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].levelSubTower[mainIndex];
        if (showLevel != null) showLevel.text = lvSubTow.ToString();
        timeDelay = 6.5f - lvSubTow * 0.025f;
        timeCOunt = 0;
        _mainColor = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        lvSubTow = userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].levelSubTower[mainIndex];
        _particle.gameObject.SetActive(lvSubTow > 0);
        transform.GetChild(0).gameObject.SetActive(lvSubTow > 0);
        transform.GetChild(1).gameObject.SetActive(lvSubTow <= 0);
        if (timeCOunt <= 0)
        {
            if (lvSubTow > 0 && _enermySelect.Count > 0)
            {
                for (int i = 0; i < listSubFire.Count; i++)
                {
                    if (!listSubFire[i].activeSelf)
                    {
                        listSubFire[i].SetActive(true);
                        listSubFire[i].transform.position = _particle.transform.position;
                        subFireBall temp = listSubFire[i].GetComponent<subFireBall>();
                        temp._target = _enermySelect[0].transform;
                        temp._speed = lvSubTow * 1.75f;
                        temp._damge = 1 + (int)(lvSubTow / 1);
                        try
                        {
                            var main = temp._particle.main;
                            main.startColor = new Color(1, 0, (lvSubTow) / 200.0f, 1);
                        }
                        catch { }
                        break;
                    }
                }
                timeCOunt = timeDelay;
            }
        }
        else
        {
            timeCOunt -= Time.deltaTime;
            var main = _particle.main;
            main.startColor = new Color(_mainColor.r, _mainColor.g, lvSubTow, (1 - (timeCOunt / timeDelay)));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.gameObject.tag == "Enermy")
        {
            _enermySelect.Add(collision.gameObject.GetComponent<skeletonsScript>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.tag == "Enermy")
        {
            _enermySelect.Remove(collision.gameObject.GetComponent<skeletonsScript>());
        }
    }
}
