using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos.World
{
    static class WorldObjects
    {
        static Dictionary<DuckTypes, Model> Ducks;
        static Model Terrain;
        static Model Tree;
        static Model Flower;
        static Model Fish;
        static Model Fountain;
        static Model Lake;

        public static void InitModels(ContentManager content)
        {
            Ducks.Add( DuckTypes.Black, content.Load<Model>("models\\p1_saucer"));
            Ducks.Add( DuckTypes.While, content.Load<Model>("models\\p1_saucer"));
            Ducks.Add( DuckTypes.Red, content.Load<Model>("models\\p1_saucer"));
            Ducks.Add( DuckTypes.Green, content.Load<Model>("models\\p1_saucer"));
            Ducks.Add( DuckTypes.Blue, content.Load<Model>("models\\p1_saucer"));
            Terrain = content.Load<Model>("models\\terrain");
            Tree = content.Load<Model>("models\\tree");
            Flower = content.Load<Model>("models\\p1_saucer");
            Fish = content.Load<Model>("models\\p1_saucer");
            Fountain = content.Load<Model>("models\\p1_saucer");
            Lake = content.Load<Model>("models\\p1_saucer");
        }
    }

    public enum DuckTypes
    {
        Black,
        While,
        Red,
        Green,
        Blue
    }
    
}
