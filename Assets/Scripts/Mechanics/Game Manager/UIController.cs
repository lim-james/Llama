using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseObj;
    [SerializeField]
    private TimeController timeController;
    [SerializeField]
    private FruitManager fruitsManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePause()
    {
        if (pauseObj.active == false)
        {
            // pause
            pauseObj.SetActive(true);

            for(int i = 0; i < fruitsManager.fruitRBList.Count; ++i)
            {
                fruitsManager.fruitRBList[i].constraints = RigidbodyConstraints.FreezePosition;
                //fruitsManager.fruitRBList[i].isKinematic = true;
            }
        }
        else
        {
            pauseObj.SetActive(false);

            for (int i = 0; i < fruitsManager.fruitRBList.Count; ++i)
            {
                fruitsManager.fruitRBList[i].constraints = RigidbodyConstraints.None;
                //fruitsManager.fruitRBList[i].isKinematic = true;
            }
        }

        timeController.TogglePaused();
    }
}
