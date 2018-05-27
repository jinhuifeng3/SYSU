using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour {

    public new ParticleSystem particleSys;
    private ParticleSystem.Particle[] particleArr;//粒子数组
    public int particleNum = 450;//粒子数目
    public float radius = 8.0f;//粒子半径
    public float[] particleAngle; //发射角度
    public float[] particleRadius;//发射半径

    public float speed = 0.15f;//运动速度
    void Start()
    {
        particleArr = new ParticleSystem.Particle[particleNum]; 
        particleSys.maxParticles = particleNum;//粒子发射的最大数量  
        particleAngle = new float[particleNum];
        particleRadius = new float[particleNum];

        particleSys.Emit(particleNum);//发射初始化的粒子  
        particleSys.GetParticles(particleArr);

        //给粒子设定相应的位置
        for (int i = 0; i < particleNum; i++)
        {
            float r = Random.Range(radius, 12.0f);
            float angle = Random.Range(0.0f, 360.0f);
            float rad = angle / 180 * Mathf.PI; //弧长
            particleAngle[i] = angle;
            particleRadius[i] = r;
            particleArr[i].position = new Vector3(r * Mathf.Cos(rad), r * Mathf.Sin(rad), 0.0f);//粒子坐标 
        }
        particleSys.SetParticles(particleArr, particleArr.Length);
    }
    void Update()
    {
        for (int i = 0; i < particleNum; i++)
        {
            //不同速度
            if (i % 2 == 0)
            {
                particleAngle[i] += speed * (i % 7 + 1);
            }
            else
            {
                particleAngle[i] -= speed * (i % 7 + 1);
            }
            if (particleAngle[i] > 360)
                particleAngle[i] -= 360;
            if (particleAngle[i] < 0)
                particleAngle[i] += 360;
            float rad = particleAngle[i] / 180 * Mathf.PI;
            particleArr[i].position = new Vector3(particleRadius[i] * Mathf.Cos(rad), particleRadius[i] * Mathf.Sin(rad), 0f);
        }
        particleSys.SetParticles(particleArr, particleNum);
    }
}
