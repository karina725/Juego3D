using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    private Vector2 lastTapPosition;
    private Vector3 startRotation;

    public Transform topTransform;
    public Transform goalTransform;
    public GameObject helixLevelPrefab;

    public List<Stage> allStage = new List<Stage>();

    public float helixDistance;

    private List<GameObject>spawnedLevels = new List<GameObject>();

    private void Awake()
    {
        startRotation = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y + .1f);
        LoadStage(0);
    }

    void Update()
    {
       if(Input.GetMouseButton(0))
       {
        Vector2 currentTapPosition = Input.mousePosition;
        if(lastTapPosition == Vector2.zero)
        {
            lastTapPosition = currentTapPosition;
        }
        float distance = lastTapPosition.x - currentTapPosition.x;
        lastTapPosition = currentTapPosition;

        transform.Rotate(Vector3.up * distance);

       }

       if(Input.GetMouseButtonUp(0))
       {
        lastTapPosition = Vector2.zero;
       }

    }

    public void LoadStage(int stageNumber)
    {
        Stage stage = allStage[Mathf.Clamp(stageNumber,0,allStage.Count-1)];

        if (stage==null)
        {
            Debug.Log("No Stages");
            return;
        }

        Camera.main.backgroundColor = allStage[stageNumber].stageBackgroundColor;

        FindObjectOfType<BallController>().GetComponent<Renderer>().material.color = allStage[stageNumber].stageBallColor;

        transform.localEulerAngles = startRotation;

        foreach(GameObject go in spawnedLevels)
        {
            Destroy(go);
        }

        float levelDistance = helixDistance/stage.Levels.Count;
        float spawnPosY = topTransform.localPosition.y;

        for (int i = 0; i < stage.Levels.Count; i++)
        {
            spawnPosY -= levelDistance;

            GameObject level = Instantiate(helixLevelPrefab,transform);

            level.transform.localPosition = new Vector3(0, spawnPosY, 0);

            spawnedLevels.Add(level);

            int partsToDisable = 12 - stage.Levels[i].partCount;

            List<GameObject> disableParts = new List<GameObject>();

            while(disableParts.Count<partsToDisable)
            {
                GameObject randomPart = level.transform.GetChild(Random.Range(0,level.transform.childCount)).gameObject;

                if(!disableParts.Contains(randomPart))
                {
                    randomPart.SetActive(false);
                    disableParts.Add(randomPart);
                }
            }

            List<GameObject> leftPart = new List<GameObject>();

            foreach( Transform t in level.transform)
            {
                t.GetComponent<Renderer>().material.color = allStage [stageNumber].stageLevelPartColor;

                if(t.gameObject.activeInHierarchy)
                {
                    leftPart.Add(t.gameObject);
                }
            }
            
            List<GameObject>deathParts = new List<GameObject>();
            {
                while(deathParts.Count<stage.Levels[i].deathPartCount)
                {
                    GameObject randomPart = leftPart[Random.Range(0,leftPart.Count)];

                    if(!deathParts.Contains(randomPart))
                    {
                        randomPart.gameObject.AddComponent<DeathPart>();
                        deathParts.Add(randomPart);
                    
                    }
                }
            }

        }

    }


}
