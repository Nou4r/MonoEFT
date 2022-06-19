using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace eft
{
    public static class entry
    {
        private static GameObject @object;
        public static void init()
        {
            @object = new GameObject( );
            @object.AddComponent<MonoCheat>( );
            UnityEngine.Object.DontDestroyOnLoad( @object );
        }
    }
}
