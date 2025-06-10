using System.ComponentModel.DataAnnotations.Schema;

namespace Vlammend_Varken.Models
{
    public class MergedTables
    {
        public int Id { get; set; }
        
        [ForeignKey("MainTable")]
        public int MainTableId { get; set; }
        public required Table MainTable { get; set; }
        
        [ForeignKey("MergedTable")]
        public int MergedTableId { get; set; }
        public Table? MergedTable { get; set; }
    }
}
