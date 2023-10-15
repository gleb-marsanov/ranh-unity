using System.Collections;
using UnityEngine;

namespace _HW3.Infrastructure.Bootstrap
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}