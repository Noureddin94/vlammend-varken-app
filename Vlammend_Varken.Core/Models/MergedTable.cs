using System.ComponentModel.DataAnnotations.Schema;

namespace Vlammend_Varken.Models
{
    public class MergedTable
    {
        public int Id { get; set; }

        [ForeignKey("MainTable")]
        public int MainTableId { get; set; }
        public required Table MainTable { get; set; }

        [ForeignKey("MergedTableReference")]
        public int MergedTableId { get; set; }
        public Table? MergedTableReference { get; set; }
        public string MergedBy { get; set; } = string.Empty;
        public DateTime MergedAt { get; set; } = DateTime.Now;
    }
}
