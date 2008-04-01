using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics;

namespace F2D.Core
{
    public sealed class Farseer
    {
        static readonly PhysicsSimulator physics = new PhysicsSimulator(new Vector2(0f, 300f));
        static Farseer() { } 
        Farseer() { }
        //block other classes from instantiating it;
        //only the declaration and ctor can make assignments

        public static PhysicsSimulator Physics
        {
            get { return physics; }
            //no need for set as it's read only
        }
    }

}