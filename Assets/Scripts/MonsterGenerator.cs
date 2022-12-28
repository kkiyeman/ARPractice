using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MonsterGenerator : MonoBehaviour
{
    ARRaycastManager arRaycast;

    public List<GameObject> monsterList = new List<GameObject>();
    public GameObject monsterPref;
    public Transform SpawnPosition;
    public int spawnRate = 100;
    public float delay = 2f;
    GameObject curMonster;

    private void Awake()
    {
        arRaycast = GetComponent<ARRaycastManager>();
        
    }

    private void Update()
    {

        if(Input.touchCount>0)
        {
            Touch touch = Input.GetTouch(0);
            List<ARRaycastHit> touchhits = new List<ARRaycastHit>();
           if(arRaycast.Raycast(touch.position, touchhits, TrackableType.Planes))
            {
                Pose hitposition = touchhits[0].pose;
                monsterPref.transform.SetPositionAndRotation(hitposition.position, hitposition.rotation);
                monsterPref.SetActive(true);
            }
            else
            {
                monsterPref.SetActive(false);
            }


        }

        var screenPoint = Camera.current.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if(arRaycast.Raycast(screenPoint, hits, TrackableType.Planes))
        {
            if (curMonster != null)
                return;
            if (IsInvoking())
                return;

            Invoke("CheckTime", delay);       
        }
        else
        {
            CancelInvoke("CheckTime");

            if (curMonster != null)
            {
                Destroy(curMonster);
                curMonster = null;
            }
        }
        
    }

    void CheckTime()
    {
        int rate = Random.Range(0, 100);
        if (rate < spawnRate)
            SpawnMonster();
        else
            Invoke("CheckTime", delay);
    }

    void SpawnMonster()
    {
        int monsterIdx = Random.Range(0, monsterList.Count);
        curMonster = Instantiate(monsterList[monsterIdx], SpawnPosition);
        curMonster.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
    }

}
