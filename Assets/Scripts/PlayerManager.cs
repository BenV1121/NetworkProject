using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PlayerManager
{
    public static PlayerController localPlayer = null;

    public static List<PlayerController> players = new List<PlayerController>();
}
