using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    // Indicated whether or not the item has been collected by the player
    bool Collected {get; set;}

    Action OnCollected;
}
