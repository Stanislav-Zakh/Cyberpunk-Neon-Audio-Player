using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio_Player_NightWalk
{
    /// <summary>
    /// Base class that indicates that derived class is a singelton and provides familiar Instance method for derived classes to use.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingeltonViewModel<T> : BaseViewModel where T : SingeltonViewModel<T>
    {
        private static readonly Lazy<T> _instance = new (() => (Activator.CreateInstance(typeof(T), true) as T)!);

        public static T Instance => _instance.Value;

    }
}
