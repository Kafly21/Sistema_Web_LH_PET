using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP_03_UC08_LH_PET_WEB.Controllers
{
    [Table("tb_item_venda")] public class ItemVenda
    {
        [Key]
        [Column("pk_item_venda")]
        public int Id { get; set; }

        [Column("fk_venda")]
        public int VendaId { get; set; }
        public Venda? Venda { get; set; }

        [Column("fk_produto")]
        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }

        [Column("vl_quantidade")]
        public int Quantidade { get; set; }

        [Column("vl_preco_unitario")]
        public decimal PrecoUnitario { get; set; }
    }
}
