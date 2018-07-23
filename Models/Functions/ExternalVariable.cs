﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using Models.Core;
using System.Globalization;

namespace Models.Functions
{
    /// <summary>
    /// # [Name]
    /// Returns the value of a nominated external APSIM numerical variable.
    /// Note: This should be merged with the variable function when naming convention
    /// to refer to internal and external variable is standardized. FIXME
    /// </summary>
    [Serializable]
    [Description("Returns the value of a nominated external APSIM numerical variable")]
    [ViewName("UserInterface.Views.GridView")]
    [PresenterName("UserInterface.Presenters.PropertyPresenter")]
    public class ExternalVariable : Model, IFunction
    {
        /// <summary>The variable name</summary>
        [Description("VariableName")]
        public string VariableName { get; set; }

        /// <summary>Gets the value.</summary>
        public double Value(int arrayIndex = -1)
        {
            object val = Apsim.Get(this, VariableName);

            if (val != null)
            {
                if (val is Array && arrayIndex > -1)
                    return Convert.ToDouble((val as Array).GetValue(arrayIndex), CultureInfo.InvariantCulture);
                else
                    return Convert.ToDouble(val, CultureInfo.InvariantCulture);
            }
            else
                throw new Exception(Name + ": External value for " + VariableName.Trim() + " not found");
        }

    }
}