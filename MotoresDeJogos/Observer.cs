using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos
{
    public abstract class Observer
    {
        public abstract void OnNotify();
    }

    // We can create many subjects and give them a proper name for the interested group
    public class Subject
    {
        List<Observer> observers = new List<Observer>();

        public void Notify()
        {
            for(int i=0; i < observers.Count; i++)
            {
                observers[i].OnNotify();
            }
        }

        public void AddObserver(Observer observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(Observer observer)
        {
            observers.Remove(observer);
        }
    }
}
