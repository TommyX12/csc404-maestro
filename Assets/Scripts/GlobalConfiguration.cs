using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GlobalConfiguration : ScriptableObject {

    public GlobalConfiguration() {
        
    }

    public float GetBPM() {
        return 110;
    }
}
