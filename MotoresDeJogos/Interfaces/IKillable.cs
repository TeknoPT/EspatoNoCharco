﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos.Interfaces
{
    interface IKillable
    {
        bool IsDead();
        void Damage();
        void Damage(float damage);
        void Destroy();
    }
}
