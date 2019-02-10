using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissappearingCubeGroup : MonoBehaviour
{
    public List<DissappearingBlock> Blocks = new List<DissappearingBlock>();
    private int idx = -1;

    private void Start()
    {
        if (Blocks.Count > 0)
        {
            idx = 0;
        }
        else {
            Destroy(this);
        }

        foreach (DissappearingBlock b in Blocks)
        {
            b.Dissappear();
        }
    }

    public void OnBeat() {
        foreach (DissappearingBlock b in Blocks) {
            b.Dissappear();
        }
        Blocks[idx].Appear();
        idx++;
        idx %= Blocks.Count;
    }


}
