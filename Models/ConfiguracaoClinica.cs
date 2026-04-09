using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SP_03_UC08_LH_PET_WEB.Controllers
{
    [Table("tb_configuracao_clinica")] public class ConfiguracaoClinica
    {
        [Key]
        [Column("pk_configuracao")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O horário de abertura é obrigatório.")]
        [Column("tm_abertura")]
        public TimeSpan HoraAbertura { get; set; }

        [Column("ds_dias_trabalho")]
        public string DiasTrabalho { get; set; } = string.Empty; // Guardará "1,2,3,4,5" (Segunda a Sexta)

        [Required(ErrorMessage = "O tempo da consulta é obrigatório.")]
        [Column("vl_minutos_consulta")]
        public int MinutosConsulta { get; set; }

        [Required(ErrorMessage = "O tempo do banho é obrigatório.")]
        [Column("vl_minutos_banho")]
        public int MinutosBanho { get; set; }

        [Required(ErrorMessage = "O tempo da tosa é obrigatório.")]
        [Column("vl_minutos_tosa")]
        public int MinutosTosa { get; set; }
    }
}