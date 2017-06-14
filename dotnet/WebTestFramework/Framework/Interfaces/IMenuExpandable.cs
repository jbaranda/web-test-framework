using System;

namespace Framework.Interfaces
{
    public interface IMenuExpandable : IMenu
    {
        void ExpandMenu(Enum item, bool hover);
    }
}
