using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    Yemek.Resource _wood, _food;

    public Yemek.Resource Wood { get
        {
            return _wood;
        }
        }
    public Yemek.Resource Food { get
        {
            return _food;
        }
    }
}
