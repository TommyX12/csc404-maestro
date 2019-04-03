using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

[CreateAssetMenu
 (fileName = "Level Configurations",
  menuName = "Configurations/Level Configuration")]
public class LevelConfiguration : ScriptableObject {

    public List<Riff.Note> ExtraBeatsRiffNotes;
    
}
