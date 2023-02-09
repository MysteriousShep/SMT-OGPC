using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    public List<GameObject> towers;
    public int selectedTower = 0;
    public GameObject preview;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
        }
        preview.transform.position = transform.position;
        if (Input.GetButtonDown("Jump"))
        {
            Instantiate(towers[selectedTower],new Vector3(transform.position.x,transform.position.y,-0.01f),transform.rotation);
        }
    }

}
