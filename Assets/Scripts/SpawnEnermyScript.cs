using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SpawnEnermyScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enermyPrefabs = new List<GameObject>();
    private int _enermyCount = 1;
    private List<GameObject> _enermy = new List<GameObject>();
    private const int MINENERMY = 1;
    private bool isCanSpawn;

    private void Start()
    {
        isCanSpawn = true;
        _enermyCount = MINENERMY + (int)(userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].enermyLevel / 5);

        for (int i = 0; i < _enermyCount; i++)
        {
            _enermy.Add(Instantiate(
                _enermyPrefabs[i % _enermyPrefabs.Count],
                transform.position + transform.right * Random.Range(0.0f, 5.0f),
                Quaternion.identity));
            _enermy[i].GetComponent<AudioSource>().volume = settingManager.settingJson.soundSF;
            _enermy[i].transform.GetChild(0).GetComponent<AudioSource>().volume = settingManager.settingJson.soundSF;
            if((userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].timeSpawn + PlayerControl.instance.enermySpawning) < PlayerControl.instance.upLevelEner)
            {
                _enermy[i].SetActive(true);
                PlayerControl.instance.enermySpawning += 1;
            }
            else
            {
                _enermy[i].SetActive(false);
            }
        }
        InvokeRepeating("respawn", 1f, 3f);
    }

    private void respawn()
    {
        if (isCanSpawn)
        {
            foreach (GameObject obj in _enermy)
            {
                if (!obj.activeSelf && this.checkSpawn())
                {
                    obj.transform.position = transform.position + transform.right * Random.Range(0.0f, 5.0f);
                    obj.GetComponent<skeletonsScript>().resetSate();
                    PlayerControl.instance.enermySpawning += 1;
                    obj.GetComponent<AudioSource>().volume = settingManager.settingJson.soundSF;
                    obj.transform.GetChild(0).GetComponent<AudioSource>().volume = settingManager.settingJson.soundSF;
                }
            }

            {
                if (userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].timeSpawn >= PlayerControl.instance.upLevelEner)
                {
                    _enermyCount = MINENERMY + (int)(userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].enermyLevel / 5);
                    if (_enermyCount > _enermy.Count)
                    {
                        for (int i = 0; i < (_enermyCount - _enermy.Count); i++)
                        {
                            _enermy.Add(Instantiate(
                                _enermyPrefabs[(_enermy.Count + i) % _enermyPrefabs.Count],
                                transform.position + transform.right * Random.Range(0.0f, 5.0f),
                                Quaternion.identity));
                            _enermy[_enermy.Count + i].GetComponent<AudioSource>().volume = settingManager.settingJson.soundSF;
                            _enermy[_enermy.Count + i].transform.GetChild(0).GetComponent<AudioSource>().volume = settingManager.settingJson.soundSF;
                        }
                    }
                    PlayerControl.instance.takeReward();
                }
            }
        }
    }

    public void StopPawn()
    {
        isCanSpawn = false;
        foreach(GameObject obj in _enermy)
        {
            obj.SetActive(false); //GetComponent<skeletonsScript>().takeDamage(int.MaxValue);
        }
    }

    public void Spawning()
    {
        isCanSpawn = true;
    }

    public bool checkSpawn()
    {
        return 
            (userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].timeSpawn + PlayerControl.instance.enermySpawning) < PlayerControl.instance.upLevelEner;
    }
}
