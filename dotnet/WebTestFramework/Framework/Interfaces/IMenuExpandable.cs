using System;

namespace Framework.Interfaces
{
    interface IMenuExpandable : IMenu
    {
        void ExpandMenu(Enum item);
    }
}
