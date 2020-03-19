using Microsoft.Xna.Framework.Graphics;

namespace MotoresDeJogos.Models
{
    class BaseModel
    {
        protected Model model;

        public Model Model
        {
            get { return model; }
            set { model = value; }
        }

        public BaseModel(Model model)
        {
            this.model = model;
        }
    }
}
