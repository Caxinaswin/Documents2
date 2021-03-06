﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MSDiary.Models
{
    public class TipoPagamento
    {
        public int TipoPagamentoId { get; set; }
        [Display(Name = "Tipo de Pagamento")]
        [Required]
        public string TipoPagamentoNome { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string ApplicationUserId { get; set; }
    }
}