using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.Generator
{
    class Model
    {
        string key;
        string roomName;

        public Model(){}
        public Model(string key, string roomName)
        {
            this.key = key;
            this.roomName = roomName;
        }

        public string Key { get => key; set => key = value; }
        public string RoomName { get => roomName; set => roomName = value; }
    }
}
