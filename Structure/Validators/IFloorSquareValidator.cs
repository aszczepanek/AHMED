﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structure.Validators
{
    public interface IFloorSquareValidator
    {
        ValidatorInfo Validate(int x, int y, Floor fa, ValidationResult result);
    }
}
