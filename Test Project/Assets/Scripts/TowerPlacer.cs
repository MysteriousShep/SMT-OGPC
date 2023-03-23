using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    public List<GameObject> towers;
    public int selectedTower = 0;
    public GameObject preview;
    public GameObject player;
    public bool playing = false;
    private float totalDelay = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!playing)
        {
            if (Input.GetButtonDown("Right"))
            {
                transform.Translate(new Vector3(1,0,0));
            }
            if (Input.GetButtonDown("Left"))
            {
                transform.Translate(new Vector3(-1,0,0));
            }
            if (Input.GetButtonDown("Up"))
            {
                transform.Translate(new Vector3(0,1,0));
            }
            if (Input.GetButtonDown("Down"))
            {
                transform.Translate(new Vector3(0,-1,0));
            }
            if (Input.GetButtonDown("E"))
            {
                selectedTower += 1;
                if (selectedTower >= towers.Count)
                {
                    selectedTower = 0;
                }
                preview.GetComponent<SpriteRenderer>().sprite = towers[selectedTower].GetComponent<SpriteRenderer>().sprite;
                if (towers[selectedTower].GetComponent<BassCannonShoot>() != null)
                {
                    preview.GetComponent<SpriteRenderer>().sprite = towers[selectedTower].GetComponent<BassCannonShoot>().bottom.GetComponent<SpriteRenderer>().sprite;
                }
            }
            preview.transform.position = transform.position;
            if (Input.GetButtonDown("Jump"))
            {
                GameObject lastTower = Instantiate(towers[selectedTower],new Vector3(transform.position.x,transform.position.y,-0.02f),transform.rotation);
                totalDelay += 0.05f;
                if (lastTower.GetComponent<BassCannonShoot>() != null)
                {
                    totalDelay -= 0.05f;
                }
                if (lastTower.GetComponent<LaserShoot>() != null)
                {
                    lastTower.GetComponent<LaserShoot>().fireStartDelay = totalDelay;
                }
                if (lastTower.GetComponent<StormCallerShoot>() != null)
                {
                    lastTower.GetComponent<StormCallerShoot>().fireStartDelay = totalDelay;
                }
                if (lastTower.GetComponent<TrackerShoot>() != null)
                {
                    lastTower.GetComponent<TrackerShoot>().fireStartDelay = totalDelay;
                }
            }
            if (Input.GetButtonDown("Submit"))
            {
                playing = true;
                player.SetActive(true);
                gameObject.SetActive(false);
                preview.gameObject.SetActive(false);
            }
        }
    }

}
