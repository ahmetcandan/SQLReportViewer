using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SQLReportViewer.Data.Model
{
    public class DbConnection
    {
        public int DbConnectionId { get; set; }

        [Required]
        public string ConnectionName { get; set; }

        [Required]
        public string ConnectionString { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsDelete { get; set; }
    }
}
