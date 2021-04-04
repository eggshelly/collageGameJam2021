using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpdateSpriteType
{
    Cycle,
    UpdateOnCertainKeys, //e.g. if I press W, it will change to one sprite, if i press S it will change to different sprite
    UpdateOnCertainKeysWithIdle,  //only different is there will be a default sprite that it will return to if no keys are pressed
    Toggle, //binary on or off
    OnEvent,
    None
}
