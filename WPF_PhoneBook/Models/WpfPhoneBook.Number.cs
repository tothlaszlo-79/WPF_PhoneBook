//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2021. 04. 01. 16:17:53
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace WPF_PhoneBook
{
    public partial class Number {

        public Number()
        {
            this.People = new List<Person>();
            OnCreated();
        }

        public virtual int Id { get; set; }

        public virtual string NumberString { get; set; }

        public virtual IList<Person> People { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
