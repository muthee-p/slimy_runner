using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float ShakeIntensity =4f;
    private float shakeTime = 0.5f;

    private float timer;
    private CinemachineBasicMultiChannelPerlin _cbmp;

    void Awake(){
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    void Start()
    {
        StopShake();
    }

    public void ShakeCamera(){
        CinemachineBasicMultiChannelPerlin _cbmp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmp.m_AmplitudeGain = ShakeIntensity;
        timer =shakeTime;
       
    }

    public void StopShake(){
        CinemachineBasicMultiChannelPerlin _cbmp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmp.m_AmplitudeGain = 0f;
        timer = 0f;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.K)){
            ShakeCamera();
        }
            if(timer >0){
            timer -=Time.deltaTime;
            if(timer <=0){
                StopShake();
            }
        
        }
        
    }
}
