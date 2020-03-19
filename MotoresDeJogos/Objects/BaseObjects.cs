using Microsoft.Xna.Framework;
using MotoresDeJogos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos.Objects
{
    class BaseObjects
    {
        protected BaseModel model;

        public BaseModel Model
        {
            get { return model; }
            set { model = value; }
        }

        protected Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        protected Matrix world;

        public BaseObjects(BaseModel model)
        {
            this.model = model;
        }
    }
}
