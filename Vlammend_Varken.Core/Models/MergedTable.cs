using System.ComponentModel.DataAnnotations.Schema;
using Vlammend_Varken.Core.Models;

namespace Vlammend_Varken.Core.Models
{
    public class MergedTable
    {
        public int Id { get; set; }

        [ForeignKey("MainTable")]
        public int MainTableId { get; set; }
        public Table? MainTable { get; set; }

        [ForeignKey("MergedTableReference")]
        public int MergedTableId { get; set; }
        public Table? MergedTableReference { get; set; }
        public string MergedBy { get; set; } = string.Empty;
        public DateTime MergedAt { get; set; } = DateTime.Now;
    }
}
