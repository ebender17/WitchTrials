using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WT.Pool
{
    public interface IPool<T>
    {
        void Prewarm(int num);
        T Request();
        void Return(T member); 
    }
}

