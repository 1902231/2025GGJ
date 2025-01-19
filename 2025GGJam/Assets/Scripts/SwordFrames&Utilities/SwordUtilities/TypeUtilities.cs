using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace SwordFrames
{
    public class TypeUtilities :SingletonMonoBaseAuto<TypeUtilities>
    {
        public bool TypeExits(string nameWithNameSpace)
        {
            Assembly[] assem = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly asm in assem)
            {
                Type[] types = asm.GetTypes();
                foreach (Type t in types)
                {
                    if (t.FullName == nameWithNameSpace)
                    {
                        return true;
                    }
                }
            }
            return false;

        }
        public void RemoveComponent(GameObject obj,string scrName)
        {
            Destroy(obj.GetComponent(scrName));


        }

    }
}