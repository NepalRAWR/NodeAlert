using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using UnityEngine;

namespace NodeAlert
{

    class Pathfinder
    {

        static public string dllPath()
        {   
            string dllLoc;
            dllLoc = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/";
            return dllLoc;
            
        }

        static public string dllFolderName()
        {
            string dllFolder;
            dllFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location).Split(Path.DirectorySeparatorChar).Last() + "/";
            
            
            return dllFolder;
        }
    }
}