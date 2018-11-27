using System;
using System.Collections.Generic;

namespace Phnx.Web.Tests.Models
{
    public class JsonTestModel
    {
        public string FullName { get; set; }

        public string Id { get; set; }

        public DateTime LastChange { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is JsonTestModel o))
            {
                return false;
            }

            return
                o.FullName == FullName &&
                o.Id == Id &&
                o.LastChange == LastChange;
        }

        public override int GetHashCode()
        {
            var hashCode = -202351584;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FullName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + LastChange.GetHashCode();
            return hashCode;
        }
    }
}
