using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    abstract class Component
    {
        public Component() { }

        public abstract void Operation();

        public abstract void Dodaj(Component c);

        public abstract void Usun(Component c);

        public abstract bool IsComposite();
    }

    class Composite : Component
    {
        List<Component> children = new List<Component>();

        public Composite()
        {

        }

        public override void Dodaj(Component component)
        {
            children.Add(component);
        }

        public override void Usun(Component component)
        {
            children.Remove(component);
        }

        public override bool IsComposite()
        {
            return true;
        }

        public override void Operation()
        {
            int i = 0;

            Console.Write("Galaz(");
            foreach (Component component in children)
            {
                component.Operation();
                if (i != children.Count - 1)
                {
                    Console.Write("+");
                }
                i++;
            }
            Console.Write(")");
        }
    }

    class Lisc : Component
    {
        public Lisc()
        {

        }

        public override void Operation()
        {
            Console.Write("Lisc");
        }

        public override void Dodaj(Component component)
        {
            throw new NotImplementedException();
        }

        public override void Usun(Component component)
        {
            throw new NotImplementedException();
        }

        public override bool IsComposite()
        {
            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            Component Lisc = new Lisc();        
            Composite tree = new Composite();
            Composite Galaz1 = new Composite();
            Galaz1.Dodaj(new Lisc());
            Galaz1.Dodaj(new Lisc());
            Composite Galaz2 = new Composite();
            Galaz2.Dodaj(new Lisc());
            tree.Dodaj(Galaz1);
            tree.Dodaj(Galaz2);
            client.ClientCode(Lisc);
            Console.WriteLine("\n----");
            client.ClientCode2(tree, Lisc);
            Console.ReadKey();
        }
    }

    class Client
    {
        public void ClientCode(Component Lisc)
        {
            
            Lisc.Operation();
        }

        public void ClientCode2(Component component1, Component component2)
        {
            if (component1.IsComposite())
            {
                component1.Dodaj(component2);
            }
           
            component1.Operation();

            Console.WriteLine();
        }
    }
}