using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Models
{
    public interface IMark : IIdentifible
    {
        string Name { get; set; }
    }
    public class Mark : IMark
    {
        private static int _counter = 0;

        private int _id;
        private string _name;

        public int Id { get { return _id; } }
        public string Name
        {
            get { return _name; }
            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Mark name is required.");
                _name = value;
            }
        }

        public Mark(string name)
        {
            this._id = ++_counter;

            this.Name = name;
        }

        public override string ToString()
        {
            return $"Mark: {this.Name}";
        }
    }
}
