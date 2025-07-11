using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniStats
{
    public interface ITextProvider
    {
        string Text { get; }
        void Init();
    }
}