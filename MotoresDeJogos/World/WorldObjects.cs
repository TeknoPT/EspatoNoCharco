using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos.World
{
    public static class WorldObjects
    {
        public static Dictionary<DuckTypes, Model> Ducks = new Dictionary<DuckTypes, Model>();
        public static Model Terrain;
        public static Model Tree;
        public static Model Flower;
        public static Model Fish;
        public static Model Fountain;
        public static Model Lake;

        public static void InitModels(ContentManager content)
        {
            Ducks.Add( DuckTypes.Black, content.Load<Model>("models\\Black_duckie"));
            Ducks.Add( DuckTypes.While, content.Load<Model>("models\\White_duckie"));
            Ducks.Add( DuckTypes.Red, content.Load<Model>("models\\Red_Duckie"));
            Ducks.Add( DuckTypes.Green, content.Load<Model>("models\\Green_duckie"));
            Ducks.Add( DuckTypes.Blue, content.Load<Model>("models\\Blue_Duckie"));
            Terrain = content.Load<Model>("models\\Scenery");
            Tree = content.Load<Model>("models\\tree");
            Flower = content.Load<Model>("models\\Flower_v3.1");
            Fish = content.Load<Model>("models\\Flower_v3.1");
            Fountain = content.Load<Model>("models\\Fountain_v1");
            Lake = content.Load<Model>("models\\Water");
        }

        public static void UnloadEverything()
        {
            Ducks.Clear();
            Terrain = null;
            Tree = null;
            Flower = null;
            Fish = null;
            Fountain = null;
            Lake = null;
        }
    }

    public enum DuckTypes
    {
        Black = 0,
        While = 1,
        Red = 2,
        Green = 3,
        Blue = 4
    }
    
}
