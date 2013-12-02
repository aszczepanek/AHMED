﻿using Genetics.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Main.GeneticsConfiguration
{
    public interface ICrossoverConfiguration
    {
        ICrossoverOperator<List<bool>> BuildCrossoverOperator();
    }
}