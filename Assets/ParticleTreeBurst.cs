using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTreeBurst : MonoBehaviour{
    public void BurstParticle(){
        GetComponentInChildren<ParticleSystem>().Emit(50);
    }
}
