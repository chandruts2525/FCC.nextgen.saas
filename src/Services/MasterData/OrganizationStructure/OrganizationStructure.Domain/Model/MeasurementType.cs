using FCC.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationStructure.Domain.Model
{
    public class MeasurementType : BaseEntity
    {
        [Key]
        public int MeasurementTypeID { get; set; }
        public string MeasurementTypeName { get; set; }
    }
}
