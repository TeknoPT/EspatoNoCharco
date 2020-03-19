using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos.Interfaces
{
    interface ICollide
    {
        Boolean IsColliding(BoundingSphere bounding);
        Boolean IsColliding(BoundingBox bounding);
    }
}
