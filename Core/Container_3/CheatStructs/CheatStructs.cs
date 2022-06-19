using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace eft
{
    public static class CheatStructs
    {

        public class Box
        {
            public Vector2 Position;
            public Vector2 Size;
        }

        public class HitMarker
        {
            public Vector3 HitPos;
            public float Time;
        }
        public class Tracer
        {
            public Vector3 StartPos;
            public Vector3 EndPos;
            public float Time;
        }

    }
}
