using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPF_EoB.Classes
{
    public class NPCClass
    {
        public NPCClass() { }

        public NPCClass(string name, Tuple<byte, byte> position, byte spriteIndex)
        {
            mName = name;
            mPosition = position;
            mSpriteIndex = spriteIndex;
        }

        private string mName = string.Empty;
        public string Name { get { return mName; } }

        private Tuple<byte, byte> mPosition = new Tuple<byte, byte>(0, 0);
        public Tuple<byte, byte> Position { get { return mPosition; } }

        private byte mSpriteIndex = 0;
        public byte SpriteIndex { get { return mSpriteIndex; } }
    }
}
