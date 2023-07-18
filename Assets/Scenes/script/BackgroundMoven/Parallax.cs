using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
  [SerializeField]  private Transform mainCam;
  [SerializeField]  private Transform middleBG;
  [SerializeField]  private Transform sideBG;


  [SerializeField]  private float lenght = 38.4f;


    private void Update()
    {
        if (mainCam.position.x > middleBG.position.x)
            sideBG.position = middleBG.position + Vector3.right * lenght;

        if(mainCam.position.x < middleBG.position.x)
            sideBG.position = middleBG.position + Vector3.left * lenght;


        if (mainCam.position.x > sideBG.position.x || mainCam.position.x < sideBG.position.x)
        {
            Transform z = middleBG;
            middleBG = sideBG;
            sideBG = z;
        }
           

    }
}
