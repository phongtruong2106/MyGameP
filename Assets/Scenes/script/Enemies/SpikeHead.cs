using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : MonoBehaviour
{

    [Header("SpikeHead Attributes")]
    //goi vector 3 lam diem den khi phat hien nguoi choi , ta luu vi tri nguoi choi
    [SerializeField] private float Speed;
    [SerializeField] private float Range; //khoang khong co the nhin thay cua spike
    [SerializeField] private float checkDelay; //kiem tra do tre
    [SerializeField] private LayerMask layerplayer;
    //tao mot mag vector moi de chi huog
    private Vector3[] directions = new Vector3[4]; //so 4 trong nay nghia la 4 huong
    private Vector3 destination;
    private float checkTimer; // kiem tra hen gio
    //tao bien bool attacking , de nhan biet dang tan cong nguoi choi 
    private bool attacking;

  

    private void OnEnable()
    {
        Stop();
    }
    

    private void Update()
    {
        //duy chuyen den noi cuoi cung, neu khong thi se khong lam gi chuyen sang buoc tiep theo
        if(attacking)
        //su dung tranform.translate de duy chuyen spike di ve phia diem den da duoc luu
        transform.Translate(destination * Time.deltaTime * Speed ); //diem den theo thoi gian deltaTime theo mot so speed;
        else
        {
            checkTimer += Time.deltaTime; //tang bo dem thoi gian 
            //bat cu khi nao dong ho bam gio kiem tra se lon hon so voi do tre
            if(checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }

    

    private void CheckForPlayer()
    {
        CalculateDirections();
        //kiem tra xem spikehead thay nguoi choi hay khong
        for(int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red); //dung thuoc tinh color de nhan biet do dai khoang cach
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], Range, layerplayer);

            if(hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
        
    }

    //su dung phuong thuc tinh toan calculate
    private void CalculateDirections()
    {
        //dau tien la canh phai
        directions[0] = transform.right* Range;
        //trai
        directions[1] = -transform.right* Range;
        //tren
        directions[2] = transform.up * Range;
        //duoi
        directions[3] = -transform.up* Range;
    }

    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }

 
}
