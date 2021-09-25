using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coletavel : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        GameManager.instance.AddCollectible();

        GameManager.instance.audioPlayer.PlayCollectSoundEffect();

        Destroy(this.gameObject);
    }
}
